using Application.DTO.Reservation;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.CQRS.Reservation.Command.CreateReservationWithTransaction
{
    public class CreateReservationWithTransactionCommandHandler : IRequestHandler<CreateReservationWithTransactionCommand, ReservationDetailsDto>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;
        private readonly IBicycleRentalDbContext _dbContext;
        private readonly IGuestRepository _guestRepository;
        private readonly IBicycleRepository _bicycleRepository;
        private readonly IGeoLocationService _geoLocationService;

        public CreateReservationWithTransactionCommandHandler(IReservationRepository reservationRepository, IAddressRepository addressRepository, IMapper mapper, IBicycleRentalDbContext dbContext, IGuestRepository guestRepository, IBicycleRepository bicycleRepository, IGeoLocationService geoLocationService)
        {
            _reservationRepository = reservationRepository;
            _addressRepository = addressRepository;
            _mapper = mapper;
            _dbContext = dbContext;
            _guestRepository = guestRepository;
            _bicycleRepository = bicycleRepository;
            _geoLocationService = geoLocationService;
        }

        public async Task<ReservationDetailsDto> Handle(CreateReservationWithTransactionCommand request, CancellationToken cancellationToken)
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                //Address
                var address = new Domain.Entities.Address();
                if (request.Address.Type == AddressType.GuestAddress)
                {
                    var coordinatesResult = await _geoLocationService.GetCoordinatesAsync(new Domain.Entities.Address
                    {
                        Street = request.Address.Street,
                        City = request.Address.City,
                        PostalCode = request.Address.PostalCode
                    });

                    if (coordinatesResult == null)
                        throw new AddressResolutionException("Unable to resolve the coordinates for the given address.");


                    if (!_geoLocationService.IsWithinDeliveryRange(coordinatesResult.Value.Lat, coordinatesResult.Value.Lng))
                        throw new DeliveryRangeExceededException("The address is outside of the delivery range.");

                    var newAddress = new Domain.Entities.Address
                    {
                        Street = request.Address.Street,
                        City = request.Address.City,
                        PostalCode = request.Address.PostalCode,
                        Type = request.Address.Type,
                    };

                    await _addressRepository.CreateAsync(newAddress);
                    await _dbContext.SaveChangesAsync(cancellationToken);
                    address = newAddress;
                }
                else if (request.Address.Id != null)
                {
                    address = await _addressRepository.GetByIdAsync(request.Address.Id.Value);
                }
                else
                {
                    throw new ArgumentException("Address is required for pickup point reservations.");
                }
                //Guest
                var newGuest = new Domain.Entities.Guest
                {
                    FirstName = request.Guest.FirstName,
                    LastName = request.Guest.LastName,
                    Email = request.Guest.Email,
                    Phone = request.Guest.Phone,
                };
                var guest = await _guestRepository.CreateAsync(newGuest);
                await _dbContext.SaveChangesAsync(cancellationToken);
                //Bicycles  
                var allReservedBicycles = new List<Domain.Entities.Bicycle>();

                foreach (var item in request.Bicycles)
                {
                    var available = await _bicycleRepository.GetAvailableBicycles(
                        request.StartDate,
                        request.EndDate,
                        item.Size,
                        item.Name
                    );

                    if (available.Count < item.Quantity)
                        throw new BicyclesUnavailableException($"Not enough bicycles available for {item.Name} ({item.Size}). Requested: {item.Quantity}, Available: {available.Count}");

                    allReservedBicycles.AddRange(available.Take(item.Quantity));
                }

                //Reservation
                var reservation = new Domain.Entities.Reservation
                {
                    GuestId = guest.Id,
                    AddressId = address.Id,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    TotalPrice = request.TotalPrice,
                    DeliveryHours = request.DeliveryHours,
                    Bicycles = allReservedBicycles,
                };

                await _reservationRepository.CreateAsync(reservation);
                await _dbContext.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                var reservationDto = _mapper.Map<ReservationDetailsDto>(reservation);

                return reservationDto;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}

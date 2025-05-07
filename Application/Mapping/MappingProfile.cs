using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.CQRS.Address;
using Application.CQRS.Address.Command.Create;
using Application.CQRS.Bicycle;
using Application.CQRS.Bicycle.Command.Create;
using Application.CQRS.Reservation;
using Application.CQRS.Reservation.Command.Create;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //    CreateMap<Guest, GuestDto>().ReverseMap();
            //  CreateMap<Reservation, ReservationDto>().ReverseMap();
            //Bicycle
            CreateMap<Bicycle, BicycleDto>().ReverseMap();
            CreateMap<CreateBicycleCommand, Bicycle>().ReverseMap();
            //  CreateMap<Payment, PaymentDto>().ReverseMap();

            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<CreateAddressCommand, Address>().ReverseMap();

            CreateMap<Reservation, ReservationDto>().ReverseMap();
            CreateMap<CreateReservationCommand, Reservation>().ReverseMap();
        }
    }
}

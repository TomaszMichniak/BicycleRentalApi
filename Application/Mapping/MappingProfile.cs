using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.CQRS.Address;
using Application.CQRS.Address.Command.Create;
using Application.CQRS.Bicycle;
using Application.CQRS.Bicycle.Command.Create;
using Application.CQRS.Guest;
using Application.CQRS.Guest.Command.Create;
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
            //Address
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<CreateAddressCommand, Address>().ReverseMap();
            //Reservation
            CreateMap<Reservation, ReservationDto>().ReverseMap();
            CreateMap<CreateReservationCommand, Reservation>().ReverseMap();
            //Guest
            CreateMap<Guest, GuestDto>().ReverseMap();
            CreateMap<CreateGuestCommand, Guest>().ReverseMap();
        }
    }
}

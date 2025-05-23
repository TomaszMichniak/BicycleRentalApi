using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.CQRS.Address.Command.Create;
using Application.CQRS.Bicycle.Command.Create;
using Application.CQRS.Guest.Command.Create;
using Application.CQRS.Reservation.Command.Create;
using Application.DTO.Address;
using Application.DTO.Bicycle;
using Application.DTO.Guest;
using Application.DTO.Reservation;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Bicycle
            CreateMap<Bicycle, BicycleDetailsDto>().ReverseMap();
            CreateMap<CreateBicycleCommand, Bicycle>().ReverseMap();
            //Address
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<Address, AddressDetailsDto>().ReverseMap();
            CreateMap<CreateAddressCommand, Address>().ReverseMap();
            //Reservation
            CreateMap<Reservation, ReservationDetailsDto>().ReverseMap();
            CreateMap<CreateReservationCommand, Reservation>();
            //Guest
            CreateMap<Guest, GuestDetailsDto>().ReverseMap();
            CreateMap<CreateGuestCommand, Guest>().ReverseMap();
        }
    }
}

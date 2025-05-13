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
            CreateMap<CreateBicycleCommand, Bicycle>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Bicycle.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Bicycle.Description))
                .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Bicycle.Size))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Bicycle.ImageUrl))
                .ForMember(dest => dest.PricePerDay, opt => opt.MapFrom(src => src.Bicycle.PricePerDay))
                .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => src.Bicycle.IsAvailable)); ;
            //Address
            CreateMap<Address, AddressDetailsDto>().ReverseMap();
            CreateMap<CreateAddressCommand, Address>().ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Address.PostalCode)); ;
            //Reservation
            CreateMap<Reservation, ReservationDetailsDto>().ReverseMap();
            CreateMap<CreateReservationCommand, Reservation>();
            //Guest
            CreateMap<Guest, GuestDetailsDto>().ReverseMap();
            CreateMap<CreateGuestCommand, Guest>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Guest.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Guest.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Guest.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Guest.Phone));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Application.CQRS.Address;
using Application.CQRS.Address.Command.Create;
using Application.CQRS.Address.Command.Delete;
using Application.CQRS.Address.Command.Edit;
using Application.CQRS.Bicycle;
using Application.CQRS.Bicycle.Command.Create;
using Application.CQRS.Bicycle.Command.Delete;
using Application.CQRS.Bicycle.Command.Edit;
using Application.CQRS.Bicycle.Query.GetBySpecification;
using Application.CQRS.GenericHandlers;
using Application.CQRS.Guest;
using Application.CQRS.Guest.Command.Create;
using Application.CQRS.Guest.Command.Delete;
using Application.CQRS.Guest.Command.Edit;
using Application.CQRS.Reservation;
using Application.CQRS.Reservation.Command.Create;
using Application.CQRS.Reservation.Command.Delete;
using Application.CQRS.Reservation.Command.Edit;
using Application.Mapping;
using Application.Pagination;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class ServiceCollectionsExtensions
    {
        public static void AddApplication(this IServiceCollection Services)
        {
            Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            Services.AddScoped(provider => new MapperConfiguration(cfg =>
            {
                var scope = provider.CreateScope();
                cfg.AddProfile(new MappingProfile());
            }).CreateMapper()
          );
            Services.AddTransient(typeof(IGenericSpecificationSearchService<,>), typeof(GenericSpecificationSearchService<,>));
            Services.AddTransient<IRequestHandler<GetBicycleBySpecificationQuery, IPagedResult<BicycleDto>>, GetBicycleBySpecificationQueryHandler>();

            Services.AddValidatorsFromAssemblyContaining<CreateBicycleCommandValidator>();
            //Create
            Services.AddTransient<IRequestHandler<CreateBicycleCommand, BicycleDto>, GenericCreateCommandHandler<CreateBicycleCommand, Bicycle, BicycleDto>>();
            Services.AddTransient<IRequestHandler<CreateAddressCommand, AddressDto>, GenericCreateCommandHandler<CreateAddressCommand, Address, AddressDto>>();
            Services.AddTransient<IRequestHandler<CreateReservationCommand, ReservationDto>, GenericCreateCommandHandler<CreateReservationCommand, Reservation, ReservationDto>>();
            Services.AddTransient<IRequestHandler<CreateGuestCommand, GuestDto>, GenericCreateCommandHandler<CreateGuestCommand, Guest, GuestDto>>();
            //Delete
            Services.AddTransient<IRequestHandler<DeleteAddressCommand>, GenericDeleteCommandHandler<DeleteAddressCommand, Address>>();
            Services.AddTransient<IRequestHandler<DeleteBicycleCommand>, GenericDeleteCommandHandler<DeleteBicycleCommand, Bicycle>>();
            Services.AddTransient<IRequestHandler<DeleteReservationCommand>, GenericDeleteCommandHandler<DeleteReservationCommand, Reservation>>();
            Services.AddTransient<IRequestHandler<DeleteGuestCommand>, GenericDeleteCommandHandler<DeleteGuestCommand, Guest>>();
            //Edit
            Services.AddTransient<IRequestHandler<EditBicycleCommand, BicycleDto>, GenericEditCommandHandler<EditBicycleCommand, Bicycle, BicycleDto>>();
            Services.AddTransient<IRequestHandler<EditAddressCommand, AddressDto>, GenericEditCommandHandler<EditAddressCommand, Address, AddressDto>>();
            Services.AddTransient<IRequestHandler<EditReservationCommand, ReservationDto>, GenericEditCommandHandler<EditReservationCommand, Reservation, ReservationDto>>();
            Services.AddTransient<IRequestHandler<EditGuestCommand, GuestDto>, GenericEditCommandHandler<EditGuestCommand, Guest, GuestDto>>();

        }
    }
}

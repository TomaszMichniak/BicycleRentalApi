using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Application.CQRS.Address;
using Application.CQRS.Address.Command.Create;
using Application.CQRS.Address.Command.Delete;
using Application.CQRS.Bicycle;
using Application.CQRS.Bicycle.Command.Create;
using Application.CQRS.Bicycle.Command.Delete;
using Application.CQRS.Bicycle.Command.Edit;
using Application.CQRS.GenericHandlers;
using Application.CQRS.Reservation;
using Application.CQRS.Reservation.Command.Create;
using Application.CQRS.Reservation.Command.Delete;
using Application.Mapping;
using AutoMapper;
using Domain.Entities;
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
            Services.AddValidatorsFromAssemblyContaining<CreateBicycleCommandValidator>();
            //Create
            Services.AddTransient<IRequestHandler<CreateBicycleCommand, BicycleDto>, GenericCreateCommandHandler<CreateBicycleCommand, Bicycle, BicycleDto>>();
            Services.AddTransient<IRequestHandler<CreateAddressCommand, AddressDto>, GenericCreateCommandHandler<CreateAddressCommand, Address, AddressDto>>();
            Services.AddTransient<IRequestHandler<CreateReservationCommand, ReservationDto>, GenericCreateCommandHandler<CreateReservationCommand, Reservation, ReservationDto>>();
            //Delete
            Services.AddTransient<IRequestHandler<DeleteAddressCommand>, GenericDeleteCommandHandler<DeleteAddressCommand, Address>>();
            Services.AddTransient<IRequestHandler<DeleteBicycleCommand>, GenericDeleteCommandHandler<DeleteBicycleCommand, Bicycle>>();
            Services.AddTransient<IRequestHandler<DeleteReservationCommand>, GenericDeleteCommandHandler<DeleteReservationCommand, Reservation>>();
            //Edit
            Services.AddTransient<IRequestHandler<EditBicycleCommand, BicycleDto>, GenericEditCommandHandler<EditBicycleCommand, Bicycle, BicycleDto>>();

        }
    }
}

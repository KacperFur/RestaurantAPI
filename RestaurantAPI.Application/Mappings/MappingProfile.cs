using AutoMapper;
using RestaurantAPI.Application.Models;
using RestaurantAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Application.Mappings
{
    public class MappingProfile : Profile
    {   
        public MappingProfile()
        {
                // User
                CreateMap<User, UserDto>();
                CreateMap<CreateUserDto, User>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                    .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
            CreateMap<UpdateUserDto, User>();

                // MenuItem
                CreateMap<MenuItem, MenuItemDto>();
                CreateMap<CreateMenuItemDto, MenuItem>();
                CreateMap<UpdateMenuItemDto, MenuItem>()
                    .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                    .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

            // Order
            CreateMap<Order, OrderDto>();
                CreateMap<CreateOrderDto, Order>();
                CreateMap<UpdateOrderDto, Order>()
                    .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                    .ForMember(dest => dest.OrderItems, opt => opt.Ignore());

                // OrderItem
                CreateMap<OrderItem, OrderItemDto>();
                CreateMap<CreateOrderItemDto, OrderItem>();

                // Payment
                CreateMap<Payment, PaymentDto>();
                CreateMap<CreatePaymentDto, Payment>();
                CreateMap<UpdatePaymentDto, Payment>();

                // Reservation
                CreateMap<Reservation, ReservationDto>();
                CreateMap<CreateReservationDto, Reservation>();
                CreateMap<UpdateReservationDto, Reservation>();

                // Table
                CreateMap<Table, TableDto>();
                CreateMap<CreateTableDto, Table>();
                CreateMap<UpdateTableDto, Table>();

                
        }
    }
}

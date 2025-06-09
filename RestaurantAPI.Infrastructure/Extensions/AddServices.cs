using Microsoft.Extensions.DependencyInjection;
using RestaurantAPI.Application.Interfaces;
using RestaurantAPI.Application.Services;

namespace RestaurantAPI.Infrastructure.Extensions
{
    public static class AddServices
    {
        // Refactored to move service registrations to a separate extension method.
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register application services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITableService, TableService>();
            services.AddScoped<IMenuItemService, MenuItemService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IAuthService, AuthService>();
            // Add other services as needed
            return services;
        }
    }
}

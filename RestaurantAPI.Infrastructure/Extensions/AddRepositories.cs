using Microsoft.Extensions.DependencyInjection;
using RestaurantAPI.Domain.Interfaces;
using RestaurantAPI.Infrastructure.Repositories;

namespace RestaurantAPI.Infrastructure.Extensions
{
    public static class AddRepositories
    {
        // Refactored to move repository registrations to a separate extension method.
        public static IServiceCollection AddApplicationRepositories(this  IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMenuItemRepository, MenuItemRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<ITableRepository, TableRepository>();
            return services;
        }
    }
}

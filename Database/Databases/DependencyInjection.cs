using Database.Repositories;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Database.Databases
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<RealDatabase>(options =>
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Database")));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }


        //public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        //{

        //    services.AddDbContext<RealDatabase>(options => { options.UseSqlServer(connectionString); });

        //    services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        //    return services;
        //}

        //public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        //{
        //    services.AddSingleton<FakeDatabase>();
        //    return services;
        //}
    }
}
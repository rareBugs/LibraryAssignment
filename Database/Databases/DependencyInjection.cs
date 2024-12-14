using Microsoft.Extensions.DependencyInjection;

namespace Database.Databases
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastruture(this IServiceCollection services)
        {
            services.AddSingleton<FakeDatabase>();
            return services;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace JEdwards.Infrastructure.Database.Implemenations
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(connectionString));
            return services;
        }
    }
}

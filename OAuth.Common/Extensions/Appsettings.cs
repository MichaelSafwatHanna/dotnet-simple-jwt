using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OAuth.Common.Extensions
{
    public static class Appsettings
    {
        public static IServiceCollection AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration)
            => services.Configure<JwtConfiguration>(configuration.GetSection(nameof(JwtConfiguration)));
    }
}

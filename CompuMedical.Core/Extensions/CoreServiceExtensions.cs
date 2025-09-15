using CompuMedical.Core.Helper;
using Microsoft.Extensions.DependencyInjection;

namespace CompuMedical.Core.Extensions;

public static class CoreServiceExtensions
{
    public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        return services;
    }
}

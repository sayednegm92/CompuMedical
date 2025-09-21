using CompuMedical.Application.Helper;
using CompuMedical.Application.IServices.IAuthServices;
using CompuMedical.Application.IServices.IInvoices;
using CompuMedical.Application.Services.AuthServices;
using CompuMedical.Application.Services.Invoices;
using CompuMedical.Application.IServices.IProducts;
using CompuMedical.Application.Services.Products;
using CompuMedical.Core.Entities;
using CompuMedical.Core.Helper;
using CompuMedical.Core.IBasRepository;
using CompuMedical.Infrastructure.BasRepository;
using CompuMedical.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using System.Reflection;
using CompuMedical.Application.Services.Stores;
using CompuMedical.Application.IServices.IStores;

namespace CompuMedical.MVC.Extensions;

public static class MVCServiceExtensions
{
    public static IServiceCollection AddMVCDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<IResponseHandler, ResponseHandler>();
        services.AddScoped(typeof(IRepositoryApp<>), typeof(RepositoryApp<>));
        services.AddScoped<IInvoiceServices, InvoiceServices>();
        services.AddScoped<IProductServices, ProductServices>();
        services.AddScoped<IStoreServices, StoreServices>();
        services.AddScoped<IAuthService, AuthService>();
        #region JWT Authentication Configure
        var jwtSection = configuration.GetSection("Jwt");

        var jwtOptions = jwtSection.Get<JwtOptions>();
        services.AddSingleton(jwtOptions);
        #endregion

        #region Configure Identity
        services.AddIdentity<ApplicationUser, IdentityRole>()
           .AddEntityFrameworkStores<ApplicationDbContext>()
           .AddDefaultTokenProviders();
        #endregion
        return services;
    }
}

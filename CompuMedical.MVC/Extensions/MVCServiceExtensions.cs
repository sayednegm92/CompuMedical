using CompuMedical.Application.Helper;
using CompuMedical.Application.IServices.IAuthServices;
using CompuMedical.Application.IServices.IInvoices;
using CompuMedical.Application.IServices.IProducts;
using CompuMedical.Application.IServices.IStores;
using CompuMedical.Application.Services.AuthServices;
using CompuMedical.Application.Services.Invoices;
using CompuMedical.Application.Services.Products;
using CompuMedical.Application.Services.Stores;
using CompuMedical.Core.Entities;
using CompuMedical.Core.Helper;
using CompuMedical.Core.IBasRepository;
using CompuMedical.Infrastructure.BasRepository;
using CompuMedical.Infrastructure.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace CompuMedical.MVC.Extensions;

public static class MVCServiceExtensions
{
    public static IServiceCollection AddMVCDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddHttpClient();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<IResponseHandler, ResponseHandler>();
        services.AddScoped(typeof(IRepositoryApp<>), typeof(RepositoryApp<>));
        services.AddScoped<IInvoiceServices, InvoiceServices>();
        services.AddScoped<IProductServices, ProductServices>();
        services.AddScoped<IStoreServices, StoreServices>();
        services.AddScoped<IAuthService, AuthService>();


        #region Configure Identity
        services.AddIdentity<ApplicationUser, IdentityRole>()
           .AddEntityFrameworkStores<ApplicationDbContext>()
           .AddDefaultTokenProviders();
        #endregion

        #region JWT Authentication Configure
        var jwtSection = configuration.GetSection("Jwt");

        var jwtOptions = jwtSection.Get<JwtOptions>();
        services.AddSingleton(jwtOptions);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
             .AddJwtBearer(options =>
             {
                 options.RequireHttpsMetadata = false;
                 options.SaveToken = true;
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = jwtOptions.Issuer,
                     ValidAudience = jwtOptions.Audience,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey))
                 };
             });
        #endregion


        return services;
    }
}

using CompuMedical.Application.Helper;
using CompuMedical.Application.IServices.IAuthServices;
using CompuMedical.Application.IServices.IInvoices;
using CompuMedical.Application.IServices.IProducts;
using CompuMedical.Application.IServices.IStores;
using CompuMedical.Application.Services.AuthServices;
using CompuMedical.Application.Services.Invoices;
using CompuMedical.Application.Services.Products;
using CompuMedical.Application.Services.Stores;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CompuMedical.Application.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IResponseHandler, ResponseHandler>();
        services.AddScoped<IInvoiceServices, InvoiceServices>();
        services.AddScoped<IStoreServices, StoreServices>();
        services.AddScoped<IProductServices, ProductServices>();
        return services;
    }
}

using CompuMedical.Application.Extensions;
using CompuMedical.Core.Entities;
using CompuMedical.Core.Extensions;
using CompuMedical.Infrastructure.Context;
using CompuMedical.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Configure Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
   .AddEntityFrameworkStores<ApplicationDbContext>()
   .AddDefaultTokenProviders();
#endregion

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()  // Allow all origins (all domains and ports)
              .AllowAnyHeader()  // Allow any headers
              .AllowAnyMethod(); // Allow any HTTP methods (GET, POST, etc.)
    });
});


#region Connect To SQL Server
var connectionString = builder.Configuration.GetConnectionString(name: "DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(opions => opions.UseSqlServer(connectionString));
#endregion

#region Extension Methods
builder.Services.AddInfrastuctureDependencies(builder.Configuration);
builder.Services.AddApplicationDependencies();
builder.Services.AddCoreDependencies();
#endregion

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowAll");
app.MapControllers();

app.Run();

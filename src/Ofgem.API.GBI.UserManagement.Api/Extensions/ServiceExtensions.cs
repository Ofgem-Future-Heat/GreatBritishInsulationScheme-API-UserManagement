using FluentValidation;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.EntityFrameworkCore;
using Ofgem.API.GBI.UserManagement.Application.Interfaces;
using Ofgem.API.GBI.UserManagement.Application.Models;
using Ofgem.API.GBI.UserManagement.Application.Services;
using Ofgem.API.GBI.UserManagement.Application.Validators;
using Ofgem.API.GBI.UserManagement.Infrastructure.Repositories;
using Ofgem.Database.GBI.Users.Infrastructure;

namespace Ofgem.API.GBI.UserManagement.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthorization();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSingleton<ITelemetryInitializer, CustomTelemetryInitialiser>();
            services.AddApplicationInsightsTelemetry(configuration.GetSection("APPINSIGHTS_CONNECTIONSTRING"));

            services.AddDbContext<UsersDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("UsersConnection"),
                                     provider =>
                                     {
                                         provider.MigrationsAssembly(typeof(UsersDbContext).Assembly.FullName);
                                         provider.EnableRetryOnFailure();
                                     });
            });

            services.AddTransient<IExternalUserRepository, ExternalUserRepository>();
			services.AddTransient<IExternalUserService, ExternalUserService>();

            services.AddTransient<ISupplierRepository, SupplierRepository>();
            services.AddTransient<ISupplierService, SupplierService>();

			services.AddScoped<IValidator<SaveExternalUserRequest>, SaveExternalUserRequestValidator>();
            services.AddScoped<IValidator<SyncExternalUserRequest>, SyncExternalUserRequestValidator>();

            return services;
        }
    }
}

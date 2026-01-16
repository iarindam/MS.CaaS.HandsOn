using System;
using System.Reflection;
using Catalog.API.Local.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Local.ExtensionMethods
{
    public static class CustomExtensionMethods
    {
        public static IServiceCollection AddCustomDBContext(this IServiceCollection services, 
            IConfiguration configuration, 
            IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                services.AddDbContext<CatalogContext>(options=>options.UseInMemoryDatabase("InMemoryCatalogDb"));
            }
            else if (environment.IsProduction())
            {
                services.AddEntityFrameworkNpgsql().
                    AddDbContext<CatalogContext>(options => {
                        options.UseNpgsql(configuration["ConnectionString"], npgsqlOptionsAction: npgsqlOptions =>
                        {
                            npgsqlOptions.MigrationsAssembly(typeof(CustomExtensionMethods).GetTypeInfo().Assembly.GetName().Name);
                            //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency
                            npgsqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
                        });
                    });
            }            

            return services;
        }
    }
}

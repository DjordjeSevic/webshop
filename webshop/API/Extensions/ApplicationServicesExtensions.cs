using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Contracts.Dtos.Errors;
using StackExchange.Redis;
using Microsoft.AspNetCore.Http.Connections;
using Persistence.Data.Triggers;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.Scan(selector => selector
                            .FromAssemblies(
                                Services.AssemblyReference.Assembly,
                                Persistence.AssemblyReference.Assembly)
                            .AddClasses(false)
                            .AsImplementedInterfaces()
                            .WithScopedLifetime()
                            );

            services.AddControllers().AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);
            services.AddSingleton<IConnectionMultiplexer>(c =>
            {
                var options = ConfigurationOptions.Parse(config.GetConnectionString("Redis"));
                 return ConnectionMultiplexer.Connect(options);
            });

            services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
                options.UseTriggers(triggerOptions =>
                {
                    triggerOptions.AddTrigger<DecreaseProductCount>();
                });
            });

            services.AddAutoMapper(Services.AssemblyReference.Assembly);
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins(config.GetValue<string>("ClientUrl"));
                });
            });

            return services;
        }
    }
}

using Dinner.Application.Common.Interface.Authentication;
using Dinner.Application.Common.Interface.Persistence;
using Dinner.Application.Common.Services.Authentication.Commands;
using Dinner.Application.Common.Services.Authentication.Queries;
using Dinner.Application.Services.Services;
using Dinner.Infrastructure.Authentication;
using Dinner.Infrastructure.Persistence;
using Dinner.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dinner.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        //Jwt configuration
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        //DateTime provider for Jwt Config
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IUserRepository, UserRepository>();
        //Auth services
        services.AddScoped<IAuthenticationCommandService, AuthenticationCommandService>();
        services.AddScoped<IAuthenticationQueryService, AuthenticationQueryService>();
        return services;
    }
}
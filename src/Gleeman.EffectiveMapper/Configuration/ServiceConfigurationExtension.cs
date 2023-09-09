using Gleeman.EffectiveMapper.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace Gleeman.EffectiveMapper.Configuration;

public static class ServiceConfigurationExtension
{
    public static IServiceCollection AddEffectiveMapper(this IServiceCollection services)
    {
        services.AddScoped<IEffectiveMapper,Mapper.EffectiveMapper>();
        return services;
    }
}

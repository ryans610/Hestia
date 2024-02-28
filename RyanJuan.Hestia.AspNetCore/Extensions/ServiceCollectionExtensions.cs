using JetBrains.Annotations;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RyanJuan.Hestia.AspNetCore.Extensions;

// ReSharper disable once InconsistentNaming
public static class ServiceCollectionExtensions
{
    [PublicAPI]
    public static IServiceCollection AddHostedServiceFromService<THostedService>(
        this IServiceCollection service)
        where THostedService : class, IHostedService
    {
        ArgumentNullException.ThrowIfNull(service);
        return service.AddHostedService(serviceProvider => serviceProvider.GetRequiredService<THostedService>());
    }

    [PublicAPI]
    public static IServiceCollection AddSingletonWithImplementation<TService, TImplementation>(
        this IServiceCollection service)
        where TService : class
        where TImplementation : class, TService
    {
        ArgumentNullException.ThrowIfNull(service);
        service.AddSingleton<TImplementation>();
        service.AddSingleton<TService, TImplementation>(serviceProvider =>
            serviceProvider.GetRequiredService<TImplementation>());
        return service;
    }

    [PublicAPI]
    public static IServiceCollection AddSingletonDecoratorStack<TService>(
        this IServiceCollection service,
        params Type[] types)
        where TService : class
    {
        ArgumentNullException.ThrowIfNull(service);
        ArgumentNullException.ThrowIfNull(types);
        var serviceType = typeof(TService);
        for (int i = 0; i < types.Length; i += 1)
        {
            ArgumentNullException.ThrowIfNull(types[i]);
            var type = types[i];
            service.AddSingleton(type, provider =>
            {
                var constructor = type.GetConstructors().Single();
                var parameters = constructor
                    .GetParameters()
                    .Select(x =>
                    {
                        var parameterType = x.ParameterType == serviceType
                            ? types[Array.IndexOf(types, type) - 1]
                            : x.ParameterType;
                        return provider.GetRequiredService(parameterType);
                    })
                    .ToArray();
                var instance = Activator.CreateInstance(type, parameters);
                return instance;
            });
        }

        service.AddSingleton(serviceType, provider => provider.GetRequiredService(types[^1]));

        return service;
    }
}

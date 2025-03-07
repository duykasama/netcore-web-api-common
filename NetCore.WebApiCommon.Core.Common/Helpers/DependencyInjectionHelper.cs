﻿using NetCore.WebApiCommon.Core.Common.Interfaces;

namespace NetCore.WebApiCommon.Core.Common.Helpers;

public static class DependencyInjectionHelper
{
    private static IDependencyProvider ServiceProvider { get; set; } = default!;

    public static void InitProvider(IDependencyProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }

    public static T ResolveService<T>() where T : notnull
    {
        ValidateProvider();
        return ServiceProvider.ResolveService<T>();
    }

    public static object ResolveService(Type serviceType)
    {
        ValidateProvider();
        return ServiceProvider.ResolveService(serviceType);
    }

    private static void ValidateProvider()
    {
        if (ServiceProvider == null)
        {
            throw new InvalidOperationException($"No service provider for {nameof(DependencyInjectionHelper)}");
        }
    }
}
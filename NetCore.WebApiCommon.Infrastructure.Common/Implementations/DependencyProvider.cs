using Autofac;
using NetCore.WebApiCommon.Core.Common.Interfaces;

namespace NetCore.WebApiCommon.Infrastructure.Common.Implementations;

public class DependencyProvider : GenericService, IDependencyProvider
{
    private readonly ILifetimeScope _scope;

    public DependencyProvider(ILifetimeScope scope) : base(scope)
    {
        _scope = scope;
    }

    public T ResolveService<T>()
    {
        return _scope.Resolve<T>();
    }

    public object ResolveService(Type serviceType)
    {
        return _scope.Resolve(serviceType);
    }
}
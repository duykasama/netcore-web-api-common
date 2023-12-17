using Autofac;
using NetCore.WebApiCommon.Core.Common.Interfaces;

namespace NetCore.WebApiCommon.Infrastructure.Implementations;

public class AutofacDependencyProvider : GenericService, IDependencyProvider
{
    private readonly ILifetimeScope _scope;

    public AutofacDependencyProvider(ILifetimeScope scope) : base(scope)
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
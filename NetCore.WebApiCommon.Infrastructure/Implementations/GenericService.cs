using Autofac;
using NetCore.WebApiCommon.Core.Common.Interfaces;

namespace NetCore.WebApiCommon.Infrastructure.Implementations;

public abstract class GenericService : IBaseService
{
    private readonly ILifetimeScope _scope;
    protected Guid ServiceId { get; set; }

    protected GenericService(ILifetimeScope scope)
    {
        _scope = scope;
        ServiceId = Guid.NewGuid();
    }

    protected T Resolve<T>() where T : notnull
    {
        return _scope.Resolve<T>();
    }
}
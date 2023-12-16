namespace NetCore.WebApiCommon.Core.Common.Interfaces;

public interface IDependencyProvider
{
    T ResolveService<T>();
    object ResolveService(Type serviceType);
}
namespace NetCore.WebApiCommon.Core.Common.Interfaces;

public interface IDependencyProvider
{
    T ResolveService<T>() where T : notnull;
    object ResolveService(Type serviceType);
}
using Microsoft.Extensions.Primitives;

namespace NetCore.WebApiCommon.Core.Common.Interfaces;

public interface ICorrelationIdGenerator
{
    void Set(StringValues correlationId);
    StringValues Get();
}
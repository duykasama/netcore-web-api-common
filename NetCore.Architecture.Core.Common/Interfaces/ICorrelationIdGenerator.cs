using Microsoft.Extensions.Primitives;

namespace NetCore.Architecture.Core.Common.Interfaces;

public interface ICorrelationIdGenerator
{
    void Set(StringValues correlationId);
    StringValues Get();
}
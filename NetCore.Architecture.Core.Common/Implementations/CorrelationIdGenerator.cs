using Microsoft.Extensions.Primitives;
using NetCore.Architecture.Core.Common.Interfaces;

namespace NetCore.Architecture.Core.Common.Implementations;

public class CorrelationIdGenerator : ICorrelationIdGenerator
{
    private string _correlationId = Guid.NewGuid().ToString();
    public void Set(StringValues correlationId)
    {
        _correlationId = correlationId!;
    }

    public StringValues Get() => _correlationId;
}
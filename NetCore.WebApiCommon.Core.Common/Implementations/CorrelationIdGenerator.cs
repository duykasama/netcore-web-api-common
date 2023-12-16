using Microsoft.Extensions.Primitives;
using NetCore.WebApiCommon.Core.Common.Interfaces;

namespace NetCore.WebApiCommon.Core.Common.Implementations;

public class CorrelationIdGenerator : ICorrelationIdGenerator
{
    private string _correlationId = Guid.NewGuid().ToString();
    public void Set(StringValues correlationId)
    {
        _correlationId = correlationId!;
    }

    public StringValues Get() => _correlationId;
}
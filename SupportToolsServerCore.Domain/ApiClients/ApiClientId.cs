using System;
using System.Collections.Generic;
using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.ApiClients;

public sealed class ApiClientId : ValueObject
{
    public ApiClientId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static ApiClientId CreateUnique()
    {
        return new ApiClientId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

using System;
using System.Collections.Generic;
using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.Runtimes;

public sealed class RuntimeId : ValueObject
{
    public RuntimeId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static RuntimeId CreateUnique()
    {
        return new RuntimeId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

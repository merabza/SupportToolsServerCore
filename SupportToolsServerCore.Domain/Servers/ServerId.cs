using System;
using System.Collections.Generic;
using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.Servers;

public sealed class ServerId : ValueObject
{
    public ServerId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static ServerId CreateUnique()
    {
        return new ServerId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

using System;
using System.Collections.Generic;
using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.DotnetTools;

public sealed class DotnetToolId : ValueObject
{
    public DotnetToolId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static DotnetToolId CreateUnique()
    {
        return new DotnetToolId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

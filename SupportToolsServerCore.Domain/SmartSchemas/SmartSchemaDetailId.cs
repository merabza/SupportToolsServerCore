using System;
using System.Collections.Generic;
using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.SmartSchemas;

public sealed class SmartSchemaDetailId : ValueObject
{
    public SmartSchemaDetailId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static SmartSchemaDetailId CreateUnique()
    {
        return new SmartSchemaDetailId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

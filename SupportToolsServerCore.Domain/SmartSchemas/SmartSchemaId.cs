using System;
using System.Collections.Generic;
using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.SmartSchemas;

public sealed class SmartSchemaId : ValueObject
{
    public SmartSchemaId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static SmartSchemaId CreateUnique()
    {
        return new SmartSchemaId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

using System;
using System.Collections.Generic;
using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.DatabaseServerConnections;

public sealed class DatabaseServerConnectionId : ValueObject
{
    public DatabaseServerConnectionId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static DatabaseServerConnectionId CreateUnique()
    {
        return new DatabaseServerConnectionId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

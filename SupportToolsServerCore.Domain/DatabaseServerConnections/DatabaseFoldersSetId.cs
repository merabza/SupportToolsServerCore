using System;
using System.Collections.Generic;
using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.DatabaseServerConnections;

public sealed class DatabaseFoldersSetId : ValueObject
{
    public DatabaseFoldersSetId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static DatabaseFoldersSetId CreateUnique()
    {
        return new DatabaseFoldersSetId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

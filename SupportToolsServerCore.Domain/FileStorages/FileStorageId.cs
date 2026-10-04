using System;
using System.Collections.Generic;
using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.FileStorages;

public sealed class FileStorageId : ValueObject
{
    public FileStorageId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static FileStorageId CreateUnique()
    {
        return new FileStorageId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

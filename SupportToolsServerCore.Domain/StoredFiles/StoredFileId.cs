using System;
using System.Collections.Generic;
using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.StoredFiles;

public sealed class StoredFileId : ValueObject
{
    public StoredFileId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static StoredFileId CreateUnique()
    {
        return new StoredFileId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

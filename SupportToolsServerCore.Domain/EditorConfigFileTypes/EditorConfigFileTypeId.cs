using System;
using System.Collections.Generic;
using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.EditorConfigFileTypes;

public class EditorConfigFileTypeId : ValueObject
{
    public EditorConfigFileTypeId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static EditorConfigFileTypeId CreateUnique()
    {
        return new EditorConfigFileTypeId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

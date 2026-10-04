using System;
using System.Collections.Generic;
using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.ReactAppTemplates;

public sealed class ReactAppTemplateId : ValueObject
{
    public ReactAppTemplateId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static ReactAppTemplateId CreateUnique()
    {
        return new ReactAppTemplateId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

using System;
using System.Collections.Generic;
using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.ProjectTemplates;

public sealed class ProjectTemplateId : ValueObject
{
    public ProjectTemplateId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static ProjectTemplateId CreateUnique()
    {
        return new ProjectTemplateId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

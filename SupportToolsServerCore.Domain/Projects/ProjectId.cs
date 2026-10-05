using System;
using System.Collections.Generic;
using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.Projects;

public sealed class ProjectId : ValueObject
{
    public ProjectId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static ProjectId CreateUnique()
    {
        return new ProjectId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

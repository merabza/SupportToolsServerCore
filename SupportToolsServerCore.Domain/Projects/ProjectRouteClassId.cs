using System;
using System.Collections.Generic;
using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.Projects;

public sealed class ProjectRouteClassId : ValueObject
{
    public ProjectRouteClassId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static ProjectRouteClassId CreateUnique()
    {
        return new ProjectRouteClassId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

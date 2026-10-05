using System;
using System.Collections.Generic;
using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.Projects;

public sealed class ProjectEndpointId : ValueObject
{
    public ProjectEndpointId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static ProjectEndpointId CreateUnique()
    {
        return new ProjectEndpointId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

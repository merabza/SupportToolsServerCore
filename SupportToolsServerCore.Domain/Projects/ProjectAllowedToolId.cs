using System;
using System.Collections.Generic;
using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.Projects;

public sealed class ProjectAllowedToolId : ValueObject
{
    public ProjectAllowedToolId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static ProjectAllowedToolId CreateUnique()
    {
        return new ProjectAllowedToolId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

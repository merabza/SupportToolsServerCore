using System;
using System.Collections.Generic;
using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.Projects;

public sealed class ProjectNpmPackageId : ValueObject
{
    public ProjectNpmPackageId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static ProjectNpmPackageId CreateUnique()
    {
        return new ProjectNpmPackageId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

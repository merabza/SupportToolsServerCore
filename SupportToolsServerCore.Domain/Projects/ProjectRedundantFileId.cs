using System;
using System.Collections.Generic;
using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.Projects;

public sealed class ProjectRedundantFileId : ValueObject
{
    public ProjectRedundantFileId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static ProjectRedundantFileId CreateUnique()
    {
        return new ProjectRedundantFileId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

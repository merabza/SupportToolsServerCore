using System;
using System.Collections.Generic;
using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.Projects;

public sealed class ProjectGitRepoId : ValueObject
{
    public ProjectGitRepoId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static ProjectGitRepoId CreateUnique()
    {
        return new ProjectGitRepoId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

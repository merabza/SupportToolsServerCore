using System;
using System.Collections.Generic;
using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.GitRepoProjects;

public sealed class GitRepoProjectDependencyId : ValueObject
{
    public GitRepoProjectDependencyId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static GitRepoProjectDependencyId CreateUnique()
    {
        return new GitRepoProjectDependencyId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

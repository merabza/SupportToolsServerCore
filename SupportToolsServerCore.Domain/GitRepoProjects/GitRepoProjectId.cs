using System;
using System.Collections.Generic;
using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.GitRepoProjects;

public sealed class GitRepoProjectId : ValueObject
{
    public GitRepoProjectId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static GitRepoProjectId CreateUnique()
    {
        return new GitRepoProjectId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

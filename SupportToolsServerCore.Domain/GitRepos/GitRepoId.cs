using System;
using System.Collections.Generic;
using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.GitRepos;

public class GitRepoId : ValueObject
{
    public GitRepoId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static GitRepoId CreateUnique()
    {
        return new GitRepoId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

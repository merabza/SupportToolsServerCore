using System;
using System.Collections.Generic;
using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.NpmPackages;

public sealed class NpmPackageId : ValueObject
{
    public NpmPackageId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static NpmPackageId CreateUnique()
    {
        return new NpmPackageId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

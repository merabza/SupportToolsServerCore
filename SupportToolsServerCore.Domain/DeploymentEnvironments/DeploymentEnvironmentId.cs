using System;
using System.Collections.Generic;
using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.DeploymentEnvironments;

public sealed class DeploymentEnvironmentId : ValueObject
{
    public DeploymentEnvironmentId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static DeploymentEnvironmentId CreateUnique()
    {
        return new DeploymentEnvironmentId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

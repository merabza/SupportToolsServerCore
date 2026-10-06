using System;
using System.Collections.Generic;
using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.Projects;

public sealed class ServerInfoId : ValueObject
{
    public ServerInfoId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static ServerInfoId CreateUnique()
    {
        return new ServerInfoId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

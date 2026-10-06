using System;
using System.Collections.Generic;
using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.Projects;

public sealed class ServerInfoAllowedToolId : ValueObject
{
    public ServerInfoAllowedToolId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static ServerInfoAllowedToolId CreateUnique()
    {
        return new ServerInfoAllowedToolId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

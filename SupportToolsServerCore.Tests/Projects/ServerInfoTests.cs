using System.Collections.Generic;
using System.Linq;
using SupportToolsServerCore.Domain.ApiClients;
using SupportToolsServerCore.Domain.DeploymentEnvironments;
using SupportToolsServerCore.Domain.Projects;
using SupportToolsServerCore.Domain.Servers;
using Xunit;

namespace SupportToolsServerCore.Tests.Projects;

public sealed class ServerInfoTests
{
    private static DatabaseParameters NewDatabaseParameters(string databaseName)
    {
        return new DatabaseParameters(null, "Full", "Default", databaseName, null, null, 120, false, null, null, null,
            null, null, null, null);
    }

    [Fact]
    public void Constructor_SetsTheValuesWithoutDatabaseParametersAndTools_AndRaisesNoDomainEvent()
    {
        var id = ServerInfoId.CreateUnique();
        ServerId serverId = ServerId.CreateUnique();
        DeploymentEnvironmentId environmentId = DeploymentEnvironmentId.CreateUnique();
        ApiClientId webAgentId = ApiClientId.CreateUnique();

        var serverInfo = new ServerInfo(id, serverId, environmentId, webAgentId, 5022, "v1",
            @"D:\1WorkSecurity\App\PAZISI\appsettings.json", @"D:\1WorkSecurity\App\PAZISI\appsettingsEncoded.json",
            "merab");

        Assert.Equal(id, serverInfo.Id);
        Assert.Equal(serverId, serverInfo.ServerId);
        Assert.Equal(environmentId, serverInfo.EnvironmentId);
        Assert.Equal(webAgentId, serverInfo.WebAgentForCheckId);
        Assert.Equal(5022, serverInfo.ServerSidePort);
        Assert.Equal("v1", serverInfo.ApiVersionId);
        Assert.Equal(@"D:\1WorkSecurity\App\PAZISI\appsettings.json", serverInfo.AppSettingsJsonSourceFileName);
        Assert.Equal(@"D:\1WorkSecurity\App\PAZISI\appsettingsEncoded.json",
            serverInfo.AppSettingsEncodedJsonFileName);
        Assert.Equal("merab", serverInfo.ServiceUserName);
        Assert.Null(serverInfo.CurrentDatabaseParameters);
        Assert.Null(serverInfo.NewDatabaseParameters);
        Assert.Empty(serverInfo.AllowedTools);
        Assert.Empty(serverInfo.DomainEvents);
    }

    [Fact]
    public void Create_ReturnsANewServerInfoWithItsPartsAndAUniqueId()
    {
        ServerId serverId = ServerId.CreateUnique();
        DeploymentEnvironmentId environmentId = DeploymentEnvironmentId.CreateUnique();
        DatabaseParameters current = NewDatabaseParameters("App");
        DatabaseParameters next = NewDatabaseParameters("AppNew");
        ServerInfoAllowedTool tool = ServerInfoAllowedTool.Create("ProgramUpdater");

        ServerInfo first = ServerInfo.Create(serverId, environmentId, null, 0, null, null, null, null, current, next,
            [tool]);
        ServerInfo second = ServerInfo.Create(serverId, environmentId, null, 0, null, null, null, null, null, null,
            []);

        Assert.NotEqual(first.Id, second.Id);
        Assert.Equal(serverId, first.ServerId);
        Assert.Equal(environmentId, first.EnvironmentId);
        Assert.Null(first.WebAgentForCheckId);
        Assert.Equal(0, first.ServerSidePort);
        Assert.Null(first.ApiVersionId);
        Assert.Null(first.AppSettingsJsonSourceFileName);
        Assert.Null(first.AppSettingsEncodedJsonFileName);
        Assert.Null(first.ServiceUserName);
        Assert.Same(current, first.CurrentDatabaseParameters);
        Assert.Same(next, first.NewDatabaseParameters);
        Assert.Equal([tool], first.AllowedTools);
        Assert.Null(second.CurrentDatabaseParameters);
        Assert.Null(second.NewDatabaseParameters);
        Assert.Empty(second.AllowedTools);
        Assert.Empty(first.DomainEvents);
    }

    //Adding to the given list later does not change the server info
    [Fact]
    public void Create_CopiesTheGivenTools()
    {
        List<ServerInfoAllowedTool> tools = [ServerInfoAllowedTool.Create("ServiceStarter")];

        ServerInfo serverInfo = ServerInfo.Create(ServerId.CreateUnique(), DeploymentEnvironmentId.CreateUnique(),
            null, 0, null, null, null, null, null, null, tools);
        tools.Add(ServerInfoAllowedTool.Create("ServiceStopper"));

        Assert.Equal(["ServiceStarter"], serverInfo.AllowedTools.Select(x => x.ToolName));
    }

    [Fact]
    public void AllowedTool_ConstructorAndCreate_SetTheValues()
    {
        var id = ServerInfoAllowedToolId.CreateUnique();

        var tool = new ServerInfoAllowedTool(id, "AppSettingsEncoder");

        Assert.Equal(id, tool.Id);
        Assert.Equal("AppSettingsEncoder", tool.ToolName);
        Assert.Equal("VersionChecker", ServerInfoAllowedTool.Create("VersionChecker").ToolName);
        Assert.NotEqual(ServerInfoAllowedTool.Create("a").Id, ServerInfoAllowedTool.Create("a").Id);
        Assert.Empty(tool.DomainEvents);
    }

    [Fact]
    public void Ids_WithTheSameValueAreEqual()
    {
        ServerInfoId id = ServerInfoId.CreateUnique();
        ServerInfoAllowedToolId toolId = ServerInfoAllowedToolId.CreateUnique();

        Assert.Equal(id, new ServerInfoId(id.Value));
        Assert.NotEqual(id, ServerInfoId.CreateUnique());
        Assert.Equal(toolId, new ServerInfoAllowedToolId(toolId.Value));
        Assert.NotEqual(toolId, ServerInfoAllowedToolId.CreateUnique());
    }
}

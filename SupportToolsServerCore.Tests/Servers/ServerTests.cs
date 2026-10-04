using SupportToolsServerCore.Domain.ApiClients;
using SupportToolsServerCore.Domain.Runtimes;
using SupportToolsServerCore.Domain.Servers;
using Xunit;

namespace SupportToolsServerCore.Tests.Servers;

public sealed class ServerTests
{
    [Fact]
    public void Constructor_SetsTheValues_AndRaisesNoDomainEvent()
    {
        var id = ServerId.CreateUnique();
        ApiClientId webAgentId = ApiClientId.CreateUnique();
        ApiClientId webAgentInstallerId = ApiClientId.CreateUnique();
        RuntimeId runtimeId = RuntimeId.CreateUnique();

        var server = new Server(id, "dl360", webAgentId, webAgentInstallerId, "deployer", "deployers", runtimeId,
            "/home/deployer/Download", "/opt/apps", 5);

        Assert.Equal(id, server.Id);
        Assert.Equal("dl360", server.Name);
        Assert.Equal(webAgentId, server.WebAgentId);
        Assert.Equal(webAgentInstallerId, server.WebAgentInstallerId);
        Assert.Equal("deployer", server.FilesUserName);
        Assert.Equal("deployers", server.FilesUsersGroupName);
        Assert.Equal(runtimeId, server.RuntimeId);
        Assert.Equal("/home/deployer/Download", server.ServerSideDownloadFolder);
        Assert.Equal("/opt/apps", server.ServerSideDeployFolder);
        Assert.Equal(5, server.Version);
        Assert.Empty(server.DomainEvents);
    }

    [Fact]
    public void Create_ReturnsANewServerWithAUniqueIdAndTheFirstVersion()
    {
        ApiClientId webAgentId = ApiClientId.CreateUnique();
        RuntimeId runtimeId = RuntimeId.CreateUnique();

        Server first = Server.Create("PAZISI", webAgentId, webAgentId, "Administrator", "Administrators", runtimeId,
            @"D:\Download", @"D:\Apps");
        Server second = Server.Create("PAZISI", null, null, null, null, null, null, null);

        Assert.NotEqual(first.Id, second.Id);
        Assert.Equal("PAZISI", first.Name);
        Assert.Equal(webAgentId, first.WebAgentId);
        Assert.Equal(webAgentId, first.WebAgentInstallerId);
        Assert.Equal("Administrator", first.FilesUserName);
        Assert.Equal("Administrators", first.FilesUsersGroupName);
        Assert.Equal(runtimeId, first.RuntimeId);
        Assert.Equal(@"D:\Download", first.ServerSideDownloadFolder);
        Assert.Equal(@"D:\Apps", first.ServerSideDeployFolder);
        Assert.Equal(1, first.Version);
        Assert.Empty(first.DomainEvents);
        Assert.Null(second.WebAgentId);
        Assert.Null(second.WebAgentInstallerId);
        Assert.Null(second.FilesUserName);
        Assert.Null(second.FilesUsersGroupName);
        Assert.Null(second.RuntimeId);
        Assert.Null(second.ServerSideDownloadFolder);
        Assert.Null(second.ServerSideDeployFolder);
    }

    [Fact]
    public void Update_ChangesTheValuesKeepingTheIdAndIncrementsTheVersion()
    {
        var id = ServerId.CreateUnique();
        var server = new Server(id, "Pazisi", null, null, null, null, null, null, null, 2);
        ApiClientId webAgentId = ApiClientId.CreateUnique();
        ApiClientId webAgentInstallerId = ApiClientId.CreateUnique();
        RuntimeId runtimeId = RuntimeId.CreateUnique();

        server.Update("PAZISI", webAgentId, webAgentInstallerId, "user-a", "group-a", runtimeId, @"D:\Download",
            @"D:\Apps");

        Assert.Equal(id, server.Id);
        Assert.Equal("PAZISI", server.Name);
        Assert.Equal(webAgentId, server.WebAgentId);
        Assert.Equal(webAgentInstallerId, server.WebAgentInstallerId);
        Assert.Equal("user-a", server.FilesUserName);
        Assert.Equal("group-a", server.FilesUsersGroupName);
        Assert.Equal(runtimeId, server.RuntimeId);
        Assert.Equal(@"D:\Download", server.ServerSideDownloadFolder);
        Assert.Equal(@"D:\Apps", server.ServerSideDeployFolder);
        Assert.Equal(3, server.Version);

        server.Update("PAZISI", null, null, null, null, null, null, null);
        Assert.Null(server.WebAgentId);
        Assert.Null(server.WebAgentInstallerId);
        Assert.Null(server.FilesUserName);
        Assert.Null(server.FilesUsersGroupName);
        Assert.Null(server.RuntimeId);
        Assert.Null(server.ServerSideDownloadFolder);
        Assert.Null(server.ServerSideDeployFolder);
        Assert.Equal(4, server.Version);
    }

    [Fact]
    public void Ids_WithTheSameValueAreEqual()
    {
        ServerId id = ServerId.CreateUnique();

        Assert.Equal(id, new ServerId(id.Value));
        Assert.NotEqual(id, ServerId.CreateUnique());
    }
}

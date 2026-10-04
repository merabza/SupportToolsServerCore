using System.Collections.Generic;
using System.Linq;
using SupportToolsServerCore.Domain.ApiClients;
using SupportToolsServerCore.Domain.DatabaseServerConnections;
using Xunit;

namespace SupportToolsServerCore.Tests.DatabaseServerConnections;

//The user names and the passwords are made up
public sealed class DatabaseServerConnectionTests
{
    private static DatabaseServerConnection NewConnection(DatabaseServerConnectionId id, int version)
    {
        return new DatabaseServerConnection(id, "Pc1.Sql", "SqlServer", null, null, "pc1", false, "user-a",
            "password-a", true, 30, false, version);
    }

    [Fact]
    public void Constructor_SetsTheValuesWithoutFoldersSets_AndRaisesNoDomainEvent()
    {
        var id = DatabaseServerConnectionId.CreateUnique();
        ApiClientId webAgentId = ApiClientId.CreateUnique();

        var connection = new DatabaseServerConnection(id, "Pc1.Sql", "WebAgent", webAgentId, "Main", "pc1", true,
            "made-up-user", "made-up-password", true, 30, true, 5);

        Assert.Equal(id, connection.Id);
        Assert.Equal("Pc1.Sql", connection.Name);
        Assert.Equal("WebAgent", connection.DatabaseServerProvider);
        Assert.Equal(webAgentId, connection.DbWebAgentId);
        Assert.Equal("Main", connection.RemoteDbConnectionName);
        Assert.Equal("pc1", connection.ServerAddress);
        Assert.True(connection.WindowsNtIntegratedSecurity);
        Assert.Equal("made-up-user", connection.ServerUser);
        Assert.Equal("made-up-password", connection.ServerPass);
        Assert.True(connection.TrustServerCertificate);
        Assert.Equal(30, connection.ConnectionTimeOut);
        Assert.True(connection.Encrypt);
        Assert.Empty(connection.DatabaseFoldersSets);
        Assert.Equal(5, connection.Version);
        Assert.Empty(connection.DomainEvents);
    }

    [Fact]
    public void Create_ReturnsANewConnectionWithItsFoldersSetsAUniqueIdAndTheFirstVersion()
    {
        DatabaseFoldersSet main = DatabaseFoldersSet.Create("Default", @"D:\Bak", @"D:\Data", @"D:\Log");

        DatabaseServerConnection first = DatabaseServerConnection.Create("Pc1.Sql", "SqlServer", null, null, "pc1",
            false, "made-up-user", "made-up-password", false, 15, false, [main]);
        DatabaseServerConnection second = DatabaseServerConnection.Create("Pc1.Sql", "SqlServer", null, null, "pc1",
            false, null, null, false, 15, false, []);

        Assert.NotEqual(first.Id, second.Id);
        Assert.Equal("Pc1.Sql", first.Name);
        Assert.Equal("SqlServer", first.DatabaseServerProvider);
        Assert.Null(first.DbWebAgentId);
        Assert.Null(first.RemoteDbConnectionName);
        Assert.Equal("pc1", first.ServerAddress);
        Assert.False(first.WindowsNtIntegratedSecurity);
        Assert.Equal("made-up-user", first.ServerUser);
        Assert.Equal("made-up-password", first.ServerPass);
        Assert.False(first.TrustServerCertificate);
        Assert.Equal(15, first.ConnectionTimeOut);
        Assert.False(first.Encrypt);
        Assert.Equal([main], first.DatabaseFoldersSets);
        Assert.Empty(second.DatabaseFoldersSets);
        Assert.Equal(1, first.Version);
        Assert.Empty(first.DomainEvents);
    }

    //An update replaces the whole aggregate: the folders sets of the update are the only ones left
    [Fact]
    public void Update_ReplacesTheValuesAndTheFoldersSetsKeepingTheIdAndIncrementsTheVersion()
    {
        var id = DatabaseServerConnectionId.CreateUnique();
        DatabaseServerConnection connection = NewConnection(id, 2);
        connection.Update("Pc1.Sql", "SqlServer", null, null, "pc1", false, "user-a", "password-a", true, 30, false,
            [DatabaseFoldersSet.Create("Default", "a", "b", "c"), DatabaseFoldersSet.Create("Second", "d", "e", "f")]);
        ApiClientId webAgentId = ApiClientId.CreateUnique();
        DatabaseFoldersSet other = DatabaseFoldersSet.Create("Other", @"E:\Bak", null, null);

        connection.Update("PC1.SQL", "WebAgent", webAgentId, "Main", "pc2", true, "user-b", "password-b", false, 60,
            true, [other]);

        Assert.Equal(id, connection.Id);
        Assert.Equal("PC1.SQL", connection.Name);
        Assert.Equal("WebAgent", connection.DatabaseServerProvider);
        Assert.Equal(webAgentId, connection.DbWebAgentId);
        Assert.Equal("Main", connection.RemoteDbConnectionName);
        Assert.Equal("pc2", connection.ServerAddress);
        Assert.True(connection.WindowsNtIntegratedSecurity);
        Assert.Equal("user-b", connection.ServerUser);
        Assert.Equal("password-b", connection.ServerPass);
        Assert.False(connection.TrustServerCertificate);
        Assert.Equal(60, connection.ConnectionTimeOut);
        Assert.True(connection.Encrypt);
        Assert.Equal([other], connection.DatabaseFoldersSets);
        Assert.Equal(4, connection.Version);

        connection.Update("PC1.SQL", "SqlServer", null, null, null, false, null, null, false, 0, false, []);
        Assert.Null(connection.DbWebAgentId);
        Assert.Null(connection.ServerAddress);
        Assert.Null(connection.ServerUser);
        Assert.Null(connection.ServerPass);
        Assert.Empty(connection.DatabaseFoldersSets);
        Assert.Equal(5, connection.Version);
    }

    //The new folders sets may come from the current ones, e.g. the same list again
    [Fact]
    public void Update_KeepsTheFoldersSets_WhenTheyAreGivenFromTheAggregateItself()
    {
        DatabaseServerConnection connection = DatabaseServerConnection.Create("Pc1.Sql", "SqlServer", null, null,
            "pc1", false, null, null, false, 15, false,
            [DatabaseFoldersSet.Create("Default", "a", "b", "c"), DatabaseFoldersSet.Create("Second", "d", "e", "f")]);
        List<DatabaseFoldersSet> foldersSets = [.. connection.DatabaseFoldersSets];

        connection.Update("Pc1.Sql", "SqlServer", null, null, "pc1", false, null, null, false, 15, false,
            connection.DatabaseFoldersSets);

        Assert.Equal(foldersSets, connection.DatabaseFoldersSets);
        Assert.Equal(2, connection.Version);
    }

    //Adding to the given list later does not change the connection
    [Fact]
    public void Create_CopiesTheGivenFoldersSets()
    {
        List<DatabaseFoldersSet> foldersSets = [DatabaseFoldersSet.Create("Default", "a", "b", "c")];

        DatabaseServerConnection connection = DatabaseServerConnection.Create("Pc1.Sql", "SqlServer", null, null,
            "pc1", false, null, null, false, 15, false, foldersSets);
        foldersSets.Add(DatabaseFoldersSet.Create("Second", "d", "e", "f"));

        Assert.Equal(["Default"], connection.DatabaseFoldersSets.Select(x => x.Name));
    }

    [Fact]
    public void FoldersSet_ConstructorAndCreate_SetTheValues()
    {
        var id = DatabaseFoldersSetId.CreateUnique();

        var foldersSet = new DatabaseFoldersSet(id, "Default", @"D:\Bak", @"D:\Data", @"D:\Log");
        DatabaseFoldersSet first = DatabaseFoldersSet.Create("Second", null, null, null);
        DatabaseFoldersSet second = DatabaseFoldersSet.Create("Second", null, null, null);

        Assert.Equal(id, foldersSet.Id);
        Assert.Equal("Default", foldersSet.Name);
        Assert.Equal(@"D:\Bak", foldersSet.Backup);
        Assert.Equal(@"D:\Data", foldersSet.Data);
        Assert.Equal(@"D:\Log", foldersSet.DataLog);
        Assert.Equal("Second", first.Name);
        Assert.Null(first.Backup);
        Assert.Null(first.Data);
        Assert.Null(first.DataLog);
        Assert.NotEqual(first.Id, second.Id);
        Assert.Empty(first.DomainEvents);
    }

    [Fact]
    public void Ids_WithTheSameValueAreEqual()
    {
        DatabaseServerConnectionId id = DatabaseServerConnectionId.CreateUnique();
        DatabaseFoldersSetId foldersSetId = DatabaseFoldersSetId.CreateUnique();

        Assert.Equal(id, new DatabaseServerConnectionId(id.Value));
        Assert.NotEqual(id, DatabaseServerConnectionId.CreateUnique());
        Assert.Equal(foldersSetId, new DatabaseFoldersSetId(foldersSetId.Value));
        Assert.NotEqual(foldersSetId, DatabaseFoldersSetId.CreateUnique());
    }
}

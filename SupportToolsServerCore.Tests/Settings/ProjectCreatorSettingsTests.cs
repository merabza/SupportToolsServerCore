using System;
using SupportToolsServerCore.Domain.DatabaseServerConnections;
using SupportToolsServerCore.Domain.DeploymentEnvironments;
using SupportToolsServerCore.Domain.FileStorages;
using SupportToolsServerCore.Domain.Servers;
using SupportToolsServerCore.Domain.Settings;
using SupportToolsServerCore.Domain.SmartSchemas;
using Xunit;

namespace SupportToolsServerCore.Tests.Settings;

public sealed class ProjectCreatorSettingsTests
{
    [Fact]
    public void Constructor_SetsTheValues_AndRaisesNoDomainEvent()
    {
        var id = new ProjectCreatorSettingsId(Guid.NewGuid());
        ServerId serverId = ServerId.CreateUnique();
        DeploymentEnvironmentId environmentId = DeploymentEnvironmentId.CreateUnique();
        DatabaseServerConnectionId connectionId = DatabaseServerConnectionId.CreateUnique();
        FileStorageId fileStorageId = FileStorageId.CreateUnique();
        SmartSchemaId smartSchemaId = SmartSchemaId.CreateUnique();

        var projectCreatorSettings = new ProjectCreatorSettings(id, 4, "FakeHost", @"D:\1WorkDotnet",
            @"D:\1WorkSecurity", serverId, environmentId, connectionId, fileStorageId, smartSchemaId, 5);

        Assert.Equal(id, projectCreatorSettings.Id);
        Assert.Equal(4, projectCreatorSettings.IndentSize);
        Assert.Equal("FakeHost", projectCreatorSettings.FakeHostProjectName);
        Assert.Equal(@"D:\1WorkDotnet", projectCreatorSettings.ProjectsFolderPathReal);
        Assert.Equal(@"D:\1WorkSecurity", projectCreatorSettings.SecretsFolderPathReal);
        Assert.Equal(serverId, projectCreatorSettings.ProductionServerId);
        Assert.Equal(environmentId, projectCreatorSettings.ProductionEnvironmentId);
        Assert.Equal(connectionId, projectCreatorSettings.DeveloperDbConnectionId);
        Assert.Equal(fileStorageId, projectCreatorSettings.DatabaseExchangeFileStorageId);
        Assert.Equal(smartSchemaId, projectCreatorSettings.UseSmartSchemaId);
        Assert.Equal(5, projectCreatorSettings.Version);
        Assert.Empty(projectCreatorSettings.DomainEvents);
    }

    [Fact]
    public void Create_ReturnsTheSingletonWithTheFirstVersion()
    {
        ServerId serverId = ServerId.CreateUnique();
        DeploymentEnvironmentId environmentId = DeploymentEnvironmentId.CreateUnique();
        DatabaseServerConnectionId connectionId = DatabaseServerConnectionId.CreateUnique();
        FileStorageId fileStorageId = FileStorageId.CreateUnique();
        SmartSchemaId smartSchemaId = SmartSchemaId.CreateUnique();

        ProjectCreatorSettings first = ProjectCreatorSettings.Create(4, "FakeHost", @"D:\1WorkDotnet",
            @"D:\1WorkSecurity", serverId, environmentId, connectionId, fileStorageId, smartSchemaId);
        ProjectCreatorSettings second =
            ProjectCreatorSettings.Create(0, null, null, null, null, null, null, null, null);

        Assert.Equal(ProjectCreatorSettingsId.Singleton, first.Id);
        Assert.Equal(first.Id, second.Id);
        Assert.Equal(4, first.IndentSize);
        Assert.Equal("FakeHost", first.FakeHostProjectName);
        Assert.Equal(@"D:\1WorkDotnet", first.ProjectsFolderPathReal);
        Assert.Equal(@"D:\1WorkSecurity", first.SecretsFolderPathReal);
        Assert.Equal(serverId, first.ProductionServerId);
        Assert.Equal(environmentId, first.ProductionEnvironmentId);
        Assert.Equal(connectionId, first.DeveloperDbConnectionId);
        Assert.Equal(fileStorageId, first.DatabaseExchangeFileStorageId);
        Assert.Equal(smartSchemaId, first.UseSmartSchemaId);
        Assert.Equal(1, first.Version);
        Assert.Empty(first.DomainEvents);
        Assert.Equal(0, second.IndentSize);
        Assert.Null(second.FakeHostProjectName);
        Assert.Null(second.ProductionServerId);
        Assert.Null(second.UseSmartSchemaId);
    }

    [Fact]
    public void Update_ReplacesTheValuesKeepingTheIdAndIncrementsTheVersion()
    {
        var projectCreatorSettings = new ProjectCreatorSettings(ProjectCreatorSettingsId.Singleton, 2, null, null,
            null, null, null, null, null, null, 2);
        ServerId serverId = ServerId.CreateUnique();
        DeploymentEnvironmentId environmentId = DeploymentEnvironmentId.CreateUnique();
        DatabaseServerConnectionId connectionId = DatabaseServerConnectionId.CreateUnique();
        FileStorageId fileStorageId = FileStorageId.CreateUnique();
        SmartSchemaId smartSchemaId = SmartSchemaId.CreateUnique();

        projectCreatorSettings.Update(4, "FakeHost", @"D:\1WorkDotnet", @"D:\1WorkSecurity", serverId, environmentId,
            connectionId, fileStorageId, smartSchemaId);

        Assert.Equal(ProjectCreatorSettingsId.Singleton, projectCreatorSettings.Id);
        Assert.Equal(4, projectCreatorSettings.IndentSize);
        Assert.Equal("FakeHost", projectCreatorSettings.FakeHostProjectName);
        Assert.Equal(@"D:\1WorkDotnet", projectCreatorSettings.ProjectsFolderPathReal);
        Assert.Equal(@"D:\1WorkSecurity", projectCreatorSettings.SecretsFolderPathReal);
        Assert.Equal(serverId, projectCreatorSettings.ProductionServerId);
        Assert.Equal(environmentId, projectCreatorSettings.ProductionEnvironmentId);
        Assert.Equal(connectionId, projectCreatorSettings.DeveloperDbConnectionId);
        Assert.Equal(fileStorageId, projectCreatorSettings.DatabaseExchangeFileStorageId);
        Assert.Equal(smartSchemaId, projectCreatorSettings.UseSmartSchemaId);
        Assert.Equal(3, projectCreatorSettings.Version);

        projectCreatorSettings.Update(0, null, null, null, null, null, null, null, null);
        Assert.Equal(0, projectCreatorSettings.IndentSize);
        Assert.Null(projectCreatorSettings.FakeHostProjectName);
        Assert.Null(projectCreatorSettings.ProjectsFolderPathReal);
        Assert.Null(projectCreatorSettings.SecretsFolderPathReal);
        Assert.Null(projectCreatorSettings.ProductionServerId);
        Assert.Null(projectCreatorSettings.ProductionEnvironmentId);
        Assert.Null(projectCreatorSettings.DeveloperDbConnectionId);
        Assert.Null(projectCreatorSettings.DatabaseExchangeFileStorageId);
        Assert.Null(projectCreatorSettings.UseSmartSchemaId);
        Assert.Equal(4, projectCreatorSettings.Version);
    }

    [Fact]
    public void SingletonId_IsFixed()
    {
        Assert.Equal(new Guid("00000000-0000-0000-0000-000000000001"), ProjectCreatorSettingsId.Singleton.Value);
        Assert.Same(ProjectCreatorSettingsId.Singleton, ProjectCreatorSettingsId.Singleton);
        Assert.Equal(ProjectCreatorSettingsId.Singleton,
            new ProjectCreatorSettingsId(ProjectCreatorSettingsId.Singleton.Value));
        Assert.NotEqual(ProjectCreatorSettingsId.Singleton, new ProjectCreatorSettingsId(Guid.NewGuid()));
    }
}

using SupportToolsServerCore.Domain.DeploymentEnvironments;
using Xunit;

namespace SupportToolsServerCore.Tests.DeploymentEnvironments;

public sealed class DeploymentEnvironmentTests
{
    [Fact]
    public void Constructor_SetsTheValues_AndRaisesNoDomainEvent()
    {
        var id = DeploymentEnvironmentId.CreateUnique();

        var environment = new DeploymentEnvironment(id, "Prod", "Production", 5);

        Assert.Equal(id, environment.Id);
        Assert.Equal("Prod", environment.Name);
        Assert.Equal("Production", environment.Description);
        Assert.Equal(5, environment.Version);
        Assert.Empty(environment.DomainEvents);
    }

    [Fact]
    public void Create_ReturnsANewEnvironmentWithAUniqueIdAndTheFirstVersion()
    {
        DeploymentEnvironment first = DeploymentEnvironment.Create("Prod", "Production");
        DeploymentEnvironment second = DeploymentEnvironment.Create("Prod", "Production");

        Assert.NotEqual(first.Id, second.Id);
        Assert.Equal("Prod", first.Name);
        Assert.Equal("Production", first.Description);
        Assert.Equal(1, first.Version);
        Assert.Empty(first.DomainEvents);
    }

    [Fact]
    public void Create_AcceptsAMissingDescription()
    {
        DeploymentEnvironment environment = DeploymentEnvironment.Create("Dev", null);

        Assert.Null(environment.Description);
    }

    [Fact]
    public void Update_ChangesTheValuesKeepingTheIdAndIncrementsTheVersion()
    {
        var id = DeploymentEnvironmentId.CreateUnique();
        var environment = new DeploymentEnvironment(id, "Prod", "Production", 2);

        environment.Update("prod", null);

        Assert.Equal(id, environment.Id);
        Assert.Equal("prod", environment.Name);
        Assert.Null(environment.Description);
        Assert.Equal(3, environment.Version);

        environment.Update("prod", "Production");
        Assert.Equal("Production", environment.Description);
        Assert.Equal(4, environment.Version);
    }

    [Fact]
    public void Ids_WithTheSameValueAreEqual()
    {
        DeploymentEnvironmentId id = DeploymentEnvironmentId.CreateUnique();

        Assert.Equal(id, new DeploymentEnvironmentId(id.Value));
        Assert.NotEqual(id, DeploymentEnvironmentId.CreateUnique());
    }
}

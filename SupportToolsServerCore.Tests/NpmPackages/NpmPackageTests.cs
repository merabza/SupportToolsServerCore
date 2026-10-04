using SupportToolsServerCore.Domain.NpmPackages;
using Xunit;

namespace SupportToolsServerCore.Tests.NpmPackages;

public sealed class NpmPackageTests
{
    [Fact]
    public void Constructor_SetsTheValues_AndRaisesNoDomainEvent()
    {
        var id = NpmPackageId.CreateUnique();

        var npmPackage = new NpmPackage(id, "@reduxjs/toolkit", "Redux toolset", 5);

        Assert.Equal(id, npmPackage.Id);
        Assert.Equal("@reduxjs/toolkit", npmPackage.Name);
        Assert.Equal("Redux toolset", npmPackage.Description);
        Assert.Equal(5, npmPackage.Version);
        Assert.Empty(npmPackage.DomainEvents);
    }

    [Fact]
    public void Create_ReturnsANewNpmPackageWithAUniqueIdAndTheFirstVersion()
    {
        NpmPackage first = NpmPackage.Create("react-router-dom", "Routing");
        NpmPackage second = NpmPackage.Create("react-router-dom", "Routing");

        Assert.NotEqual(first.Id, second.Id);
        Assert.Equal("react-router-dom", first.Name);
        Assert.Equal("Routing", first.Description);
        Assert.Equal(1, first.Version);
        Assert.Empty(first.DomainEvents);
    }

    [Fact]
    public void Create_AcceptsAMissingDescription()
    {
        NpmPackage npmPackage = NpmPackage.Create("yup", null);

        Assert.Null(npmPackage.Description);
    }

    [Fact]
    public void Update_ChangesTheValuesKeepingTheIdAndIncrementsTheVersion()
    {
        var id = NpmPackageId.CreateUnique();
        var npmPackage = new NpmPackage(id, "react-router-dom", "Routing", 2);

        npmPackage.Update("React-Router-Dom", null);

        Assert.Equal(id, npmPackage.Id);
        Assert.Equal("React-Router-Dom", npmPackage.Name);
        Assert.Null(npmPackage.Description);
        Assert.Equal(3, npmPackage.Version);

        npmPackage.Update("React-Router-Dom", "Declarative routing");
        Assert.Equal("Declarative routing", npmPackage.Description);
        Assert.Equal(4, npmPackage.Version);
    }

    [Fact]
    public void Ids_WithTheSameValueAreEqual()
    {
        NpmPackageId id = NpmPackageId.CreateUnique();

        Assert.Equal(id, new NpmPackageId(id.Value));
        Assert.NotEqual(id, NpmPackageId.CreateUnique());
    }
}

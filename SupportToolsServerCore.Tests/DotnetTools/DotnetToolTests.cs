using SupportToolsServerCore.Domain.DotnetTools;
using Xunit;

namespace SupportToolsServerCore.Tests.DotnetTools;

public sealed class DotnetToolTests
{
    [Fact]
    public void Constructor_SetsTheValues_AndRaisesNoDomainEvent()
    {
        var id = DotnetToolId.CreateUnique();

        var dotnetTool = new DotnetTool(id, "DotnetEf", "dotnet-ef", "9.0.8", "Entity Framework", 5);

        Assert.Equal(id, dotnetTool.Id);
        Assert.Equal("DotnetEf", dotnetTool.Name);
        Assert.Equal("dotnet-ef", dotnetTool.PackageId);
        Assert.Equal("9.0.8", dotnetTool.MaxVersion);
        Assert.Equal("Entity Framework", dotnetTool.Description);
        Assert.Equal(5, dotnetTool.Version);
        Assert.Empty(dotnetTool.DomainEvents);
    }

    [Fact]
    public void Create_ReturnsANewDotnetToolWithAUniqueIdAndTheFirstVersion()
    {
        DotnetTool first = DotnetTool.Create("Stryker", "dotnet-stryker", "5.0.0", "mutation testing");
        DotnetTool second = DotnetTool.Create("Stryker", "dotnet-stryker", "5.0.0", "mutation testing");

        Assert.NotEqual(first.Id, second.Id);
        Assert.Equal("Stryker", first.Name);
        Assert.Equal("dotnet-stryker", first.PackageId);
        Assert.Equal("5.0.0", first.MaxVersion);
        Assert.Equal("mutation testing", first.Description);
        Assert.Equal(1, first.Version);
        Assert.Empty(first.DomainEvents);
    }

    //Without a maximum version the latest one is installed
    [Fact]
    public void Create_AcceptsAMissingMaxVersionAndDescription()
    {
        DotnetTool dotnetTool = DotnetTool.Create("Stryker", "dotnet-stryker", null, null);

        Assert.Null(dotnetTool.MaxVersion);
        Assert.Null(dotnetTool.Description);
    }

    [Fact]
    public void Update_ChangesTheValuesKeepingTheIdAndIncrementsTheVersion()
    {
        var id = DotnetToolId.CreateUnique();
        var dotnetTool = new DotnetTool(id, "DotnetEf", "dotnet-ef", "9.0.8", "Entity Framework", 2);

        dotnetTool.Update("DOTNETEF", "Dotnet-Ef", null, null);

        Assert.Equal(id, dotnetTool.Id);
        Assert.Equal("DOTNETEF", dotnetTool.Name);
        Assert.Equal("Dotnet-Ef", dotnetTool.PackageId);
        Assert.Null(dotnetTool.MaxVersion);
        Assert.Null(dotnetTool.Description);
        Assert.Equal(3, dotnetTool.Version);

        dotnetTool.Update("DOTNETEF", "dotnet-ef", "10.0.12", "EF Core tools");
        Assert.Equal("dotnet-ef", dotnetTool.PackageId);
        Assert.Equal("10.0.12", dotnetTool.MaxVersion);
        Assert.Equal("EF Core tools", dotnetTool.Description);
        Assert.Equal(4, dotnetTool.Version);
    }

    [Fact]
    public void Ids_WithTheSameValueAreEqual()
    {
        DotnetToolId id = DotnetToolId.CreateUnique();

        Assert.Equal(id, new DotnetToolId(id.Value));
        Assert.NotEqual(id, DotnetToolId.CreateUnique());
    }
}

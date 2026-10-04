using SupportToolsServerCore.Domain.Runtimes;
using Xunit;

namespace SupportToolsServerCore.Tests.Runtimes;

public sealed class RuntimeTests
{
    [Fact]
    public void Constructor_SetsTheValues_AndRaisesNoDomainEvent()
    {
        var id = RuntimeId.CreateUnique();

        var runtime = new Runtime(id, "win-x64", "Windows x64", 5);

        Assert.Equal(id, runtime.Id);
        Assert.Equal("win-x64", runtime.Name);
        Assert.Equal("Windows x64", runtime.Description);
        Assert.Equal(5, runtime.Version);
        Assert.Empty(runtime.DomainEvents);
    }

    [Fact]
    public void Create_ReturnsANewRuntimeWithAUniqueIdAndTheFirstVersion()
    {
        Runtime first = Runtime.Create("win-x64", "Windows x64");
        Runtime second = Runtime.Create("win-x64", "Windows x64");

        Assert.NotEqual(first.Id, second.Id);
        Assert.Equal("win-x64", first.Name);
        Assert.Equal("Windows x64", first.Description);
        Assert.Equal(1, first.Version);
        Assert.Empty(first.DomainEvents);
    }

    [Fact]
    public void Create_AcceptsAMissingDescription()
    {
        Runtime runtime = Runtime.Create("linux-x64", null);

        Assert.Null(runtime.Description);
    }

    [Fact]
    public void Update_ChangesTheValuesKeepingTheIdAndIncrementsTheVersion()
    {
        var id = RuntimeId.CreateUnique();
        var runtime = new Runtime(id, "win-x64", "Windows x64", 2);

        runtime.Update("WIN-X64", null);

        Assert.Equal(id, runtime.Id);
        Assert.Equal("WIN-X64", runtime.Name);
        Assert.Null(runtime.Description);
        Assert.Equal(3, runtime.Version);

        runtime.Update("WIN-X64", "Windows 64 bit");
        Assert.Equal("Windows 64 bit", runtime.Description);
        Assert.Equal(4, runtime.Version);
    }

    [Fact]
    public void Ids_WithTheSameValueAreEqual()
    {
        RuntimeId id = RuntimeId.CreateUnique();

        Assert.Equal(id, new RuntimeId(id.Value));
        Assert.NotEqual(id, RuntimeId.CreateUnique());
    }
}

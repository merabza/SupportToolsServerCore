using SupportToolsServerCore.Domain.Primitives;
using Xunit;

namespace SupportToolsServerCore.Tests.Primitives;

public sealed class VersionedEntityTests
{
    [Fact]
    public void Initial_IsOne()
    {
        Assert.Equal(1, EntityVersion.Initial);
    }

    [Fact]
    public void Constructor_KeepsTheIdAndTheGivenVersion()
    {
        var entity = new TestVersionedEntity(7, 3);

        Assert.Equal(7, entity.Id);
        Assert.Equal(3, entity.Version);
    }

    [Fact]
    public void IncrementVersion_AddsOneOnEveryCall()
    {
        var entity = new TestVersionedEntity(7, EntityVersion.Initial);

        entity.Change();
        Assert.Equal(2, entity.Version);

        entity.Change();
        Assert.Equal(3, entity.Version);
    }

    private sealed class TestVersionedEntity : VersionedEntity<int>
    {
        public TestVersionedEntity(int id, int version) : base(id, version)
        {
        }

        public void Change()
        {
            IncrementVersion();
        }
    }
}

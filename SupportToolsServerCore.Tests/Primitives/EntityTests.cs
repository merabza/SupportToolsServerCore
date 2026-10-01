using SupportToolsServerCore.Domain.Primitives;
using SystemTools.SharedKernel;
using Xunit;

namespace SupportToolsServerCore.Tests.Primitives;

public sealed class EntityTests
{
    private static TestEntity Entity(int id)
    {
        return new TestEntity(id);
    }

    [Fact]
    public void Equals_ReturnsTrue_ForEntitiesWithTheSameId()
    {
        TestEntity entity = Entity(1);

        Assert.True(entity.Equals(Entity(1)));
        Assert.True(entity.Equals((object)Entity(1)));
        Assert.True(entity == Entity(1));
        Assert.False(entity != Entity(1));
    }

    [Fact]
    public void Equals_ReturnsFalse_ForEntitiesWithDifferentIds()
    {
        TestEntity entity = Entity(1);

        Assert.False(entity.Equals(Entity(2)));
        Assert.False(entity.Equals((object)Entity(2)));
        Assert.False(entity == Entity(2));
        Assert.True(entity != Entity(2));
    }

    [Fact]
    public void Equals_ReturnsFalse_ForNullAndOtherTypes()
    {
        TestEntity entity = Entity(1);

        Assert.False(entity.Equals(null));
        Assert.False(entity.Equals((object?)null));
        Assert.False(entity.Equals(1));
    }

    [Fact]
    public void GetHashCode_IsTheHashCodeOfTheId()
    {
        Assert.Equal(7.GetHashCode(), Entity(7).GetHashCode());
    }

    [Fact]
    public void Raise_CollectsTheDomainEventsInOrder()
    {
        TestEntity entity = Entity(1);
        var first = new TestDomainEvent(1);
        var second = new TestDomainEvent(2);

        entity.Raise(first);
        entity.Raise(second);

        Assert.Equal([first, second], entity.DomainEvents);
    }

    private sealed class TestEntity : Entity<int>
    {
        public TestEntity(int id) : base(id)
        {
        }
    }

    private sealed record TestDomainEvent(int Number) : IDomainEvent;
}

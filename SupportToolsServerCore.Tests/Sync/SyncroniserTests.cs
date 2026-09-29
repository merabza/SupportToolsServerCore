using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using SupportToolsServerCore.Domain.Primitives;
using SupportToolsServerCore.Domain.Sync;
using Xunit;

namespace SupportToolsServerCore.Tests.Sync;

public sealed class SyncroniserTests
{
    private readonly Mock<ICrudRepository<SyncTestEntity, int>> _repository = new();

    private void GivenStored(params SyncTestEntity[] entities)
    {
        _repository.Setup(r => r.GetAll(It.IsAny<CancellationToken>())).ReturnsAsync([.. entities]);
    }

    private Syncroniser<SyncTestEntity, int> CreateSyncroniser(params SyncTestEntity[] entities)
    {
        return new Syncroniser<SyncTestEntity, int>(_repository.Object, [.. entities]);
    }

    [Fact]
    public async Task DoSyncUp_WithoutMerge_DeletesOnlyTheStoredEntitiesMissingFromTheList()
    {
        var stored1 = new SyncTestEntity(1, "one");
        GivenStored(stored1, new SyncTestEntity(2, "two"), new SyncTestEntity(3, "three"));
        Syncroniser<SyncTestEntity, int> syncroniser =
            CreateSyncroniser(new SyncTestEntity(2, "two*"), new SyncTestEntity(3, "three*"));

        await syncroniser.DoSyncUp(false, CancellationToken.None);

        _repository.Verify(r => r.Delete(It.Is<SyncTestEntity>(e => ReferenceEquals(e, stored1))), Times.Once);
        _repository.Verify(r => r.Delete(It.IsAny<SyncTestEntity>()), Times.Once);
    }

    [Fact]
    public async Task DoSyncUp_WithoutMerge_AndAnEmptyList_DeletesEveryStoredEntity()
    {
        GivenStored(new SyncTestEntity(1, "one"), new SyncTestEntity(2, "two"));
        Syncroniser<SyncTestEntity, int> syncroniser = CreateSyncroniser();

        await syncroniser.DoSyncUp(false, CancellationToken.None);

        _repository.Verify(r => r.Delete(It.IsAny<SyncTestEntity>()), Times.Exactly(2));
        _repository.Verify(r => r.Add(It.IsAny<SyncTestEntity>()), Times.Never);
        _repository.Verify(r => r.Update(It.IsAny<SyncTestEntity>()), Times.Never);
    }

    [Fact]
    public async Task DoSyncUp_AddsOnlyTheEntitiesThatAreNotStored()
    {
        GivenStored(new SyncTestEntity(1, "one"), new SyncTestEntity(2, "two"));
        var added = new SyncTestEntity(3, "three");
        Syncroniser<SyncTestEntity, int> syncroniser = CreateSyncroniser(new SyncTestEntity(2, "two*"), added);

        await syncroniser.DoSyncUp(false, CancellationToken.None);

        _repository.Verify(r => r.Add(It.Is<SyncTestEntity>(e => ReferenceEquals(e, added))), Times.Once);
        _repository.Verify(r => r.Add(It.IsAny<SyncTestEntity>()), Times.Once);
    }

    [Fact]
    public async Task DoSyncUp_UpdatesTheStoredEntitiesWithTheInstancesOfTheList()
    {
        GivenStored(new SyncTestEntity(1, "one"), new SyncTestEntity(2, "two"));
        var changed = new SyncTestEntity(2, "two*");
        Syncroniser<SyncTestEntity, int> syncroniser = CreateSyncroniser(changed, new SyncTestEntity(3, "three"));

        await syncroniser.DoSyncUp(false, CancellationToken.None);

        _repository.Verify(r => r.Update(It.Is<SyncTestEntity>(e => ReferenceEquals(e, changed))), Times.Once);
        _repository.Verify(r => r.Update(It.IsAny<SyncTestEntity>()), Times.Once);
    }

    [Fact]
    public async Task DoSyncUp_WithMerge_KeepsTheStoredEntitiesMissingFromTheList()
    {
        GivenStored(new SyncTestEntity(1, "one"), new SyncTestEntity(2, "two"));
        Syncroniser<SyncTestEntity, int> syncroniser =
            CreateSyncroniser(new SyncTestEntity(2, "two*"), new SyncTestEntity(3, "three"));

        await syncroniser.DoSyncUp(true, CancellationToken.None);

        _repository.Verify(r => r.Delete(It.IsAny<SyncTestEntity>()), Times.Never);
        _repository.Verify(r => r.Add(It.IsAny<SyncTestEntity>()), Times.Once);
        _repository.Verify(r => r.Update(It.IsAny<SyncTestEntity>()), Times.Once);
    }

    [Fact]
    public async Task DoSyncUp_MergesByDefault()
    {
        GivenStored(new SyncTestEntity(1, "one"));
        Syncroniser<SyncTestEntity, int> syncroniser = CreateSyncroniser(new SyncTestEntity(2, "two"));

        await syncroniser.DoSyncUp();

        _repository.Verify(r => r.Delete(It.IsAny<SyncTestEntity>()), Times.Never);
        _repository.Verify(r => r.Add(It.IsAny<SyncTestEntity>()), Times.Once);
    }

    [Fact]
    public async Task DoSyncUp_WithAnEmptyStore_AddsEveryEntity()
    {
        GivenStored();
        Syncroniser<SyncTestEntity, int> syncroniser =
            CreateSyncroniser(new SyncTestEntity(1, "one"), new SyncTestEntity(2, "two"));

        await syncroniser.DoSyncUp(false, CancellationToken.None);

        _repository.Verify(r => r.Add(It.IsAny<SyncTestEntity>()), Times.Exactly(2));
        _repository.Verify(r => r.Update(It.IsAny<SyncTestEntity>()), Times.Never);
        _repository.Verify(r => r.Delete(It.IsAny<SyncTestEntity>()), Times.Never);
    }

    [Fact]
    public async Task DoSyncUp_PassesTheCancellationTokenToTheRepository()
    {
        GivenStored();
        using var cancellationTokenSource = new CancellationTokenSource();
        Syncroniser<SyncTestEntity, int> syncroniser = CreateSyncroniser();

        await syncroniser.DoSyncUp(true, cancellationTokenSource.Token);

        _repository.Verify(r => r.GetAll(cancellationTokenSource.Token), Times.Once);
    }
}

//Moq needs a public type for its proxy
public sealed class SyncTestEntity : Entity<int>
{
    public SyncTestEntity(int id, string value) : base(id)
    {
        Value = value;
    }

    public string Value { get; }
}

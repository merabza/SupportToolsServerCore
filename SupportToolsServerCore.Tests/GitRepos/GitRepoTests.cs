using SupportToolsServerCore.Domain.GitIgnoreFileTypes;
using SupportToolsServerCore.Domain.GitRepos;
using Xunit;

namespace SupportToolsServerCore.Tests.GitRepos;

public sealed class GitRepoTests
{
    private readonly GitIgnoreFileTypeId _cSharpId = GitIgnoreFileTypeId.CreateUnique();

    [Fact]
    public void Constructor_SetsTheValues_AndRaisesNoDomainEvent()
    {
        var id = GitRepoId.CreateUnique();

        var gitRepo = new GitRepo(id, "RepoA", "addressA", "FolderA", _cSharpId, 4);

        Assert.Equal(id, gitRepo.Id);
        Assert.Equal("RepoA", gitRepo.Name);
        Assert.Equal("addressA", gitRepo.Address);
        Assert.Equal("FolderA", gitRepo.FolderName);
        Assert.Equal(_cSharpId, gitRepo.GitIgnoreFileTypeId);
        Assert.Equal(4, gitRepo.Version);
        Assert.Empty(gitRepo.DomainEvents);
    }

    [Fact]
    public void Create_ReturnsANewGitWithAUniqueIdAndTheFirstVersion()
    {
        GitRepo first = GitRepo.Create("RepoA", "addressA", "FolderA", _cSharpId);
        GitRepo second = GitRepo.Create("RepoA", "addressA", "FolderA", _cSharpId);

        Assert.NotEqual(first.Id, second.Id);
        Assert.Equal("RepoA", first.Name);
        Assert.Equal("addressA", first.Address);
        Assert.Equal("FolderA", first.FolderName);
        Assert.Equal(_cSharpId, first.GitIgnoreFileTypeId);
        Assert.Equal(1, first.Version);
    }

    [Fact]
    public void Create_RaisesGitRepoAddedDomainEvent()
    {
        GitRepo gitRepo = GitRepo.Create("RepoA", "addressA", "FolderA", _cSharpId);

        Assert.Equal(new GitRepoAddedDomainEvent(gitRepo.Id, "RepoA", "addressA", "FolderA"),
            Assert.Single(gitRepo.DomainEvents));
    }

    [Fact]
    public void Update_ChangesTheValuesKeepingTheId()
    {
        var id = GitRepoId.CreateUnique();
        var gitRepo = new GitRepo(id, "RepoA", "addressA", "FolderA", _cSharpId, 1);
        var reactId = GitIgnoreFileTypeId.CreateUnique();

        gitRepo.Update("RepoB", "addressB", "FolderB", reactId);

        Assert.Equal(id, gitRepo.Id);
        Assert.Equal("RepoB", gitRepo.Name);
        Assert.Equal("addressB", gitRepo.Address);
        Assert.Equal("FolderB", gitRepo.FolderName);
        Assert.Equal(reactId, gitRepo.GitIgnoreFileTypeId);
    }

    [Fact]
    public void Update_IncrementsTheVersionOnEveryCall()
    {
        var gitRepo = new GitRepo(GitRepoId.CreateUnique(), "RepoA", "addressA", "FolderA", _cSharpId, 3);

        gitRepo.Update("RepoA", "addressA", "FolderA", _cSharpId);
        Assert.Equal(4, gitRepo.Version);

        gitRepo.Update("RepoA", "addressB", "FolderA", _cSharpId);
        Assert.Equal(5, gitRepo.Version);
    }

    [Fact]
    public void Update_RaisesGitRepoUpdatedDomainEventWithTheNewValues()
    {
        var gitRepo = new GitRepo(GitRepoId.CreateUnique(), "RepoA", "addressA", "FolderA", _cSharpId, 1);

        gitRepo.Update("RepoB", "addressB", "FolderB", _cSharpId);

        Assert.Equal(new GitRepoUpdatedDomainEvent(gitRepo.Id, "RepoB", "addressB", "FolderB"),
            Assert.Single(gitRepo.DomainEvents));
    }

    [Fact]
    public void ClearDomainEvents_RemovesTheRaisedEvents()
    {
        GitRepo gitRepo = GitRepo.Create("RepoA", "addressA", "FolderA", _cSharpId);

        gitRepo.ClearDomainEvents();

        Assert.Empty(gitRepo.DomainEvents);
    }
}

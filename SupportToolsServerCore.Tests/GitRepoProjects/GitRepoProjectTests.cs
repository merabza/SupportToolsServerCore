using System.Linq;
using SupportToolsServerCore.Domain.GitRepoProjects;
using SupportToolsServerCore.Domain.GitRepos;
using Xunit;

namespace SupportToolsServerCore.Tests.GitRepoProjects;

public sealed class GitRepoProjectTests
{
    [Fact]
    public void Constructor_SetsTheValuesWithoutDependencies_AndRaisesNoDomainEvent()
    {
        var id = GitRepoProjectId.CreateUnique();
        var gitRepoId = GitRepoId.CreateUnique();

        var gitRepoProject = new GitRepoProject(id, gitRepoId, @"RepoA\AppA", "AppA.csproj");

        Assert.Equal(id, gitRepoProject.Id);
        Assert.Equal(gitRepoId, gitRepoProject.GitRepoId);
        Assert.Equal(@"RepoA\AppA", gitRepoProject.ProjectRelativePath);
        Assert.Equal("AppA.csproj", gitRepoProject.ProjectFileName);
        Assert.Empty(gitRepoProject.Dependencies);
        Assert.Empty(gitRepoProject.DomainEvents);
    }

    //Every name becomes a dependency with an id of its own, in the order given
    [Fact]
    public void Create_ReturnsANewProjectWithADependencyForEveryNameAndUniqueIds()
    {
        var gitRepoId = GitRepoId.CreateUnique();

        GitRepoProject first = GitRepoProject.Create(gitRepoId, @"RepoA\AppA", "AppA.csproj", ["LibB", "LibA"]);
        GitRepoProject second = GitRepoProject.Create(gitRepoId, @"RepoA\LibA", "LibA.csproj", []);

        Assert.NotEqual(first.Id, second.Id);
        Assert.Equal(gitRepoId, first.GitRepoId);
        Assert.Equal(@"RepoA\AppA", first.ProjectRelativePath);
        Assert.Equal("AppA.csproj", first.ProjectFileName);
        Assert.Equal(["LibB", "LibA"], first.Dependencies.Select(x => x.ProjectName));
        Assert.NotEqual(first.Dependencies[0].Id, first.Dependencies[1].Id);
        Assert.Empty(second.Dependencies);
        Assert.Empty(first.DomainEvents);
    }

    [Fact]
    public void Dependency_Create_ReturnsADependencyWithAUniqueId()
    {
        GitRepoProjectDependency first = GitRepoProjectDependency.Create("LibA");
        GitRepoProjectDependency second = GitRepoProjectDependency.Create("LibA");

        Assert.Equal("LibA", first.ProjectName);
        Assert.NotEqual(first.Id, second.Id);
    }

    [Fact]
    public void Dependency_Constructor_SetsTheValues()
    {
        var id = GitRepoProjectDependencyId.CreateUnique();

        var dependency = new GitRepoProjectDependency(id, "LibA");

        Assert.Equal(id, dependency.Id);
        Assert.Equal("LibA", dependency.ProjectName);
    }

    [Fact]
    public void Ids_AreEqualByTheirValue()
    {
        var projectId = GitRepoProjectId.CreateUnique();
        var dependencyId = GitRepoProjectDependencyId.CreateUnique();

        Assert.Equal(projectId, new GitRepoProjectId(projectId.Value));
        Assert.Equal(dependencyId, new GitRepoProjectDependencyId(dependencyId.Value));
        Assert.NotEqual(projectId.Value, GitRepoProjectId.CreateUnique().Value);
    }
}

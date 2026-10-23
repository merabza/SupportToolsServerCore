using SupportToolsServerCore.Domain.GitRepos;
using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.Projects;

//პროექტის git რეპოზიტორია (GitRepo, კონტრაქტში სახელით). ერთი git პროექტში ერთხელ გვხვდება. აგრეგატის შვილია,
//ამიტომ საკუთარი ვერსია არ აქვს (README G7)
public sealed class ProjectGitRepo : Entity<ProjectGitRepoId>
{
    public ProjectGitRepo(ProjectGitRepoId id, GitRepoId gitRepoId) : base(id)
    {
        GitRepoId = gitRepoId;
    }

    public GitRepoId GitRepoId { get; private set; }

    public static ProjectGitRepo Create(GitRepoId gitRepoId)
    {
        return new ProjectGitRepo(ProjectGitRepoId.CreateUnique(), gitRepoId);
    }
}

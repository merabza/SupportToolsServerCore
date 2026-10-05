using SupportToolsServerCore.Domain.GitRepos;
using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.Projects;

//პროექტის git რეპოზიტორია (GitRepo, კონტრაქტში სახელით) და მისი როლი. ერთი git ერთ როლში პროექტში ერთხელ გვხვდება,
//ორივე როლში კი შეიძლება. აგრეგატის შვილია, ამიტომ საკუთარი ვერსია არ აქვს (README G7)
public sealed class ProjectGitRepo : Entity<ProjectGitRepoId>
{
    //როლის სახელის სვეტი (Main, ScaffoldSeed)
    public const int KindMaxLength = 20;

    public ProjectGitRepo(ProjectGitRepoId id, GitRepoId gitRepoId, EProjectGitRepoKind kind) : base(id)
    {
        GitRepoId = gitRepoId;
        Kind = kind;
    }

    public GitRepoId GitRepoId { get; private set; }
    public EProjectGitRepoKind Kind { get; private set; }

    public static ProjectGitRepo Create(GitRepoId gitRepoId, EProjectGitRepoKind kind)
    {
        return new ProjectGitRepo(ProjectGitRepoId.CreateUnique(), gitRepoId, kind);
    }
}

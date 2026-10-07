using System.Collections.Generic;
using System.Linq;
using SupportToolsServerCore.Domain.GitRepos;
using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.GitRepoProjects;

//git რეპოზიტორიის ერთი პროექტის ფაილი (csproj ან esproj), SupportTools-ის GitProjects-ის ჩანაწერის ანალოგი. სერვერი მას
//თავისი კლონის სკანირებით ითვლის (B9). ProjectRelativePath Gits ფოლდერის მიმართ შეფარდებითი ფოლდერია კლიენტის ფორმით:
//\-ით და რეპოზიტორიის ფოლდერის სახელით იწყება. ეს გამოთვლადი მონაცემია და არა რეესტრის ჩანაწერი: ვერსია არ აქვს,
//GitRepo-ს აგრეგატში არ შედის და სკანირება რეპოზიტორიის ყველა პროექტს ერთად ანაცვლებს
public sealed class GitRepoProject : Entity<GitRepoProjectId>
{
    public const int ProjectRelativePathMaxLength = 260;
    public const int ProjectFileNameMaxLength = 128;

    private readonly List<GitRepoProjectDependency> _dependencies = [];

    public GitRepoProject(GitRepoProjectId id, GitRepoId gitRepoId, string projectRelativePath,
        string projectFileName) : base(id)
    {
        GitRepoId = gitRepoId;
        ProjectRelativePath = projectRelativePath;
        ProjectFileName = projectFileName;
    }

    public GitRepoId GitRepoId { get; private set; }
    public string ProjectRelativePath { get; private set; }
    public string ProjectFileName { get; private set; }
    public IReadOnlyList<GitRepoProjectDependency> Dependencies => _dependencies;

    public static GitRepoProject Create(GitRepoId gitRepoId, string projectRelativePath, string projectFileName,
        IEnumerable<string> dependsOnProjectNames)
    {
        var gitRepoProject = new GitRepoProject(GitRepoProjectId.CreateUnique(), gitRepoId, projectRelativePath,
            projectFileName);
        gitRepoProject._dependencies.AddRange(dependsOnProjectNames.Select(GitRepoProjectDependency.Create));
        return gitRepoProject;
    }
}

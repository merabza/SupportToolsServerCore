using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.GitRepoProjects;

//პროექტის დამოკიდებულება: ProjectReference-ის ფაილის სახელი გაფართოების გარეშე, როგორც კლიენტის
//GitProjectDataModel.DependsOnProjectNames-ში. დამოკიდებულება პროექტის შვილია და მასთან ერთად იშლება
public sealed class GitRepoProjectDependency : Entity<GitRepoProjectDependencyId>
{
    public const int ProjectNameMaxLength = 128;

    public GitRepoProjectDependency(GitRepoProjectDependencyId id, string projectName) : base(id)
    {
        ProjectName = projectName;
    }

    public string ProjectName { get; private set; }

    public static GitRepoProjectDependency Create(string projectName)
    {
        return new GitRepoProjectDependency(GitRepoProjectDependencyId.CreateUnique(), projectName);
    }
}

namespace SupportToolsServerCore.Domain.Projects;

//პროექტის git-ის როლი: Main — პროექტის git-ები (კლიენტის GitProjectNames), ScaffoldSeed — scaffold seeder-ის
//git-ები (ScaffoldSeederGitProjectNames). ბაზაში სახელით ინახება
public enum EProjectGitRepoKind
{
    Main,
    ScaffoldSeed
}

using Microsoft.EntityFrameworkCore;
using SupportToolsServerCore.Domain.DeploymentEnvironments;
using SupportToolsServerCore.Domain.DotnetTools;
using SupportToolsServerCore.Domain.EditorConfigFileTypes;
using SupportToolsServerCore.Domain.GitIgnoreFileTypes;
using SupportToolsServerCore.Domain.GitRepos;
using SupportToolsServerCore.Domain.NpmPackages;
using SupportToolsServerCore.Domain.ReactAppTemplates;
using SupportToolsServerCore.Domain.Runtimes;

namespace SupportToolsServerCore.Application.Abstractions;

public interface ISupportToolsServerDbContext
{
    DbSet<DotnetTool> DotnetTools { get; set; }
    DbSet<EditorConfigFileType> EditorConfigFileTypes { get; set; }
    DbSet<DeploymentEnvironment> Environments { get; set; }
    DbSet<GitIgnoreFileType> GitIgnoreFileTypes { get; set; }
    DbSet<GitRepo> GitRepos { get; set; }
    DbSet<NpmPackage> NpmPackages { get; set; }
    DbSet<ReactAppTemplate> ReactAppTemplates { get; set; }
    DbSet<Runtime> Runtimes { get; set; }
}

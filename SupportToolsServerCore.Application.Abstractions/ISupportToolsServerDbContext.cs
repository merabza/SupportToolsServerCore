using Microsoft.EntityFrameworkCore;
using SupportToolsServerCore.Domain.ApiClients;
using SupportToolsServerCore.Domain.DatabaseServerConnections;
using SupportToolsServerCore.Domain.DeploymentEnvironments;
using SupportToolsServerCore.Domain.DotnetTools;
using SupportToolsServerCore.Domain.EditorConfigFileTypes;
using SupportToolsServerCore.Domain.FileStorages;
using SupportToolsServerCore.Domain.GitIgnoreFileTypes;
using SupportToolsServerCore.Domain.GitRepos;
using SupportToolsServerCore.Domain.NpmPackages;
using SupportToolsServerCore.Domain.Projects;
using SupportToolsServerCore.Domain.ProjectTemplates;
using SupportToolsServerCore.Domain.ReactAppTemplates;
using SupportToolsServerCore.Domain.Runtimes;
using SupportToolsServerCore.Domain.Servers;
using SupportToolsServerCore.Domain.Settings;
using SupportToolsServerCore.Domain.SmartSchemas;
using SupportToolsServerCore.Domain.StoredFiles;

namespace SupportToolsServerCore.Application.Abstractions;

public interface ISupportToolsServerDbContext
{
    DbSet<ApiClient> ApiClients { get; set; }
    DbSet<DatabaseServerConnection> DatabaseServerConnections { get; set; }
    DbSet<DotnetTool> DotnetTools { get; set; }
    DbSet<EditorConfigFileType> EditorConfigFileTypes { get; set; }
    DbSet<DeploymentEnvironment> Environments { get; set; }
    DbSet<FileStorage> FileStorages { get; set; }
    DbSet<GitIgnoreFileType> GitIgnoreFileTypes { get; set; }
    DbSet<GitRepo> GitRepos { get; set; }
    DbSet<GlobalSettings> GlobalSettings { get; set; }
    DbSet<NpmPackage> NpmPackages { get; set; }
    DbSet<ProjectCreatorSettings> ProjectCreatorSettings { get; set; }
    DbSet<Project> Projects { get; set; }
    DbSet<ProjectTemplate> ProjectTemplates { get; set; }
    DbSet<ReactAppTemplate> ReactAppTemplates { get; set; }
    DbSet<Runtime> Runtimes { get; set; }
    DbSet<Server> Servers { get; set; }
    DbSet<SmartSchema> SmartSchemas { get; set; }
    DbSet<StoredFile> StoredFiles { get; set; }
}

using System.Collections.Generic;
using System.Linq;
using SupportToolsServerCore.Domain.DatabaseServerConnections;
using SupportToolsServerCore.Domain.EditorConfigFileTypes;
using SupportToolsServerCore.Domain.FileStorages;
using SupportToolsServerCore.Domain.GitRepos;
using SupportToolsServerCore.Domain.NpmPackages;
using SupportToolsServerCore.Domain.Projects;
using SupportToolsServerCore.Domain.SmartSchemas;
using Xunit;

namespace SupportToolsServerCore.Tests.Projects;

//Every text field gets its own value ("<field><suffix>"), so that a swapped argument fails. The key part is made up
public sealed class ProjectTests
{
    private static readonly EditorConfigFileTypeId EditorConfigId = EditorConfigFileTypeId.CreateUnique();

    private static Project NewProject(ProjectId id, string s, int version,
        EditorConfigFileTypeId? editorConfigFileTypeId = null)
    {
        return new Project(id, "Name" + s, "Type" + s, "Group" + s, "Description" + s, 3, 7, true,
            editorConfigFileTypeId, "Main" + s, "ApiContracts" + s, "Spa" + s, "DbContext" + s, "Prefix" + s,
            "ScaffoldSeeder" + s, "DbContextProject" + s, "NewDataSeeding" + s, "ArchiveMask" + s,
            "ArchiveExtension" + s, "ParametersMask" + s, "ParametersExtension" + s, "Folder" + s, "Solution" + s,
            "Security" + s, "MigrationStartup" + s, "Migration" + s, "SeederRules" + s, "OldDataConvertor" + s,
            "Seed" + s, "SeedParameters" + s, "ExcludesRules" + s, "AppSetEnKeys" + s, "MigrationSql" + s,
            "PrepareProdCopy" + s, "PrepareProdCopyParameters" + s, "PairedDbObjects" + s, "made-up-key" + s,
            version);
    }

    private static void UpdateProject(Project project, string s, DatabaseParameters? dev,
        DatabaseParameters? prodCopy, IEnumerable<ProjectGitRepo> gitRepos, IEnumerable<ProjectNpmPackage> npmPackages,
        IEnumerable<ProjectRedundantFile> redundantFiles, IEnumerable<ProjectAllowedTool> allowedTools,
        IEnumerable<ProjectEndpoint> endpoints, IEnumerable<ProjectRouteClass> routeClasses)
    {
        project.Update("Name" + s, "Type" + s, "Group" + s, "Description" + s, 3, 7, true, EditorConfigId,
            "Main" + s, "ApiContracts" + s, "Spa" + s, "DbContext" + s, "Prefix" + s, "ScaffoldSeeder" + s,
            "DbContextProject" + s, "NewDataSeeding" + s, "ArchiveMask" + s, "ArchiveExtension" + s,
            "ParametersMask" + s, "ParametersExtension" + s, "Folder" + s, "Solution" + s, "Security" + s,
            "MigrationStartup" + s, "Migration" + s, "SeederRules" + s, "OldDataConvertor" + s, "Seed" + s,
            "SeedParameters" + s, "ExcludesRules" + s, "AppSetEnKeys" + s, "MigrationSql" + s, "PrepareProdCopy" + s,
            "PrepareProdCopyParameters" + s, "PairedDbObjects" + s, "made-up-key" + s, dev, prodCopy, gitRepos,
            npmPackages, redundantFiles, allowedTools, endpoints, routeClasses);
    }

    private static void AssertFields(Project project, string s)
    {
        Assert.Equal("Name" + s, project.Name);
        Assert.Equal("Type" + s, project.ProjectType);
        Assert.Equal("Group" + s, project.ProjectGroupName);
        Assert.Equal("Description" + s, project.ProjectDescription);
        Assert.Equal(3, project.MajorVersion);
        Assert.Equal(7, project.MinorVersion);
        Assert.True(project.UseAlternativeWebAgent);
        Assert.Equal("Main" + s, project.MainProjectName);
        Assert.Equal("ApiContracts" + s, project.ApiContractsProjectName);
        Assert.Equal("Spa" + s, project.SpaProjectName);
        Assert.Equal("DbContext" + s, project.DbContextName);
        Assert.Equal("Prefix" + s, project.ProjectShortPrefix);
        Assert.Equal("ScaffoldSeeder" + s, project.ScaffoldSeederProjectName);
        Assert.Equal("DbContextProject" + s, project.DbContextProjectName);
        Assert.Equal("NewDataSeeding" + s, project.NewDataSeedingClassLibProjectName);
        Assert.Equal("ArchiveMask" + s, project.ProgramArchiveDateMask);
        Assert.Equal("ArchiveExtension" + s, project.ProgramArchiveExtension);
        Assert.Equal("ParametersMask" + s, project.ParametersFileDateMask);
        Assert.Equal("ParametersExtension" + s, project.ParametersFileExtension);
        Assert.Equal("Folder" + s, project.ProjectFolderName);
        Assert.Equal("Solution" + s, project.SolutionFileName);
        Assert.Equal("Security" + s, project.ProjectSecurityFolderPath);
        Assert.Equal("MigrationStartup" + s, project.MigrationStartupProjectFilePath);
        Assert.Equal("Migration" + s, project.MigrationProjectFilePath);
        Assert.Equal("SeederRules" + s, project.DataSeederRulesByTableStartupProjectFilePath);
        Assert.Equal("OldDataConvertor" + s, project.OldDataConvertorForDataSeeder);
        Assert.Equal("Seed" + s, project.SeedProjectFilePath);
        Assert.Equal("SeedParameters" + s, project.SeedProjectParametersFilePath);
        Assert.Equal("ExcludesRules" + s, project.ExcludesRulesParametersFilePath);
        Assert.Equal("AppSetEnKeys" + s, project.AppSetEnKeysJsonFileName);
        Assert.Equal("MigrationSql" + s, project.MigrationSqlFilesFolder);
        Assert.Equal("PrepareProdCopy" + s, project.PrepareProdCopyDatabaseProjectFilePath);
        Assert.Equal("PrepareProdCopyParameters" + s, project.PrepareProdCopyDatabaseProjectParametersFilePath);
        Assert.Equal("PairedDbObjects" + s, project.PairedDbObjectsResultFileName);
        Assert.Equal("made-up-key" + s, project.KeyGuidPart);
    }

    private static DatabaseParameters NewDatabaseParameters(string databaseName)
    {
        return new DatabaseParameters(DatabaseServerConnectionId.CreateUnique(), "Full", "Default", databaseName,
            SmartSchemaId.CreateUnique(), FileStorageId.CreateUnique(), 120, true, "dev", "yyyyMMddHHmmss", ".bak",
            "_FullDb_", true, false, "Diff");
    }

    [Fact]
    public void Constructor_SetsTheValuesWithoutDatabaseParametersAndChildren_AndRaisesNoDomainEvent()
    {
        var id = ProjectId.CreateUnique();

        Project project = NewProject(id, "1", 5, EditorConfigId);

        Assert.Equal(id, project.Id);
        AssertFields(project, "1");
        Assert.Equal(EditorConfigId, project.EditorConfigFileTypeId);
        Assert.Null(project.DevDatabaseParameters);
        Assert.Null(project.ProdCopyDatabaseParameters);
        Assert.Empty(project.GitRepos);
        Assert.Empty(project.NpmPackages);
        Assert.Empty(project.RedundantFiles);
        Assert.Empty(project.AllowedTools);
        Assert.Empty(project.Endpoints);
        Assert.Empty(project.RouteClasses);
        Assert.Equal(5, project.Version);
        Assert.Empty(project.DomainEvents);
    }

    [Fact]
    public void Create_ReturnsANewProjectWithItsPartsAUniqueIdAndTheFirstVersion()
    {
        DatabaseParameters dev = NewDatabaseParameters("AppDev");
        ProjectGitRepo gitRepo = ProjectGitRepo.Create(GitRepoId.CreateUnique(), EProjectGitRepoKind.Main);
        ProjectNpmPackage npmPackage = ProjectNpmPackage.Create(NpmPackageId.CreateUnique());
        ProjectRedundantFile redundantFile = ProjectRedundantFile.Create("*.pdb");
        ProjectAllowedTool allowedTool = ProjectAllowedTool.Create("SeedData");
        ProjectEndpoint endpoint = ProjectEndpoint.Create("Upload", "Upload", "/upload", true, "Post", "Command",
            null, false);
        ProjectRouteClass routeClass = ProjectRouteClass.Create("Git", "api", "v1", "/git");

        Project first = Project.Create("App", "IsService", null, null, 1, 0, false, null, null, null, null, null,
            null, null, null, null, null, null, null, null, @"D:\1WorkDotnet\App", null, null, null, null, null,
            null, null, null, null, null, null, null, null, null, null, dev, null, [gitRepo], [npmPackage],
            [redundantFile], [allowedTool], [endpoint], [routeClass]);
        Project second = Project.Create("App", "IsService", null, null, 1, 0, false, null, null, null, null, null,
            null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
            null, null, null, null, null, null, null, null, null, [], [], [], [], [], []);

        Assert.NotEqual(first.Id, second.Id);
        Assert.Equal("App", first.Name);
        Assert.Equal("IsService", first.ProjectType);
        Assert.Equal(1, first.MajorVersion);
        Assert.False(first.UseAlternativeWebAgent);
        Assert.Null(first.EditorConfigFileTypeId);
        Assert.Equal(@"D:\1WorkDotnet\App", first.ProjectFolderName);
        Assert.Null(first.KeyGuidPart);
        Assert.Same(dev, first.DevDatabaseParameters);
        Assert.Null(first.ProdCopyDatabaseParameters);
        Assert.Equal([gitRepo], first.GitRepos);
        Assert.Equal([npmPackage], first.NpmPackages);
        Assert.Equal([redundantFile], first.RedundantFiles);
        Assert.Equal([allowedTool], first.AllowedTools);
        Assert.Equal([endpoint], first.Endpoints);
        Assert.Equal([routeClass], first.RouteClasses);
        Assert.Empty(second.GitRepos);
        Assert.Equal(1, first.Version);
        Assert.Empty(first.DomainEvents);
    }

    //An update replaces the whole aggregate: the parts of the update are the only ones left
    [Fact]
    public void Update_ReplacesTheValuesTheDatabaseParametersAndTheChildrenKeepingTheIdAndIncrementsTheVersion()
    {
        var id = ProjectId.CreateUnique();
        Project project = NewProject(id, "1", 2);
        UpdateProject(project, "1", NewDatabaseParameters("Old"), null,
            [ProjectGitRepo.Create(GitRepoId.CreateUnique(), EProjectGitRepoKind.Main)],
            [ProjectNpmPackage.Create(NpmPackageId.CreateUnique())], [ProjectRedundantFile.Create("*.pdb")],
            [ProjectAllowedTool.Create("SeedData")],
            [ProjectEndpoint.Create("Old", null, null, false, "Get", "Query", null, false)],
            [ProjectRouteClass.Create("Old", "api", "v1", "/old")]);
        DatabaseParameters dev = NewDatabaseParameters("AppDev");
        DatabaseParameters prodCopy = NewDatabaseParameters("AppProdCopy");
        ProjectGitRepo gitRepo = ProjectGitRepo.Create(GitRepoId.CreateUnique(), EProjectGitRepoKind.ScaffoldSeed);
        ProjectNpmPackage npmPackage = ProjectNpmPackage.Create(NpmPackageId.CreateUnique());
        ProjectRedundantFile redundantFile = ProjectRedundantFile.Create("*.xml");
        ProjectAllowedTool allowedTool = ProjectAllowedTool.Create("GenerateApiRoutes");
        ProjectEndpoint endpoint = ProjectEndpoint.Create("New", "New", "/new", true, "Post", "Command", "int", true);
        ProjectRouteClass routeClass = ProjectRouteClass.Create("New", "api", "v2", "/new");

        UpdateProject(project, "2", dev, prodCopy, [gitRepo], [npmPackage], [redundantFile], [allowedTool],
            [endpoint], [routeClass]);

        Assert.Equal(id, project.Id);
        AssertFields(project, "2");
        Assert.Equal(EditorConfigId, project.EditorConfigFileTypeId);
        Assert.Same(dev, project.DevDatabaseParameters);
        Assert.Same(prodCopy, project.ProdCopyDatabaseParameters);
        Assert.Equal([gitRepo], project.GitRepos);
        Assert.Equal([npmPackage], project.NpmPackages);
        Assert.Equal([redundantFile], project.RedundantFiles);
        Assert.Equal([allowedTool], project.AllowedTools);
        Assert.Equal([endpoint], project.Endpoints);
        Assert.Equal([routeClass], project.RouteClasses);
        Assert.Equal(4, project.Version);

        UpdateProject(project, "2", null, null, [], [], [], [], [], []);
        Assert.Null(project.DevDatabaseParameters);
        Assert.Null(project.ProdCopyDatabaseParameters);
        Assert.Empty(project.GitRepos);
        Assert.Empty(project.NpmPackages);
        Assert.Empty(project.RedundantFiles);
        Assert.Empty(project.AllowedTools);
        Assert.Empty(project.Endpoints);
        Assert.Empty(project.RouteClasses);
        Assert.Equal(5, project.Version);
    }

    //The version of the root is the version of the whole aggregate (README G7)
    [Fact]
    public void Update_IncrementsTheVersion_WhenOnlyAChildChanges()
    {
        Project project = NewProject(ProjectId.CreateUnique(), "1", 1);
        UpdateProject(project, "1", null, null, [], [], [ProjectRedundantFile.Create("*.pdb")], [], [], []);

        UpdateProject(project, "1", null, null, [], [], [ProjectRedundantFile.Create("*.xml")], [], [], []);

        AssertFields(project, "1");
        Assert.Equal(["*.xml"], project.RedundantFiles.Select(x => x.FileName));
        Assert.Equal(3, project.Version);
    }

    //The new parts may come from the current ones, e.g. the same lists again
    [Fact]
    public void Update_KeepsTheChildren_WhenTheyAreGivenFromTheAggregateItself()
    {
        Project project = NewProject(ProjectId.CreateUnique(), "1", 1);
        UpdateProject(project, "1", null, null,
            [
                ProjectGitRepo.Create(GitRepoId.CreateUnique(), EProjectGitRepoKind.Main),
                ProjectGitRepo.Create(GitRepoId.CreateUnique(), EProjectGitRepoKind.ScaffoldSeed)
            ], [ProjectNpmPackage.Create(NpmPackageId.CreateUnique())], [ProjectRedundantFile.Create("*.pdb")],
            [ProjectAllowedTool.Create("SeedData")],
            [ProjectEndpoint.Create("Get", null, null, false, "Get", "Query", null, false)],
            [ProjectRouteClass.Create("Git", null, null, null)]);
        List<ProjectGitRepo> gitRepos = [.. project.GitRepos];
        List<ProjectNpmPackage> npmPackages = [.. project.NpmPackages];
        List<ProjectRedundantFile> redundantFiles = [.. project.RedundantFiles];
        List<ProjectAllowedTool> allowedTools = [.. project.AllowedTools];
        List<ProjectEndpoint> endpoints = [.. project.Endpoints];
        List<ProjectRouteClass> routeClasses = [.. project.RouteClasses];

        UpdateProject(project, "1", null, null, project.GitRepos, project.NpmPackages, project.RedundantFiles,
            project.AllowedTools, project.Endpoints, project.RouteClasses);

        Assert.Equal(gitRepos, project.GitRepos);
        Assert.Equal(npmPackages, project.NpmPackages);
        Assert.Equal(redundantFiles, project.RedundantFiles);
        Assert.Equal(allowedTools, project.AllowedTools);
        Assert.Equal(endpoints, project.Endpoints);
        Assert.Equal(routeClasses, project.RouteClasses);
        Assert.Equal(3, project.Version);
    }

    //Adding to the given lists later does not change the project
    [Fact]
    public void Create_CopiesTheGivenChildren()
    {
        List<ProjectRedundantFile> redundantFiles = [ProjectRedundantFile.Create("*.pdb")];
        List<ProjectAllowedTool> allowedTools = [ProjectAllowedTool.Create("SeedData")];

        Project project = Project.Create("App", "Standard", null, null, 1, 0, false, null, null, null, null, null,
            null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
            null, null, null, null, null, null, null, null, null, [], [], redundantFiles, allowedTools, [], []);
        redundantFiles.Add(ProjectRedundantFile.Create("*.xml"));
        allowedTools.Add(ProjectAllowedTool.Create("GenerateApiRoutes"));

        Assert.Equal(["*.pdb"], project.RedundantFiles.Select(x => x.FileName));
        Assert.Equal(["SeedData"], project.AllowedTools.Select(x => x.ToolName));
    }

    [Fact]
    public void DatabaseParameters_Constructor_SetsTheValues()
    {
        DatabaseServerConnectionId connectionId = DatabaseServerConnectionId.CreateUnique();
        SmartSchemaId smartSchemaId = SmartSchemaId.CreateUnique();
        FileStorageId fileStorageId = FileStorageId.CreateUnique();

        var parameters = new DatabaseParameters(connectionId, "Simple", "Default", "AppDev", smartSchemaId,
            fileStorageId, 120, true, "dev", "yyyyMMdd", ".bak", "_FullDb_", true, false, "Diff");
        var empty = new DatabaseParameters(null, null, null, null, null, null, 0, false, null, null, null, null, null,
            null, null);

        Assert.Equal(connectionId, parameters.DbConnectionId);
        Assert.Equal("Simple", parameters.DatabaseRecoveryModel);
        Assert.Equal("Default", parameters.DbServerFoldersSetName);
        Assert.Equal("AppDev", parameters.DatabaseName);
        Assert.Equal(smartSchemaId, parameters.SmartSchemaId);
        Assert.Equal(fileStorageId, parameters.FileStorageId);
        Assert.Equal(120, parameters.CommandTimeOut);
        Assert.True(parameters.SkipBackupBeforeRestore);
        Assert.Equal("dev", parameters.BackupNamePrefix);
        Assert.Equal("yyyyMMdd", parameters.DateMask);
        Assert.Equal(".bak", parameters.BackupFileExtension);
        Assert.Equal("_FullDb_", parameters.BackupNameMiddlePart);
        Assert.True(parameters.Compress);
        Assert.False(parameters.Verify);
        Assert.Equal("Diff", parameters.BackupType);
        Assert.Null(empty.DbConnectionId);
        Assert.Null(empty.SmartSchemaId);
        Assert.Null(empty.FileStorageId);
        Assert.Null(empty.Compress);
        Assert.Null(empty.Verify);
        Assert.Null(empty.BackupType);
    }

    [Fact]
    public void Children_ConstructorsAndCreate_SetTheValues()
    {
        GitRepoId gitRepoId = GitRepoId.CreateUnique();
        NpmPackageId npmPackageId = NpmPackageId.CreateUnique();
        var gitRepoChildId = ProjectGitRepoId.CreateUnique();

        var gitRepo = new ProjectGitRepo(gitRepoChildId, gitRepoId, EProjectGitRepoKind.ScaffoldSeed);
        ProjectNpmPackage npmPackage = ProjectNpmPackage.Create(npmPackageId);
        ProjectRedundantFile redundantFile = ProjectRedundantFile.Create("*.pdb");
        ProjectAllowedTool allowedTool = ProjectAllowedTool.Create("SeedData");
        ProjectEndpoint endpoint = ProjectEndpoint.Create("Upload", "UploadGitRepos", "/uploadgitrepos", true, "Post",
            "Command", "List<string>", true);
        ProjectRouteClass routeClass = ProjectRouteClass.Create("Git", "api", "v1", "/git");

        Assert.Equal(gitRepoChildId, gitRepo.Id);
        Assert.Equal(gitRepoId, gitRepo.GitRepoId);
        Assert.Equal(EProjectGitRepoKind.ScaffoldSeed, gitRepo.Kind);
        Assert.Equal(npmPackageId, npmPackage.NpmPackageId);
        Assert.Equal("*.pdb", redundantFile.FileName);
        Assert.Equal("SeedData", allowedTool.ToolName);
        Assert.Equal("Upload", endpoint.Name);
        Assert.Equal("UploadGitRepos", endpoint.EndpointName);
        Assert.Equal("/uploadgitrepos", endpoint.EndpointRoute);
        Assert.True(endpoint.RequireAuthorization);
        Assert.Equal("Post", endpoint.HttpMethod);
        Assert.Equal("Command", endpoint.EndpointType);
        Assert.Equal("List<string>", endpoint.ReturnType);
        Assert.True(endpoint.SendMessageToCurrentUser);
        Assert.Equal("Git", routeClass.Name);
        Assert.Equal("api", routeClass.Root);
        Assert.Equal("v1", routeClass.ApiVersion);
        Assert.Equal("/git", routeClass.Base);
        Assert.NotEqual(ProjectGitRepo.Create(gitRepoId, EProjectGitRepoKind.Main).Id,
            ProjectGitRepo.Create(gitRepoId, EProjectGitRepoKind.Main).Id);
        Assert.NotEqual(ProjectNpmPackage.Create(npmPackageId).Id, ProjectNpmPackage.Create(npmPackageId).Id);
        Assert.NotEqual(ProjectRedundantFile.Create("a").Id, ProjectRedundantFile.Create("a").Id);
        Assert.NotEqual(ProjectAllowedTool.Create("a").Id, ProjectAllowedTool.Create("a").Id);
        Assert.NotEqual(ProjectEndpoint.Create("a", null, null, false, "Get", "Query", null, false).Id,
            ProjectEndpoint.Create("a", null, null, false, "Get", "Query", null, false).Id);
        Assert.NotEqual(ProjectRouteClass.Create("a", null, null, null).Id,
            ProjectRouteClass.Create("a", null, null, null).Id);
        Assert.Empty(endpoint.DomainEvents);
    }

    [Fact]
    public void Ids_WithTheSameValueAreEqual()
    {
        ProjectId id = ProjectId.CreateUnique();
        ProjectGitRepoId gitRepoId = ProjectGitRepoId.CreateUnique();
        ProjectNpmPackageId npmPackageId = ProjectNpmPackageId.CreateUnique();
        ProjectRedundantFileId redundantFileId = ProjectRedundantFileId.CreateUnique();
        ProjectAllowedToolId allowedToolId = ProjectAllowedToolId.CreateUnique();
        ProjectEndpointId endpointId = ProjectEndpointId.CreateUnique();
        ProjectRouteClassId routeClassId = ProjectRouteClassId.CreateUnique();

        Assert.Equal(id, new ProjectId(id.Value));
        Assert.NotEqual(id, ProjectId.CreateUnique());
        Assert.Equal(gitRepoId, new ProjectGitRepoId(gitRepoId.Value));
        Assert.NotEqual(gitRepoId, ProjectGitRepoId.CreateUnique());
        Assert.Equal(npmPackageId, new ProjectNpmPackageId(npmPackageId.Value));
        Assert.NotEqual(npmPackageId, ProjectNpmPackageId.CreateUnique());
        Assert.Equal(redundantFileId, new ProjectRedundantFileId(redundantFileId.Value));
        Assert.NotEqual(redundantFileId, ProjectRedundantFileId.CreateUnique());
        Assert.Equal(allowedToolId, new ProjectAllowedToolId(allowedToolId.Value));
        Assert.NotEqual(allowedToolId, ProjectAllowedToolId.CreateUnique());
        Assert.Equal(endpointId, new ProjectEndpointId(endpointId.Value));
        Assert.NotEqual(endpointId, ProjectEndpointId.CreateUnique());
        Assert.Equal(routeClassId, new ProjectRouteClassId(routeClassId.Value));
        Assert.NotEqual(routeClassId, ProjectRouteClassId.CreateUnique());
    }
}

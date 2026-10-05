using System.Collections.Generic;
using SupportToolsServerCore.Domain.EditorConfigFileTypes;
using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.Projects;

//პროექტი, SupportToolsParameters.Projects-ის ჩანაწერი (კლიენტის ProjectModel): სახელი (dictionary-ის key), აღწერა,
//კოდის სახელები, ნიღბები, კანონიკური გზები (README G3: სერვერი მათ ინახავს ისე, როგორც მოვიდა), ბაზის პარამეტრები და
//შვილი კოლექციები. ProjectType კლიენტის EProjectType-ის სახელია; სერვერი enum-ს არ იცნობს და მხოლოდ სიგრძეს ამოწმებს.
//EditorConfigFileTypeId, git-ები და npm პაკეტები სხვა აგრეგატების ჩანაწერებია (კონტრაქტში სახელებით). KeyGuidPart
//საიდუმლოა: ღიად ინახება (README G2), მაგრამ არსად იბეჭდება. ServerInfo-ები B7-ში დაემატება
public sealed class Project : VersionedEntity<ProjectId>
{
    public const int NameMaxLength = 100;
    public const int ProjectTypeMaxLength = 50;
    public const int ProjectGroupNameMaxLength = 100;
    public const int ProjectDescriptionMaxLength = 255;

    //solution-ის პროექტების, DbContext-ის კლასისა და მოკლე პრეფიქსის სახელები (MainProjectName … ProjectShortPrefix)
    public const int CodeNameMaxLength = 100;

    //თარიღის ნიღბები და გაფართოებები, GlobalSettings-ის იგივე ველების მსგავსად
    public const int MaskMaxLength = 50;

    //კანონიკური გზები (README G3), Windows-ის MAX_PATH
    public const int PathMaxLength = 260;

    public const int KeyGuidPartMaxLength = 256;

    private readonly List<ProjectAllowedTool> _allowedTools = [];
    private readonly List<ProjectEndpoint> _endpoints = [];
    private readonly List<ProjectGitRepo> _gitRepos = [];
    private readonly List<ProjectNpmPackage> _npmPackages = [];
    private readonly List<ProjectRedundantFile> _redundantFiles = [];
    private readonly List<ProjectRouteClass> _routeClasses = [];

    //ბაზის პარამეტრებსა და შვილ კოლექციებს EF კონსტრუქტორით ვერ გადასცემს და ჩანაწერის წაკითხვისას თვითონ ავსებს
    public Project(ProjectId id, string name, string projectType, string? projectGroupName,
        string? projectDescription, int majorVersion, int minorVersion, bool useAlternativeWebAgent,
        EditorConfigFileTypeId? editorConfigFileTypeId, string? mainProjectName, string? apiContractsProjectName,
        string? spaProjectName, string? dbContextName, string? projectShortPrefix, string? scaffoldSeederProjectName,
        string? dbContextProjectName, string? newDataSeedingClassLibProjectName, string? programArchiveDateMask,
        string? programArchiveExtension, string? parametersFileDateMask, string? parametersFileExtension,
        string? projectFolderName, string? solutionFileName, string? projectSecurityFolderPath,
        string? migrationStartupProjectFilePath, string? migrationProjectFilePath,
        string? dataSeederRulesByTableStartupProjectFilePath, string? oldDataConvertorForDataSeeder,
        string? seedProjectFilePath, string? seedProjectParametersFilePath, string? excludesRulesParametersFilePath,
        string? appSetEnKeysJsonFileName, string? migrationSqlFilesFolder,
        string? prepareProdCopyDatabaseProjectFilePath, string? prepareProdCopyDatabaseProjectParametersFilePath,
        string? pairedDbObjectsResultFileName, string? keyGuidPart, int version) : base(id, version)
    {
        Name = name;
        ProjectType = projectType;
        ProjectGroupName = projectGroupName;
        ProjectDescription = projectDescription;
        MajorVersion = majorVersion;
        MinorVersion = minorVersion;
        UseAlternativeWebAgent = useAlternativeWebAgent;
        EditorConfigFileTypeId = editorConfigFileTypeId;
        MainProjectName = mainProjectName;
        ApiContractsProjectName = apiContractsProjectName;
        SpaProjectName = spaProjectName;
        DbContextName = dbContextName;
        ProjectShortPrefix = projectShortPrefix;
        ScaffoldSeederProjectName = scaffoldSeederProjectName;
        DbContextProjectName = dbContextProjectName;
        NewDataSeedingClassLibProjectName = newDataSeedingClassLibProjectName;
        ProgramArchiveDateMask = programArchiveDateMask;
        ProgramArchiveExtension = programArchiveExtension;
        ParametersFileDateMask = parametersFileDateMask;
        ParametersFileExtension = parametersFileExtension;
        ProjectFolderName = projectFolderName;
        SolutionFileName = solutionFileName;
        ProjectSecurityFolderPath = projectSecurityFolderPath;
        MigrationStartupProjectFilePath = migrationStartupProjectFilePath;
        MigrationProjectFilePath = migrationProjectFilePath;
        DataSeederRulesByTableStartupProjectFilePath = dataSeederRulesByTableStartupProjectFilePath;
        OldDataConvertorForDataSeeder = oldDataConvertorForDataSeeder;
        SeedProjectFilePath = seedProjectFilePath;
        SeedProjectParametersFilePath = seedProjectParametersFilePath;
        ExcludesRulesParametersFilePath = excludesRulesParametersFilePath;
        AppSetEnKeysJsonFileName = appSetEnKeysJsonFileName;
        MigrationSqlFilesFolder = migrationSqlFilesFolder;
        PrepareProdCopyDatabaseProjectFilePath = prepareProdCopyDatabaseProjectFilePath;
        PrepareProdCopyDatabaseProjectParametersFilePath = prepareProdCopyDatabaseProjectParametersFilePath;
        PairedDbObjectsResultFileName = pairedDbObjectsResultFileName;
        KeyGuidPart = keyGuidPart;
    }

    public string Name { get; private set; }
    public string ProjectType { get; private set; }
    public string? ProjectGroupName { get; private set; }
    public string? ProjectDescription { get; private set; }
    public int MajorVersion { get; private set; }
    public int MinorVersion { get; private set; }
    public bool UseAlternativeWebAgent { get; private set; }

    //.editorconfig შაბლონი. null ნიშნავს, რომ პროექტის .editorconfig ფაილი არ მოწმდება
    public EditorConfigFileTypeId? EditorConfigFileTypeId { get; private set; }

    public string? MainProjectName { get; private set; }
    public string? ApiContractsProjectName { get; private set; }
    public string? SpaProjectName { get; private set; }
    public string? DbContextName { get; private set; }
    public string? ProjectShortPrefix { get; private set; }
    public string? ScaffoldSeederProjectName { get; private set; }
    public string? DbContextProjectName { get; private set; }
    public string? NewDataSeedingClassLibProjectName { get; private set; }
    public string? ProgramArchiveDateMask { get; private set; }
    public string? ProgramArchiveExtension { get; private set; }
    public string? ParametersFileDateMask { get; private set; }
    public string? ParametersFileExtension { get; private set; }
    public string? ProjectFolderName { get; private set; }
    public string? SolutionFileName { get; private set; }
    public string? ProjectSecurityFolderPath { get; private set; }
    public string? MigrationStartupProjectFilePath { get; private set; }
    public string? MigrationProjectFilePath { get; private set; }
    public string? DataSeederRulesByTableStartupProjectFilePath { get; private set; }
    public string? OldDataConvertorForDataSeeder { get; private set; }
    public string? SeedProjectFilePath { get; private set; }
    public string? SeedProjectParametersFilePath { get; private set; }
    public string? ExcludesRulesParametersFilePath { get; private set; }
    public string? AppSetEnKeysJsonFileName { get; private set; }
    public string? MigrationSqlFilesFolder { get; private set; }
    public string? PrepareProdCopyDatabaseProjectFilePath { get; private set; }
    public string? PrepareProdCopyDatabaseProjectParametersFilePath { get; private set; }
    public string? PairedDbObjectsResultFileName { get; private set; }
    public string? KeyGuidPart { get; private set; }
    public DatabaseParameters? DevDatabaseParameters { get; private set; }
    public DatabaseParameters? ProdCopyDatabaseParameters { get; private set; }
    public IReadOnlyList<ProjectGitRepo> GitRepos => _gitRepos;
    public IReadOnlyList<ProjectNpmPackage> NpmPackages => _npmPackages;
    public IReadOnlyList<ProjectRedundantFile> RedundantFiles => _redundantFiles;
    public IReadOnlyList<ProjectAllowedTool> AllowedTools => _allowedTools;
    public IReadOnlyList<ProjectEndpoint> Endpoints => _endpoints;
    public IReadOnlyList<ProjectRouteClass> RouteClasses => _routeClasses;

    public static Project Create(string name, string projectType, string? projectGroupName,
        string? projectDescription, int majorVersion, int minorVersion, bool useAlternativeWebAgent,
        EditorConfigFileTypeId? editorConfigFileTypeId, string? mainProjectName, string? apiContractsProjectName,
        string? spaProjectName, string? dbContextName, string? projectShortPrefix, string? scaffoldSeederProjectName,
        string? dbContextProjectName, string? newDataSeedingClassLibProjectName, string? programArchiveDateMask,
        string? programArchiveExtension, string? parametersFileDateMask, string? parametersFileExtension,
        string? projectFolderName, string? solutionFileName, string? projectSecurityFolderPath,
        string? migrationStartupProjectFilePath, string? migrationProjectFilePath,
        string? dataSeederRulesByTableStartupProjectFilePath, string? oldDataConvertorForDataSeeder,
        string? seedProjectFilePath, string? seedProjectParametersFilePath, string? excludesRulesParametersFilePath,
        string? appSetEnKeysJsonFileName, string? migrationSqlFilesFolder,
        string? prepareProdCopyDatabaseProjectFilePath, string? prepareProdCopyDatabaseProjectParametersFilePath,
        string? pairedDbObjectsResultFileName, string? keyGuidPart, DatabaseParameters? devDatabaseParameters,
        DatabaseParameters? prodCopyDatabaseParameters, IEnumerable<ProjectGitRepo> gitRepos,
        IEnumerable<ProjectNpmPackage> npmPackages, IEnumerable<ProjectRedundantFile> redundantFiles,
        IEnumerable<ProjectAllowedTool> allowedTools, IEnumerable<ProjectEndpoint> endpoints,
        IEnumerable<ProjectRouteClass> routeClasses)
    {
        var project = new Project(ProjectId.CreateUnique(), name, projectType, projectGroupName, projectDescription,
            majorVersion, minorVersion, useAlternativeWebAgent, editorConfigFileTypeId, mainProjectName,
            apiContractsProjectName, spaProjectName, dbContextName, projectShortPrefix, scaffoldSeederProjectName,
            dbContextProjectName, newDataSeedingClassLibProjectName, programArchiveDateMask, programArchiveExtension,
            parametersFileDateMask, parametersFileExtension, projectFolderName, solutionFileName,
            projectSecurityFolderPath, migrationStartupProjectFilePath, migrationProjectFilePath,
            dataSeederRulesByTableStartupProjectFilePath, oldDataConvertorForDataSeeder, seedProjectFilePath,
            seedProjectParametersFilePath, excludesRulesParametersFilePath, appSetEnKeysJsonFileName,
            migrationSqlFilesFolder, prepareProdCopyDatabaseProjectFilePath,
            prepareProdCopyDatabaseProjectParametersFilePath, pairedDbObjectsResultFileName, keyGuidPart,
            EntityVersion.Initial);
        project.ReplaceParts(devDatabaseParameters, prodCopyDatabaseParameters, gitRepos, npmPackages, redundantFiles,
            allowedTools, endpoints, routeClasses);
        return project;
    }

    //რედაქტირება მთელ აგრეგატს ანაცვლებს (README G7): ბაზის პარამეტრები და შვილი კოლექციები ახლით იცვლება და ვერსია
    //ნებისმიერ ცვლილებაზე იზრდება, მათ შორის მაშინაც, როცა მხოლოდ შვილი შეიცვალა. სახელის შეცვლა (მაგალითად, მხოლოდ
    //რეგისტრის) იგივე ჩანაწერის განახლებაა
    public void Update(string name, string projectType, string? projectGroupName, string? projectDescription,
        int majorVersion, int minorVersion, bool useAlternativeWebAgent, EditorConfigFileTypeId? editorConfigFileTypeId,
        string? mainProjectName, string? apiContractsProjectName, string? spaProjectName, string? dbContextName,
        string? projectShortPrefix, string? scaffoldSeederProjectName, string? dbContextProjectName,
        string? newDataSeedingClassLibProjectName, string? programArchiveDateMask, string? programArchiveExtension,
        string? parametersFileDateMask, string? parametersFileExtension, string? projectFolderName,
        string? solutionFileName, string? projectSecurityFolderPath, string? migrationStartupProjectFilePath,
        string? migrationProjectFilePath, string? dataSeederRulesByTableStartupProjectFilePath,
        string? oldDataConvertorForDataSeeder, string? seedProjectFilePath, string? seedProjectParametersFilePath,
        string? excludesRulesParametersFilePath, string? appSetEnKeysJsonFileName, string? migrationSqlFilesFolder,
        string? prepareProdCopyDatabaseProjectFilePath, string? prepareProdCopyDatabaseProjectParametersFilePath,
        string? pairedDbObjectsResultFileName, string? keyGuidPart, DatabaseParameters? devDatabaseParameters,
        DatabaseParameters? prodCopyDatabaseParameters, IEnumerable<ProjectGitRepo> gitRepos,
        IEnumerable<ProjectNpmPackage> npmPackages, IEnumerable<ProjectRedundantFile> redundantFiles,
        IEnumerable<ProjectAllowedTool> allowedTools, IEnumerable<ProjectEndpoint> endpoints,
        IEnumerable<ProjectRouteClass> routeClasses)
    {
        Name = name;
        ProjectType = projectType;
        ProjectGroupName = projectGroupName;
        ProjectDescription = projectDescription;
        MajorVersion = majorVersion;
        MinorVersion = minorVersion;
        UseAlternativeWebAgent = useAlternativeWebAgent;
        EditorConfigFileTypeId = editorConfigFileTypeId;
        MainProjectName = mainProjectName;
        ApiContractsProjectName = apiContractsProjectName;
        SpaProjectName = spaProjectName;
        DbContextName = dbContextName;
        ProjectShortPrefix = projectShortPrefix;
        ScaffoldSeederProjectName = scaffoldSeederProjectName;
        DbContextProjectName = dbContextProjectName;
        NewDataSeedingClassLibProjectName = newDataSeedingClassLibProjectName;
        ProgramArchiveDateMask = programArchiveDateMask;
        ProgramArchiveExtension = programArchiveExtension;
        ParametersFileDateMask = parametersFileDateMask;
        ParametersFileExtension = parametersFileExtension;
        ProjectFolderName = projectFolderName;
        SolutionFileName = solutionFileName;
        ProjectSecurityFolderPath = projectSecurityFolderPath;
        MigrationStartupProjectFilePath = migrationStartupProjectFilePath;
        MigrationProjectFilePath = migrationProjectFilePath;
        DataSeederRulesByTableStartupProjectFilePath = dataSeederRulesByTableStartupProjectFilePath;
        OldDataConvertorForDataSeeder = oldDataConvertorForDataSeeder;
        SeedProjectFilePath = seedProjectFilePath;
        SeedProjectParametersFilePath = seedProjectParametersFilePath;
        ExcludesRulesParametersFilePath = excludesRulesParametersFilePath;
        AppSetEnKeysJsonFileName = appSetEnKeysJsonFileName;
        MigrationSqlFilesFolder = migrationSqlFilesFolder;
        PrepareProdCopyDatabaseProjectFilePath = prepareProdCopyDatabaseProjectFilePath;
        PrepareProdCopyDatabaseProjectParametersFilePath = prepareProdCopyDatabaseProjectParametersFilePath;
        PairedDbObjectsResultFileName = pairedDbObjectsResultFileName;
        KeyGuidPart = keyGuidPart;
        ReplaceParts(devDatabaseParameters, prodCopyDatabaseParameters, gitRepos, npmPackages, redundantFiles,
            allowedTools, endpoints, routeClasses);
        IncrementVersion();
    }

    //ახალი სიები ჯერ კოპირდება, რადგან ისინი შეიძლება აგრეგატის მიმდინარე სიებიდან მოდიოდეს
    private void ReplaceParts(DatabaseParameters? devDatabaseParameters,
        DatabaseParameters? prodCopyDatabaseParameters, IEnumerable<ProjectGitRepo> gitRepos,
        IEnumerable<ProjectNpmPackage> npmPackages, IEnumerable<ProjectRedundantFile> redundantFiles,
        IEnumerable<ProjectAllowedTool> allowedTools, IEnumerable<ProjectEndpoint> endpoints,
        IEnumerable<ProjectRouteClass> routeClasses)
    {
        List<ProjectGitRepo> newGitRepos = [.. gitRepos];
        List<ProjectNpmPackage> newNpmPackages = [.. npmPackages];
        List<ProjectRedundantFile> newRedundantFiles = [.. redundantFiles];
        List<ProjectAllowedTool> newAllowedTools = [.. allowedTools];
        List<ProjectEndpoint> newEndpoints = [.. endpoints];
        List<ProjectRouteClass> newRouteClasses = [.. routeClasses];
        DevDatabaseParameters = devDatabaseParameters;
        ProdCopyDatabaseParameters = prodCopyDatabaseParameters;
        Replace(_gitRepos, newGitRepos);
        Replace(_npmPackages, newNpmPackages);
        Replace(_redundantFiles, newRedundantFiles);
        Replace(_allowedTools, newAllowedTools);
        Replace(_endpoints, newEndpoints);
        Replace(_routeClasses, newRouteClasses);
    }

    private static void Replace<T>(List<T> items, List<T> newItems)
    {
        items.Clear();
        items.AddRange(newItems);
    }
}

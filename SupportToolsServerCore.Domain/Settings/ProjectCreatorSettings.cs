using SupportToolsServerCore.Domain.DatabaseServerConnections;
using SupportToolsServerCore.Domain.DeploymentEnvironments;
using SupportToolsServerCore.Domain.FileStorages;
using SupportToolsServerCore.Domain.Primitives;
using SupportToolsServerCore.Domain.Servers;
using SupportToolsServerCore.Domain.SmartSchemas;

namespace SupportToolsServerCore.Domain.Settings;

//პროექტის შემქმნელის პარამეტრები: კლიენტის AppProjectCreatorAllParameters, Templates-ის გარეშე (შაბლონები ცალკე
//აგრეგატია, ProjectTemplate). ჩანაწერი ერთადერთია (singleton) და მისი გასაღები ყოველთვის
//ProjectCreatorSettingsId.Singleton-ია. ProjectsFolderPathReal და SecretsFolderPathReal კანონიკური გზებია (README G3):
//სერვერი მათ ინახავს ისე, როგორც მოვიდა. ProductionServerId, ProductionEnvironmentId, DeveloperDbConnectionId,
//DatabaseExchangeFileStorageId და UseSmartSchemaId სხვა აგრეგატების ჩანაწერებია (კონტრაქტში სახელებით)
public sealed class ProjectCreatorSettings : VersionedEntity<ProjectCreatorSettingsId>
{
    public const int FakeHostProjectNameMaxLength = 100;

    //Windows-ის MAX_PATH
    public const int ProjectsFolderPathRealMaxLength = 260;
    public const int SecretsFolderPathRealMaxLength = 260;

    public ProjectCreatorSettings(ProjectCreatorSettingsId id, int indentSize, string? fakeHostProjectName,
        string? projectsFolderPathReal, string? secretsFolderPathReal, ServerId? productionServerId,
        DeploymentEnvironmentId? productionEnvironmentId, DatabaseServerConnectionId? developerDbConnectionId,
        FileStorageId? databaseExchangeFileStorageId, SmartSchemaId? useSmartSchemaId, int version) : base(id, version)
    {
        IndentSize = indentSize;
        FakeHostProjectName = fakeHostProjectName;
        ProjectsFolderPathReal = projectsFolderPathReal;
        SecretsFolderPathReal = secretsFolderPathReal;
        ProductionServerId = productionServerId;
        ProductionEnvironmentId = productionEnvironmentId;
        DeveloperDbConnectionId = developerDbConnectionId;
        DatabaseExchangeFileStorageId = databaseExchangeFileStorageId;
        UseSmartSchemaId = useSmartSchemaId;
    }

    //გენერირებული კოდის შეწევის ზომა
    public int IndentSize { get; private set; }

    public string? FakeHostProjectName { get; private set; }
    public string? ProjectsFolderPathReal { get; private set; }
    public string? SecretsFolderPathReal { get; private set; }
    public ServerId? ProductionServerId { get; private set; }
    public DeploymentEnvironmentId? ProductionEnvironmentId { get; private set; }
    public DatabaseServerConnectionId? DeveloperDbConnectionId { get; private set; }
    public FileStorageId? DatabaseExchangeFileStorageId { get; private set; }

    //კლიენტის UseSmartSchema: სახელის მიუხედავად, ჭკვიანი სქემის მითითებაა
    public SmartSchemaId? UseSmartSchemaId { get; private set; }

    public static ProjectCreatorSettings Create(int indentSize, string? fakeHostProjectName,
        string? projectsFolderPathReal, string? secretsFolderPathReal, ServerId? productionServerId,
        DeploymentEnvironmentId? productionEnvironmentId, DatabaseServerConnectionId? developerDbConnectionId,
        FileStorageId? databaseExchangeFileStorageId, SmartSchemaId? useSmartSchemaId)
    {
        return new ProjectCreatorSettings(ProjectCreatorSettingsId.Singleton, indentSize, fakeHostProjectName,
            projectsFolderPathReal, secretsFolderPathReal, productionServerId, productionEnvironmentId,
            developerDbConnectionId, databaseExchangeFileStorageId, useSmartSchemaId, EntityVersion.Initial);
    }

    //რედაქტირება მთელ ჩანაწერს ანაცვლებს და ვერსიას ზრდის
    public void Update(int indentSize, string? fakeHostProjectName, string? projectsFolderPathReal,
        string? secretsFolderPathReal, ServerId? productionServerId, DeploymentEnvironmentId? productionEnvironmentId,
        DatabaseServerConnectionId? developerDbConnectionId, FileStorageId? databaseExchangeFileStorageId,
        SmartSchemaId? useSmartSchemaId)
    {
        IndentSize = indentSize;
        FakeHostProjectName = fakeHostProjectName;
        ProjectsFolderPathReal = projectsFolderPathReal;
        SecretsFolderPathReal = secretsFolderPathReal;
        ProductionServerId = productionServerId;
        ProductionEnvironmentId = productionEnvironmentId;
        DeveloperDbConnectionId = developerDbConnectionId;
        DatabaseExchangeFileStorageId = databaseExchangeFileStorageId;
        UseSmartSchemaId = useSmartSchemaId;
        IncrementVersion();
    }
}

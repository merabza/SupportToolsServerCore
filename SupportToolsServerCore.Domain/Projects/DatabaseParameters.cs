using SupportToolsServerCore.Domain.DatabaseServerConnections;
using SupportToolsServerCore.Domain.FileStorages;
using SupportToolsServerCore.Domain.SmartSchemas;

namespace SupportToolsServerCore.Domain.Projects;

//ბაზის პარამეტრები, კლიენტის DatabaseParameters: პროექტის DevDatabaseParameters და ProdCopyDatabaseParameters, ასევე
//ServerInfo-ს CurrentDatabaseParameters და NewDatabaseParameters. DbConnectionId, SmartSchemaId და FileStorageId სხვა
//აგრეგატების ჩანაწერებია (კონტრაქტში სახელებით). DbServerFoldersSetName ბაზის კავშირის ფოლდერების ნაკრების სახელია;
//ნაკრები სხვა აგრეგატის შვილია, ამიტომ სერვერი მის არსებობას არ ამოწმებს. DatabaseRecoveryModel და BackupType კლიენტის
//enum-ების სახელებია; სერვერი enum-ებს არ იცნობს და მხოლოდ სიგრძეს ამოწმებს. ცალკე ცხრილი არ აქვს: EF-ის owned
//type-ია და მფლობელის სტრიქონში ინახება. ნაწილი არასავალდებულოა (null ნიშნავს, რომ პარამეტრები არ არის, და კლიენტი ამას
//ამოწმებს): EF მის არსებობას სავალდებულო CommandTimeOut და SkipBackupBeforeRestore ველების სვეტებით ცნობს
public sealed class DatabaseParameters
{
    public const int DatabaseRecoveryModelMaxLength = 50;
    public const int DbServerFoldersSetNameMaxLength = DatabaseFoldersSet.NameMaxLength;

    //SQL Server-ის sysname
    public const int DatabaseNameMaxLength = 128;

    public const int BackupNamePrefixMaxLength = 100;

    //თარიღის ნიღაბი (yyyyMMddHHmmss) და გაფართოება (.bak)
    public const int DateMaskMaxLength = 50;
    public const int BackupFileExtensionMaxLength = 50;

    public const int BackupNameMiddlePartMaxLength = 100;
    public const int BackupTypeMaxLength = 50;

    public DatabaseParameters(DatabaseServerConnectionId? dbConnectionId, string? databaseRecoveryModel,
        string? dbServerFoldersSetName, string? databaseName, SmartSchemaId? smartSchemaId,
        FileStorageId? fileStorageId, int commandTimeOut, bool skipBackupBeforeRestore, string? backupNamePrefix,
        string? dateMask, string? backupFileExtension, string? backupNameMiddlePart, bool? compress, bool? verify,
        string? backupType)
    {
        DbConnectionId = dbConnectionId;
        DatabaseRecoveryModel = databaseRecoveryModel;
        DbServerFoldersSetName = dbServerFoldersSetName;
        DatabaseName = databaseName;
        SmartSchemaId = smartSchemaId;
        FileStorageId = fileStorageId;
        CommandTimeOut = commandTimeOut;
        SkipBackupBeforeRestore = skipBackupBeforeRestore;
        BackupNamePrefix = backupNamePrefix;
        DateMask = dateMask;
        BackupFileExtension = backupFileExtension;
        BackupNameMiddlePart = backupNameMiddlePart;
        Compress = compress;
        Verify = verify;
        BackupType = backupType;
    }

    public DatabaseServerConnectionId? DbConnectionId { get; private set; }
    public string? DatabaseRecoveryModel { get; private set; }
    public string? DbServerFoldersSetName { get; private set; }
    public string? DatabaseName { get; private set; }

    //ჭკვიანი სქემა ძველი ბექაპის ფაილების დასატოვებლად და წასაშლელად (ბაზის სერვერის მხარეს)
    public SmartSchemaId? SmartSchemaId { get; private set; }

    //ბაზის ბექაპების გაცვლის ფაილსაცავი (სერვერის მხარეს)
    public FileStorageId? FileStorageId { get; private set; }

    public int CommandTimeOut { get; private set; }
    public bool SkipBackupBeforeRestore { get; private set; }
    public string? BackupNamePrefix { get; private set; }
    public string? DateMask { get; private set; }
    public string? BackupFileExtension { get; private set; }
    public string? BackupNameMiddlePart { get; private set; }
    public bool? Compress { get; private set; }
    public bool? Verify { get; private set; }
    public string? BackupType { get; private set; }
}

using SupportToolsServerCore.Domain.ApiClients;
using SupportToolsServerCore.Domain.FileStorages;
using SupportToolsServerCore.Domain.Primitives;
using SupportToolsServerCore.Domain.SmartSchemas;

namespace SupportToolsServerCore.Domain.Settings;

//გლობალური პარამეტრები: SupportToolsParameters-ის საერთო ველები (README §4.1). ჩანაწერი ერთადერთია (singleton) და მისი
//გასაღები ყოველთვის GlobalSettingsId.Singleton-ია. FileStorageForExchangeId, SmartSchemaForExchangeId,
//SmartSchemaForLocalId და LocalPackageManagerWebApiClientId სხვა აგრეგატების ჩანაწერებია (კონტრაქტში სახელებით).
//MediatRLicenseKey საიდუმლოა: ღიად ინახება (README G2), მაგრამ არსად იბეჭდება. კომპიუტერის ველები
//(SupportToolsServerWebApiClientName, LocalInstallerSettings, Archivers, ფოლდერები) აქ არ ინახება
public sealed class GlobalSettings : VersionedEntity<GlobalSettingsId>
{
    public const int ServiceDescriptionSignatureMaxLength = 100;

    //გაფართოებები (.up!, .zip, .json) და თარიღის ნიღბები (yyyyMMddHHmmss)
    public const int UploadTempExtensionMaxLength = 50;
    public const int ProgramArchiveDateMaskMaxLength = 50;
    public const int ProgramArchiveExtensionMaxLength = 50;
    public const int ParametersFileDateMaskMaxLength = 50;
    public const int ParametersFileExtensionMaxLength = 50;

    //MediatR-ის ლიცენზიის გასაღები JWT-ია (1-1.5 ათასი სიმბოლო). 4000 MAX-ის გარეშე nvarchar-ის უდიდესი სიგრძეა
    public const int MediatRLicenseKeyMaxLength = 4000;

    public GlobalSettings(GlobalSettingsId id, string? serviceDescriptionSignature, string? uploadTempExtension,
        string? programArchiveDateMask, string? programArchiveExtension, string? parametersFileDateMask,
        string? parametersFileExtension, string? mediatRLicenseKey, FileStorageId? fileStorageForExchangeId,
        SmartSchemaId? smartSchemaForExchangeId, SmartSchemaId? smartSchemaForLocalId,
        ApiClientId? localPackageManagerWebApiClientId, DatabasesBackupFilesExchange databasesBackupFilesExchange,
        int version) : this(id, serviceDescriptionSignature, uploadTempExtension, programArchiveDateMask,
        programArchiveExtension, parametersFileDateMask, parametersFileExtension, mediatRLicenseKey,
        fileStorageForExchangeId, smartSchemaForExchangeId, smartSchemaForLocalId, localPackageManagerWebApiClientId,
        version)
    {
        DatabasesBackupFilesExchange = databasesBackupFilesExchange;
    }

    //EF-ის კონსტრუქტორი: owned type-ს (DatabasesBackupFilesExchange) EF კონსტრუქტორით ვერ გადასცემს და მას ჩანაწერის
    //წაკითხვისას თვითონ ავსებს
    private GlobalSettings(GlobalSettingsId id, string? serviceDescriptionSignature, string? uploadTempExtension,
        string? programArchiveDateMask, string? programArchiveExtension, string? parametersFileDateMask,
        string? parametersFileExtension, string? mediatRLicenseKey, FileStorageId? fileStorageForExchangeId,
        SmartSchemaId? smartSchemaForExchangeId, SmartSchemaId? smartSchemaForLocalId,
        ApiClientId? localPackageManagerWebApiClientId, int version) : base(id, version)
    {
        ServiceDescriptionSignature = serviceDescriptionSignature;
        UploadTempExtension = uploadTempExtension;
        ProgramArchiveDateMask = programArchiveDateMask;
        ProgramArchiveExtension = programArchiveExtension;
        ParametersFileDateMask = parametersFileDateMask;
        ParametersFileExtension = parametersFileExtension;
        MediatRLicenseKey = mediatRLicenseKey;
        FileStorageForExchangeId = fileStorageForExchangeId;
        SmartSchemaForExchangeId = smartSchemaForExchangeId;
        SmartSchemaForLocalId = smartSchemaForLocalId;
        LocalPackageManagerWebApiClientId = localPackageManagerWebApiClientId;
        DatabasesBackupFilesExchange = null!;
    }

    //სერვისის აღწერის ხელმოწერა
    public string? ServiceDescriptionSignature { get; private set; }

    public string? UploadTempExtension { get; private set; }
    public string? ProgramArchiveDateMask { get; private set; }
    public string? ProgramArchiveExtension { get; private set; }
    public string? ParametersFileDateMask { get; private set; }
    public string? ParametersFileExtension { get; private set; }
    public string? MediatRLicenseKey { get; private set; }

    //პროგრამების დაზიპული ფაილების ასატვირთი ფაილსაცავი
    public FileStorageId? FileStorageForExchangeId { get; private set; }

    public SmartSchemaId? SmartSchemaForExchangeId { get; private set; }
    public SmartSchemaId? SmartSchemaForLocalId { get; private set; }
    public ApiClientId? LocalPackageManagerWebApiClientId { get; private set; }
    public DatabasesBackupFilesExchange DatabasesBackupFilesExchange { get; private set; }

    public static GlobalSettings Create(string? serviceDescriptionSignature, string? uploadTempExtension,
        string? programArchiveDateMask, string? programArchiveExtension, string? parametersFileDateMask,
        string? parametersFileExtension, string? mediatRLicenseKey, FileStorageId? fileStorageForExchangeId,
        SmartSchemaId? smartSchemaForExchangeId, SmartSchemaId? smartSchemaForLocalId,
        ApiClientId? localPackageManagerWebApiClientId, DatabasesBackupFilesExchange databasesBackupFilesExchange)
    {
        return new GlobalSettings(GlobalSettingsId.Singleton, serviceDescriptionSignature, uploadTempExtension,
            programArchiveDateMask, programArchiveExtension, parametersFileDateMask, parametersFileExtension,
            mediatRLicenseKey, fileStorageForExchangeId, smartSchemaForExchangeId, smartSchemaForLocalId,
            localPackageManagerWebApiClientId, databasesBackupFilesExchange, EntityVersion.Initial);
    }

    //რედაქტირება მთელ ჩანაწერს ანაცვლებს და ვერსიას ზრდის
    public void Update(string? serviceDescriptionSignature, string? uploadTempExtension,
        string? programArchiveDateMask, string? programArchiveExtension, string? parametersFileDateMask,
        string? parametersFileExtension, string? mediatRLicenseKey, FileStorageId? fileStorageForExchangeId,
        SmartSchemaId? smartSchemaForExchangeId, SmartSchemaId? smartSchemaForLocalId,
        ApiClientId? localPackageManagerWebApiClientId, DatabasesBackupFilesExchange databasesBackupFilesExchange)
    {
        ServiceDescriptionSignature = serviceDescriptionSignature;
        UploadTempExtension = uploadTempExtension;
        ProgramArchiveDateMask = programArchiveDateMask;
        ProgramArchiveExtension = programArchiveExtension;
        ParametersFileDateMask = parametersFileDateMask;
        ParametersFileExtension = parametersFileExtension;
        MediatRLicenseKey = mediatRLicenseKey;
        FileStorageForExchangeId = fileStorageForExchangeId;
        SmartSchemaForExchangeId = smartSchemaForExchangeId;
        SmartSchemaForLocalId = smartSchemaForLocalId;
        LocalPackageManagerWebApiClientId = localPackageManagerWebApiClientId;
        DatabasesBackupFilesExchange = databasesBackupFilesExchange;
        IncrementVersion();
    }
}

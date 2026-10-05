using SupportToolsServerCore.Domain.FileStorages;
using SupportToolsServerCore.Domain.SmartSchemas;

namespace SupportToolsServerCore.Domain.Settings;

//GlobalSettings-ის ნაწილი, კლიენტის DatabasesBackupFilesExchangeParameters, LocalPath-ის გარეშე: ის კომპიუტერის
//ფოლდერია (README §4.1). ბაზების backup ფაილების გაცვლის დროებითი გაფართოებები, გაცვლის ფაილსაცავი და ჭკვიანი სქემები
//გაცვლისა და ლოკალური მხარისთვის (კონტრაქტში სახელებით). ცალკე ცხრილი არ აქვს: EF-ის owned type-ია და GlobalSettings-ის
//სტრიქონში ინახება
public sealed class DatabasesBackupFilesExchange
{
    //გაფართოებები, მაგალითად .down! და .up!
    public const int DownloadTempExtensionMaxLength = 50;
    public const int UploadTempExtensionMaxLength = 50;

    public DatabasesBackupFilesExchange(string? downloadTempExtension, string? uploadTempExtension,
        FileStorageId? exchangeFileStorageId, SmartSchemaId? exchangeSmartSchemaId, SmartSchemaId? localSmartSchemaId)
    {
        DownloadTempExtension = downloadTempExtension;
        UploadTempExtension = uploadTempExtension;
        ExchangeFileStorageId = exchangeFileStorageId;
        ExchangeSmartSchemaId = exchangeSmartSchemaId;
        LocalSmartSchemaId = localSmartSchemaId;
    }

    public string? DownloadTempExtension { get; private set; }
    public string? UploadTempExtension { get; private set; }
    public FileStorageId? ExchangeFileStorageId { get; private set; }
    public SmartSchemaId? ExchangeSmartSchemaId { get; private set; }
    public SmartSchemaId? LocalSmartSchemaId { get; private set; }
}

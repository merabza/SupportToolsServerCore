using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.FileStorages;

//ფაილსაცავი, SupportToolsParameters.FileStorages-ის ჩანაწერი: სახელი (dictionary-ის key), გზა, მომხმარებელი, პაროლი და
//FTP-ის ფაილების სიის წაკითხვის პარამეტრები. გზა URL-იცაა (ftp://…) და ლოკალური გზაც; სერვერი მას ინახავს ისე, როგორც
//მოვიდა, გარდაქმნა კლიენტის საქმეა. პაროლი საიდუმლოა: ღიად ინახება (README G2), მაგრამ არსად იბეჭდება
public sealed class FileStorage : VersionedEntity<FileStorageId>
{
    public const int NameMaxLength = 100;

    //Windows-ის MAX_PATH. FTP-ის მისამართიც ეტევა
    public const int FileStoragePathMaxLength = 260;

    public const int UserNameMaxLength = 128;
    public const int PasswordMaxLength = 256;

    public FileStorage(FileStorageId id, string name, string? fileStoragePath, string? userName, string? password,
        int fileNameMaxLength, int fileSizeSplitPositionInRow, int ftpSiteLsFileOffset, int version) : base(id,
        version)
    {
        Name = name;
        FileStoragePath = fileStoragePath;
        UserName = userName;
        Password = password;
        FileNameMaxLength = fileNameMaxLength;
        FileSizeSplitPositionInRow = fileSizeSplitPositionInRow;
        FtpSiteLsFileOffset = ftpSiteLsFileOffset;
    }

    public string Name { get; private set; }
    public string? FileStoragePath { get; private set; }
    public string? UserName { get; private set; }
    public string? Password { get; private set; }

    //ფაილსაცავში ფაილის სახელის მაქსიმალური სიგრძე (კლიენტის პარამეტრი, არა სვეტის სიგრძე)
    public int FileNameMaxLength { get; private set; }

    public int FileSizeSplitPositionInRow { get; private set; }
    public int FtpSiteLsFileOffset { get; private set; }

    public static FileStorage Create(string name, string? fileStoragePath, string? userName, string? password,
        int fileNameMaxLength, int fileSizeSplitPositionInRow, int ftpSiteLsFileOffset)
    {
        return new FileStorage(FileStorageId.CreateUnique(), name, fileStoragePath, userName, password,
            fileNameMaxLength, fileSizeSplitPositionInRow, ftpSiteLsFileOffset, EntityVersion.Initial);
    }

    //რედაქტირება ვერსიას ზრდის. სახელის შეცვლა (მაგალითად, მხოლოდ რეგისტრის) იგივე ჩანაწერის განახლებაა
    public void Update(string name, string? fileStoragePath, string? userName, string? password,
        int fileNameMaxLength, int fileSizeSplitPositionInRow, int ftpSiteLsFileOffset)
    {
        Name = name;
        FileStoragePath = fileStoragePath;
        UserName = userName;
        Password = password;
        FileNameMaxLength = fileNameMaxLength;
        FileSizeSplitPositionInRow = fileSizeSplitPositionInRow;
        FtpSiteLsFileOffset = ftpSiteLsFileOffset;
        IncrementVersion();
    }
}

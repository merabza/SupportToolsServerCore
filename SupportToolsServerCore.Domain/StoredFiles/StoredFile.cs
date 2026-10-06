using System;
using System.Security.Cryptography;
using System.Text;
using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.StoredFiles;

//საიდუმლო ფაილი (README G2, §4.1): ტექსტური ფაილი, რომელზეც რეესტრი მიუთითებს (მაგალითად,
//ServerInfo.AppSettingsJsonSourceFileName). Path კანონიკური გზაა (README G3), Windows-ის აბსოლუტური ფორმით, და
//ჩანაწერის გასაღებია, რეგისტრის გარეშე. შიგთავსი ღიად ინახება, მაგრამ არსად იბეჭდება. Sha256 და Length შიგთავსიდან
//ითვლება, ამიტომ ფაილების სიას შიგთავსის წაკითხვა არ სჭირდება
public sealed class StoredFile : VersionedEntity<StoredFileId>
{
    //Windows-ის MAX_PATH-ზე (260) მეტია, long path-ებისთვის, და 450-ზე ნაკლები, რომ უნიკალური ინდექსის გასაღები
    //(nvarchar, 2 ბაიტი სიმბოლოზე) SQL Server-ის 900 ბაიტში ჩაეტიოს
    public const int PathMaxLength = 400;

    //SHA-256-ის 32 ბაიტი hex-ად
    public const int Sha256Length = 64;

    public StoredFile(StoredFileId id, string path, string content, string sha256, int length, DateTime updatedAtUtc,
        int version) : base(id, version)
    {
        Path = path;
        Content = content;
        Sha256 = sha256;
        Length = length;
        UpdatedAtUtc = updatedAtUtc;
    }

    public string Path { get; private set; }
    public string Content { get; private set; }

    //შიგთავსის UTF-8 ბაიტების (BOM-ის გარეშე) SHA-256, hex დიდი ასოებით. კლიენტი (C6) ლოკალურ ფაილს იმავე წესით ჰეშავს
    public string Sha256 { get; private set; }

    //შიგთავსის UTF-8 ბაიტების რაოდენობა: იგივე ბაიტები, რომლებზეც Sha256 ითვლება
    public int Length { get; private set; }

    //ბოლო შექმნის ან განახლების დრო, UTC
    public DateTime UpdatedAtUtc { get; private set; }

    public static StoredFile Create(string path, string content, DateTime updatedAtUtc)
    {
        return new StoredFile(StoredFileId.CreateUnique(), path, content, ComputeSha256(content),
            Encoding.UTF8.GetByteCount(content), updatedAtUtc, EntityVersion.Initial);
    }

    //განახლება ვერსიას ზრდის. გზის შეცვლა (მხოლოდ რეგისტრის) იგივე ჩანაწერის განახლებაა
    public void Update(string path, string content, DateTime updatedAtUtc)
    {
        Path = path;
        Content = content;
        Sha256 = ComputeSha256(content);
        Length = Encoding.UTF8.GetByteCount(content);
        UpdatedAtUtc = updatedAtUtc;
        IncrementVersion();
    }

    private static string ComputeSha256(string content)
    {
        return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(content)));
    }
}

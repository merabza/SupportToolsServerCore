using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.DotnetTools;

//dotnet-ის ხელსაწყო, SupportToolsParameters.DotnetTools-ის ჩანაწერის საერთო ნაწილი: სახელი (dictionary-ის key),
//NuGet-ის პაკეტის Id, მაქსიმალური ვერსია (ცარიელი ნიშნავს ბოლო ვერსიას) და აღწერა. დაყენებული და ბოლო ვერსია და
//ბრძანების სახელი კომპიუტერისაა, ამიტომ სერვერზე არ ინახება
public sealed class DotnetTool : VersionedEntity<DotnetToolId>
{
    //სახელად პაკეტის Id-იც შეიძლება ეწეროს
    public const int NameMaxLength = 100;

    //NuGet-ის პაკეტის Id-ის მაქსიმალური სიგრძე
    public const int PackageIdMaxLength = 100;

    //NuGet-ის ვერსიის სტრიქონის მაქსიმალური სიგრძე. ვერსიების დიაპაზონიც (მაგალითად, [8.0,9.0)) ეტევა
    public const int MaxVersionMaxLength = 64;

    public const int DescriptionMaxLength = 255;

    public DotnetTool(DotnetToolId id, string name, string packageId, string? maxVersion, string? description,
        int version) : base(id, version)
    {
        Name = name;
        PackageId = packageId;
        MaxVersion = maxVersion;
        Description = description;
    }

    public string Name { get; private set; }
    public string PackageId { get; private set; }
    public string? MaxVersion { get; private set; }
    public string? Description { get; private set; }

    public static DotnetTool Create(string name, string packageId, string? maxVersion, string? description)
    {
        return new DotnetTool(DotnetToolId.CreateUnique(), name, packageId, maxVersion, description,
            EntityVersion.Initial);
    }

    //რედაქტირება ვერსიას ზრდის. სახელის შეცვლა (მაგალითად, მხოლოდ რეგისტრის) იგივე ჩანაწერის განახლებაა
    public void Update(string name, string packageId, string? maxVersion, string? description)
    {
        Name = name;
        PackageId = packageId;
        MaxVersion = maxVersion;
        Description = description;
        IncrementVersion();
    }
}

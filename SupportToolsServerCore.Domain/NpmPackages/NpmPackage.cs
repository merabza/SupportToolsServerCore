using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.NpmPackages;

//npm-ის პაკეტი, SupportToolsParameters.NpmPackages-ის ჩანაწერი: სახელი (react-router-dom, @reduxjs/toolkit, ...) და
//აღწერა. npm-ის პაკეტის სახელი 214 სიმბოლომდეა
public sealed class NpmPackage : VersionedEntity<NpmPackageId>
{
    public const int NameMaxLength = 214;
    public const int DescriptionMaxLength = 255;

    public NpmPackage(NpmPackageId id, string name, string? description, int version) : base(id, version)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; private set; }
    public string? Description { get; private set; }

    public static NpmPackage Create(string name, string? description)
    {
        return new NpmPackage(NpmPackageId.CreateUnique(), name, description, EntityVersion.Initial);
    }

    //რედაქტირება ვერსიას ზრდის. სახელის შეცვლა (მაგალითად, მხოლოდ რეგისტრის) იგივე ჩანაწერის განახლებაა
    public void Update(string name, string? description)
    {
        Name = name;
        Description = description;
        IncrementVersion();
    }
}

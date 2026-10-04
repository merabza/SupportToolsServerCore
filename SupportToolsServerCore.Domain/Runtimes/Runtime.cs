using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.Runtimes;

//.NET-ის Runtime Identifier (RID: win-x64, linux-x64, ...), SupportToolsParameters.RunTimes-ის ჩანაწერი: სახელი და
//აღწერა
public sealed class Runtime : VersionedEntity<RuntimeId>
{
    public const int NameMaxLength = 50;
    public const int DescriptionMaxLength = 255;

    public Runtime(RuntimeId id, string name, string? description, int version) : base(id, version)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; private set; }
    public string? Description { get; private set; }

    public static Runtime Create(string name, string? description)
    {
        return new Runtime(RuntimeId.CreateUnique(), name, description, EntityVersion.Initial);
    }

    //რედაქტირება ვერსიას ზრდის. სახელის შეცვლა (მაგალითად, მხოლოდ რეგისტრის) იგივე ჩანაწერის განახლებაა
    public void Update(string name, string? description)
    {
        Name = name;
        Description = description;
        IncrementVersion();
    }
}

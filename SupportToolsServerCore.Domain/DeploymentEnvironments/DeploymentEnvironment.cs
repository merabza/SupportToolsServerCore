using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.DeploymentEnvironments;

//გარემო (Prod, Stage, Test, Dev, ...), SupportToolsParameters.Environments-ის ჩანაწერი: სახელი და აღწერა.
//კლასის სახელი Environment არ არის, რადგან ის System.Environment-ს ემთხვევა. ცხრილი Environments-ია
public sealed class DeploymentEnvironment : VersionedEntity<DeploymentEnvironmentId>
{
    public const int NameMaxLength = 50;
    public const int DescriptionMaxLength = 255;

    public DeploymentEnvironment(DeploymentEnvironmentId id, string name, string? description, int version) : base(id,
        version)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; private set; }
    public string? Description { get; private set; }

    public static DeploymentEnvironment Create(string name, string? description)
    {
        return new DeploymentEnvironment(DeploymentEnvironmentId.CreateUnique(), name, description,
            EntityVersion.Initial);
    }

    //რედაქტირება ვერსიას ზრდის. სახელის შეცვლა (მაგალითად, მხოლოდ რეგისტრის) იგივე ჩანაწერის განახლებაა
    public void Update(string name, string? description)
    {
        Name = name;
        Description = description;
        IncrementVersion();
    }
}

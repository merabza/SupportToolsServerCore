using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.Projects;

//პროექტის route კლასის აღწერა, კლიენტის RouteClasses dictionary-ის ჩანაწერი (RouteClassModel). Name dictionary-ის
//key-ა და პროექტში უნიკალურია. ApiVersion კლიენტის Version-ია: სახელი იმიტომ განსხვავდება, რომ აგრეგატის Version-ში არ
//აგერიოს. ტექსტური ველები კლიენტში შეიძლება ცარიელი იყოს, ამიტომ არასავალდებულოა. აგრეგატის შვილია, ამიტომ საკუთარი
//ვერსია არ აქვს (README G7)
public sealed class ProjectRouteClass : Entity<ProjectRouteClassId>
{
    public const int NameMaxLength = 100;
    public const int RootMaxLength = 50;
    public const int ApiVersionMaxLength = 50;
    public const int BaseMaxLength = 256;

    public ProjectRouteClass(ProjectRouteClassId id, string name, string? root, string? apiVersion, string? @base) :
        base(id)
    {
        Name = name;
        Root = root;
        ApiVersion = apiVersion;
        Base = @base;
    }

    public string Name { get; private set; }
    public string? Root { get; private set; }
    public string? ApiVersion { get; private set; }
    public string? Base { get; private set; }

    public static ProjectRouteClass Create(string name, string? root, string? apiVersion, string? @base)
    {
        return new ProjectRouteClass(ProjectRouteClassId.CreateUnique(), name, root, apiVersion, @base);
    }
}

using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.Projects;

//პროექტის endpoint-ის აღწერა, კლიენტის Endpoints dictionary-ის ჩანაწერი (EndpointModel). Name dictionary-ის key-ა და
//პროექტში უნიკალურია. HttpMethod და EndpointType კლიენტის EHttpMethod-ისა და EEndpointType-ის სახელებია; სერვერი
//enum-ებს არ იცნობს და მხოლოდ სიგრძეს ამოწმებს. ტექსტური ველები კლიენტში შეიძლება ცარიელი იყოს, ამიტომ
//არასავალდებულოა. აგრეგატის შვილია, ამიტომ საკუთარი ვერსია არ აქვს (README G7)
public sealed class ProjectEndpoint : Entity<ProjectEndpointId>
{
    public const int NameMaxLength = 100;
    public const int EndpointNameMaxLength = 100;
    public const int EndpointRouteMaxLength = 256;
    public const int HttpMethodMaxLength = 50;
    public const int EndpointTypeMaxLength = 50;
    public const int ReturnTypeMaxLength = 256;

    public ProjectEndpoint(ProjectEndpointId id, string name, string? endpointName, string? endpointRoute,
        bool requireAuthorization, string httpMethod, string endpointType, string? returnType,
        bool sendMessageToCurrentUser) : base(id)
    {
        Name = name;
        EndpointName = endpointName;
        EndpointRoute = endpointRoute;
        RequireAuthorization = requireAuthorization;
        HttpMethod = httpMethod;
        EndpointType = endpointType;
        ReturnType = returnType;
        SendMessageToCurrentUser = sendMessageToCurrentUser;
    }

    public string Name { get; private set; }
    public string? EndpointName { get; private set; }
    public string? EndpointRoute { get; private set; }
    public bool RequireAuthorization { get; private set; }
    public string HttpMethod { get; private set; }
    public string EndpointType { get; private set; }
    public string? ReturnType { get; private set; }
    public bool SendMessageToCurrentUser { get; private set; }

    public static ProjectEndpoint Create(string name, string? endpointName, string? endpointRoute,
        bool requireAuthorization, string httpMethod, string endpointType, string? returnType,
        bool sendMessageToCurrentUser)
    {
        return new ProjectEndpoint(ProjectEndpointId.CreateUnique(), name, endpointName, endpointRoute,
            requireAuthorization, httpMethod, endpointType, returnType, sendMessageToCurrentUser);
    }
}

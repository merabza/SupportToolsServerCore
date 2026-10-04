using SupportToolsServerCore.Domain.ApiClients;
using SupportToolsServerCore.Domain.Primitives;
using SupportToolsServerCore.Domain.Runtimes;

namespace SupportToolsServerCore.Domain.Servers;

//სერვერი, SupportToolsParameters.Servers-ის ჩანაწერი. WebAgentId და WebAgentInstallerId ვებაგენტებად გამოყენებული
//ApiClient-ებია, RuntimeId კი სერვერის Runtime (კონტრაქტში სამივე სახელით გადაიცემა). FilesUserName და
//FilesUsersGroupName სამიზნე სერვერის OS ანგარიშებია, ServerSideDownloadFolder და ServerSideDeployFolder კი მისი
//გზები, რომლებსაც კლიენტის გზების გარდაქმნა არ ეხება. IsLocal კომპიუტერზეა დამოკიდებული და აქ არ ინახება (README G6).
//სახელი AppSettings-ის დაშიფვრის გასაღების ნაწილია, ამიტომ სერვერის გადარქმევა წაშლა და ახლის შექმნაა
public sealed class Server : VersionedEntity<ServerId>
{
    public const int NameMaxLength = 100;

    //OS-ის ანგარიშისა და ჯგუფის სახელები, მომხმარებლის სახელების სიგრძით
    public const int FilesUserNameMaxLength = 128;
    public const int FilesUsersGroupNameMaxLength = 128;

    public const int ServerSideDownloadFolderMaxLength = 260;
    public const int ServerSideDeployFolderMaxLength = 260;

    public Server(ServerId id, string name, ApiClientId? webAgentId, ApiClientId? webAgentInstallerId,
        string? filesUserName, string? filesUsersGroupName, RuntimeId? runtimeId, string? serverSideDownloadFolder,
        string? serverSideDeployFolder, int version) : base(id, version)
    {
        Name = name;
        WebAgentId = webAgentId;
        WebAgentInstallerId = webAgentInstallerId;
        FilesUserName = filesUserName;
        FilesUsersGroupName = filesUsersGroupName;
        RuntimeId = runtimeId;
        ServerSideDownloadFolder = serverSideDownloadFolder;
        ServerSideDeployFolder = serverSideDeployFolder;
    }

    public string Name { get; private set; }
    public ApiClientId? WebAgentId { get; private set; }
    public ApiClientId? WebAgentInstallerId { get; private set; }
    public string? FilesUserName { get; private set; }
    public string? FilesUsersGroupName { get; private set; }
    public RuntimeId? RuntimeId { get; private set; }
    public string? ServerSideDownloadFolder { get; private set; }
    public string? ServerSideDeployFolder { get; private set; }

    public static Server Create(string name, ApiClientId? webAgentId, ApiClientId? webAgentInstallerId,
        string? filesUserName, string? filesUsersGroupName, RuntimeId? runtimeId, string? serverSideDownloadFolder,
        string? serverSideDeployFolder)
    {
        return new Server(ServerId.CreateUnique(), name, webAgentId, webAgentInstallerId, filesUserName,
            filesUsersGroupName, runtimeId, serverSideDownloadFolder, serverSideDeployFolder, EntityVersion.Initial);
    }

    //რედაქტირება ვერსიას ზრდის. სახელის მხოლოდ რეგისტრის შეცვლა იგივე ჩანაწერის განახლებაა: დაშიფვრის გასაღებში
    //სახელი Capitalize-ით შედის, ანუ რეგისტრი იქ არ მოქმედებს
    public void Update(string name, ApiClientId? webAgentId, ApiClientId? webAgentInstallerId, string? filesUserName,
        string? filesUsersGroupName, RuntimeId? runtimeId, string? serverSideDownloadFolder,
        string? serverSideDeployFolder)
    {
        Name = name;
        WebAgentId = webAgentId;
        WebAgentInstallerId = webAgentInstallerId;
        FilesUserName = filesUserName;
        FilesUsersGroupName = filesUsersGroupName;
        RuntimeId = runtimeId;
        ServerSideDownloadFolder = serverSideDownloadFolder;
        ServerSideDeployFolder = serverSideDeployFolder;
        IncrementVersion();
    }
}

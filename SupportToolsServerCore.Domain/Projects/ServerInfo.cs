using System.Collections.Generic;
using SupportToolsServerCore.Domain.ApiClients;
using SupportToolsServerCore.Domain.DeploymentEnvironments;
using SupportToolsServerCore.Domain.Primitives;
using SupportToolsServerCore.Domain.Servers;

namespace SupportToolsServerCore.Domain.Projects;

//პროექტის პარამეტრები ერთ სერვერსა და გარემოში, კლიენტის ServerInfos dictionary-ის ჩანაწერი (ServerInfoModel).
//კლიენტის key ნატურალური გასაღები არ არის (ზოგი GUID-ია, ზოგი "Server|Env") და სერვერზე არ მოდის: გასაღებია სერვერი
//და გარემო, რომლებიც პროექტში ერთად ერთხელ გვხვდება. ServerId, EnvironmentId და WebAgentForCheckId სხვა აგრეგატების
//ჩანაწერებია (კონტრაქტში სახელებით). ServerSidePort 0 ნიშნავს, რომ პორტი არ არის. AppSettings-ის ფაილები კანონიკური
//გზებია (README G3): სერვერი მათ ინახავს ისე, როგორც მოვიდა. ServiceUserName სერვისის OS ანგარიშია და საიდუმლო არ არის.
//ბაზის პარამეტრები პროექტის ბაზის პარამეტრების ტიპისაა (owned, ServerInfo-ს სტრიქონში). აგრეგატის შვილია, ამიტომ
//საკუთარი ვერსია არ აქვს (README G7): მისი ნებისმიერი ცვლილება პროექტის ვერსიას ზრდის
public sealed class ServerInfo : Entity<ServerInfoId>
{
    //API-ის ვერსიის იდენტიფიკატორი (v1)
    public const int ApiVersionIdMaxLength = 50;

    //კანონიკური გზები (README G3), Windows-ის MAX_PATH
    public const int PathMaxLength = 260;

    //OS-ის ანგარიში, სერვერის FilesUserName-ის მსგავსად
    public const int ServiceUserNameMaxLength = 128;

    private readonly List<ServerInfoAllowedTool> _allowedTools = [];

    //ბაზის პარამეტრებსა და ინსტრუმენტებს EF კონსტრუქტორით ვერ გადასცემს და ჩანაწერის წაკითხვისას თვითონ ავსებს
    public ServerInfo(ServerInfoId id, ServerId serverId, DeploymentEnvironmentId environmentId,
        ApiClientId? webAgentForCheckId, int serverSidePort, string? apiVersionId,
        string? appSettingsJsonSourceFileName, string? appSettingsEncodedJsonFileName, string? serviceUserName) :
        base(id)
    {
        ServerId = serverId;
        EnvironmentId = environmentId;
        WebAgentForCheckId = webAgentForCheckId;
        ServerSidePort = serverSidePort;
        ApiVersionId = apiVersionId;
        AppSettingsJsonSourceFileName = appSettingsJsonSourceFileName;
        AppSettingsEncodedJsonFileName = appSettingsEncodedJsonFileName;
        ServiceUserName = serviceUserName;
    }

    public ServerId ServerId { get; private set; }
    public DeploymentEnvironmentId EnvironmentId { get; private set; }

    //ვებაგენტი (ApiClient), რომლითაც სერვერზე დაყენებული პროგრამის ვერსია მოწმდება
    public ApiClientId? WebAgentForCheckId { get; private set; }

    public int ServerSidePort { get; private set; }
    public string? ApiVersionId { get; private set; }
    public string? AppSettingsJsonSourceFileName { get; private set; }
    public string? AppSettingsEncodedJsonFileName { get; private set; }
    public string? ServiceUserName { get; private set; }
    public DatabaseParameters? CurrentDatabaseParameters { get; private set; }
    public DatabaseParameters? NewDatabaseParameters { get; private set; }
    public IReadOnlyList<ServerInfoAllowedTool> AllowedTools => _allowedTools;

    //რედაქტირების მეთოდი არ აქვს: პროექტის განახლება ServerInfo-ების სიას ახლით ანაცვლებს (README G7)
    public static ServerInfo Create(ServerId serverId, DeploymentEnvironmentId environmentId,
        ApiClientId? webAgentForCheckId, int serverSidePort, string? apiVersionId,
        string? appSettingsJsonSourceFileName, string? appSettingsEncodedJsonFileName, string? serviceUserName,
        DatabaseParameters? currentDatabaseParameters, DatabaseParameters? newDatabaseParameters,
        IEnumerable<ServerInfoAllowedTool> allowedTools)
    {
        var serverInfo = new ServerInfo(ServerInfoId.CreateUnique(), serverId, environmentId, webAgentForCheckId,
            serverSidePort, apiVersionId, appSettingsJsonSourceFileName, appSettingsEncodedJsonFileName,
            serviceUserName)
        {
            CurrentDatabaseParameters = currentDatabaseParameters, NewDatabaseParameters = newDatabaseParameters
        };
        serverInfo._allowedTools.AddRange(allowedTools);
        return serverInfo;
    }
}

using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.ApiClients;

//API კლიენტი, SupportToolsParameters.ApiClients-ის ჩანაწერი: სახელი (dictionary-ის key), სერვერის მისამართი და API key.
//გასაღები საიდუმლოა: ღიად ინახება (README G2), მაგრამ არსად იბეჭდება. ApiClient-ს სხვა აგრეგატები სახელით მიმართავენ
//(DatabaseServerConnection.DbWebAgentName, Server.WebAgentName და WebAgentInstallerName), ამიტომ მისი წაშლა
//მომხმარებლების შემოწმების შემდეგ ხდება
public sealed class ApiClient : VersionedEntity<ApiClientId>
{
    public const int NameMaxLength = 100;
    public const int ServerMaxLength = 256;
    public const int ApiKeyMaxLength = 256;

    public ApiClient(ApiClientId id, string name, string? server, string? apiKey, int version) : base(id, version)
    {
        Name = name;
        Server = server;
        ApiKey = apiKey;
    }

    public string Name { get; private set; }
    public string? Server { get; private set; }
    public string? ApiKey { get; private set; }

    public static ApiClient Create(string name, string? server, string? apiKey)
    {
        return new ApiClient(ApiClientId.CreateUnique(), name, server, apiKey, EntityVersion.Initial);
    }

    //რედაქტირება ვერსიას ზრდის. სახელის შეცვლა (მაგალითად, მხოლოდ რეგისტრის) იგივე ჩანაწერის განახლებაა
    public void Update(string name, string? server, string? apiKey)
    {
        Name = name;
        Server = server;
        ApiKey = apiKey;
        IncrementVersion();
    }
}

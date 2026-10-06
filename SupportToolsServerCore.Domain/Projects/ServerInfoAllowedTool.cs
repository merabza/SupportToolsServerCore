using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.Projects;

//სერვერზე პროექტისთვის დაშვებული ინსტრუმენტი (კლიენტის ServerInfoModel.AllowToolsList). ToolName კლიენტის
//EProjectServerTools-ის სახელია; სერვერი enum-ს არ იცნობს და მხოლოდ სიგრძეს ამოწმებს. ServerInfo-ში ერთხელ გვხვდება.
//აგრეგატის შვილია, ამიტომ საკუთარი ვერსია არ აქვს (README G7)
public sealed class ServerInfoAllowedTool : Entity<ServerInfoAllowedToolId>
{
    public const int ToolNameMaxLength = 50;

    public ServerInfoAllowedTool(ServerInfoAllowedToolId id, string toolName) : base(id)
    {
        ToolName = toolName;
    }

    public string ToolName { get; private set; }

    public static ServerInfoAllowedTool Create(string toolName)
    {
        return new ServerInfoAllowedTool(ServerInfoAllowedToolId.CreateUnique(), toolName);
    }
}

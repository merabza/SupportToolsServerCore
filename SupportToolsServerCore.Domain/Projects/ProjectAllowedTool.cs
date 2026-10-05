using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.Projects;

//პროექტისთვის დაშვებული ინსტრუმენტი (კლიენტის AllowToolsList). ToolName კლიენტის EProjectTools-ის სახელია; სერვერი
//enum-ს არ იცნობს და მხოლოდ სიგრძეს ამოწმებს. პროექტში ერთხელ გვხვდება. აგრეგატის შვილია, ამიტომ საკუთარი ვერსია არ
//აქვს (README G7)
public sealed class ProjectAllowedTool : Entity<ProjectAllowedToolId>
{
    public const int ToolNameMaxLength = 50;

    public ProjectAllowedTool(ProjectAllowedToolId id, string toolName) : base(id)
    {
        ToolName = toolName;
    }

    public string ToolName { get; private set; }

    public static ProjectAllowedTool Create(string toolName)
    {
        return new ProjectAllowedTool(ProjectAllowedToolId.CreateUnique(), toolName);
    }
}

using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.Projects;

//პროექტის ზედმეტი ფაილის სახელი ან ნიღაბი (კლიენტის RedundantFileNames): პროგრამის გამოქვეყნებისას ასეთი ფაილები
//იშლება. პროექტში ერთხელ გვხვდება. აგრეგატის შვილია, ამიტომ საკუთარი ვერსია არ აქვს (README G7)
public sealed class ProjectRedundantFile : Entity<ProjectRedundantFileId>
{
    //Windows-ის MAX_PATH
    public const int FileNameMaxLength = 260;

    public ProjectRedundantFile(ProjectRedundantFileId id, string fileName) : base(id)
    {
        FileName = fileName;
    }

    public string FileName { get; private set; }

    public static ProjectRedundantFile Create(string fileName)
    {
        return new ProjectRedundantFile(ProjectRedundantFileId.CreateUnique(), fileName);
    }
}

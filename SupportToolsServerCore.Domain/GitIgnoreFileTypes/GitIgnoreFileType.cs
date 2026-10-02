using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.GitIgnoreFileTypes;

//ჩანაწერი მხოლოდ კონსტრუქტორით იქმნება. ახალს EntityVersion.Initial ეძლევა, განახლებისას კი რეპოზიტორის Update-ს
//ახალი ეგზემპლარი გადაეცემა შენახული Id-ითა და შენახული Version + 1-ით
public class GitIgnoreFileType : VersionedEntity<GitIgnoreFileTypeId>
{
    public const int NameMaxLength = 50;
    public const int ContentMaxLength = 16384;

    public GitIgnoreFileType(GitIgnoreFileTypeId id, string name, string content, int version) : base(id, version)
    {
        Name = name;
        Content = content;
    }

    public string Name { get; private set; }
    public string Content { get; private set; }
}

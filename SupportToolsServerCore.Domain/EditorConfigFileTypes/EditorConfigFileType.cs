using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.EditorConfigFileTypes;

//ჩანაწერი მხოლოდ კონსტრუქტორით იქმნება. ახალს EntityVersion.Initial ეძლევა, განახლებისას კი რეპოზიტორის Update-ს
//ახალი ეგზემპლარი გადაეცემა შენახული Id-ითა და შენახული Version + 1-ით
public class EditorConfigFileType : VersionedEntity<EditorConfigFileTypeId>
{
    public const int NameMaxLength = 50;

    //.editorconfig ფაილები .gitignore ფაილებზე გაცილებით დიდია: default შაბლონი უკვე 16000 სიმბოლომდეა
    public const int ContentMaxLength = 65536;

    public EditorConfigFileType(EditorConfigFileTypeId id, string name, string content, int version) : base(id, version)
    {
        Name = name;
        Content = content;
    }

    public string Name { get; private set; }
    public string Content { get; private set; }
}

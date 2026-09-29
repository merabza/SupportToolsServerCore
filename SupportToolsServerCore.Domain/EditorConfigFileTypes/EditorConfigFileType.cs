using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.EditorConfigFileTypes;

public class EditorConfigFileType : Entity<EditorConfigFileTypeId>
{
    public const int NameMaxLength = 50;

    //.editorconfig ფაილები .gitignore ფაილებზე გაცილებით დიდია: default შაბლონი უკვე 16000 სიმბოლომდეა
    public const int ContentMaxLength = 65536;

    public EditorConfigFileType(EditorConfigFileTypeId id, string name, string content) : base(id)
    {
        Name = name;
        Content = content;
    }

    public string Name { get; private set; }
    public string Content { get; private set; }
}

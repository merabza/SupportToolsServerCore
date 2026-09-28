using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.GitIgnoreFileTypes;

public class GitIgnoreFileType : Entity<GitIgnoreFileTypeId>
{
    public const int NameMaxLength = 50;
    public const int ContentMaxLength = 16384;

    public GitIgnoreFileType(GitIgnoreFileTypeId id, string name, string content) : base(id)
    {
        Name = name;
        Content = content;
    }

    public string Name { get; private set; }
    public string Content { get; private set; }
}

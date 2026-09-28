using SupportToolsServerCore.Domain.GitIgnoreFileTypes;
using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.GitRepos;

public class GitRepo : Entity<GitRepoId>
{
    public const int NameMaxLength = 50;
    public const int AddressMaxLength = 256;
    public const int FolderNameMaxLength = 100;

    public GitRepo(GitRepoId id, string name, string address, string folderName,
        GitIgnoreFileTypeId gitIgnoreFileTypeId) : base(id)
    {
        Name = name;
        Address = address;
        FolderName = folderName;
        GitIgnoreFileTypeId = gitIgnoreFileTypeId;
    }

    public string Name { get; private set; }
    public string Address { get; private set; }
    public string FolderName { get; private set; }
    public GitIgnoreFileTypeId GitIgnoreFileTypeId { get; private set; }
}

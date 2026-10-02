using SupportToolsServerCore.Domain.GitIgnoreFileTypes;
using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.GitRepos;

public class GitRepo : VersionedEntity<GitRepoId>
{
    public const int NameMaxLength = 50;
    public const int AddressMaxLength = 256;
    public const int FolderNameMaxLength = 100;

    public GitRepo(GitRepoId id, string name, string address, string folderName,
        GitIgnoreFileTypeId gitIgnoreFileTypeId, int version) : base(id, version)
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

    //ახალი რეპოზიტორიის შექმნა GitRepoAddedDomainEvent მოვლენას აგენერირებს
    public static GitRepo Create(string name, string address, string folderName,
        GitIgnoreFileTypeId gitIgnoreFileTypeId)
    {
        var gitRepo = new GitRepo(GitRepoId.CreateUnique(), name, address, folderName, gitIgnoreFileTypeId,
            EntityVersion.Initial);
        gitRepo.Raise(new GitRepoAddedDomainEvent(gitRepo.Id, name, address, folderName));
        return gitRepo;
    }

    //არსებული რეპოზიტორიის რედაქტირება ვერსიას ზრდის და GitRepoUpdatedDomainEvent მოვლენას აგენერირებს
    public void Update(string name, string address, string folderName, GitIgnoreFileTypeId gitIgnoreFileTypeId)
    {
        Name = name;
        Address = address;
        FolderName = folderName;
        GitIgnoreFileTypeId = gitIgnoreFileTypeId;
        IncrementVersion();
        Raise(new GitRepoUpdatedDomainEvent(Id, name, address, folderName));
    }
}

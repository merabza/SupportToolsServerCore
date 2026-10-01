using SystemTools.SharedKernel;

namespace SupportToolsServerCore.Domain.GitRepos;

public sealed record GitRepoAddedDomainEvent(GitRepoId GitRepoId, string Name, string Address, string FolderName)
    : IDomainEvent;

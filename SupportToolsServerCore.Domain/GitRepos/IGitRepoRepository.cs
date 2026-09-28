using System.Threading;
using System.Threading.Tasks;
using SupportToolsServerCore.Domain.Sync;

namespace SupportToolsServerCore.Domain.GitRepos;

public interface IGitRepoRepository : ICrudRepository<GitRepo, GitRepoId>
{
    Task<GitRepo?> GetByName(string name, CancellationToken cancellationToken);
}

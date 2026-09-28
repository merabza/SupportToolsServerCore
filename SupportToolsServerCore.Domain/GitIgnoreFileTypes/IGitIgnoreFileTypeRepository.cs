using System.Threading;
using System.Threading.Tasks;
using SupportToolsServerCore.Domain.Sync;

namespace SupportToolsServerCore.Domain.GitIgnoreFileTypes;

public interface IGitIgnoreFileTypeRepository : ICrudRepository<GitIgnoreFileType, GitIgnoreFileTypeId>
{
    Task<GitIgnoreFileType?> GetByName(string name, CancellationToken cancellationToken);
}

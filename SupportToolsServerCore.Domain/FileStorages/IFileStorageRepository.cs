using System.Threading;
using System.Threading.Tasks;
using SupportToolsServerCore.Domain.Sync;

namespace SupportToolsServerCore.Domain.FileStorages;

public interface IFileStorageRepository : ICrudRepository<FileStorage, FileStorageId>
{
    //სახელი რეგისტრის გარეშე ედრება (OrdinalIgnoreCase), ბაზის collation-ისგან დამოუკიდებლად
    Task<FileStorage?> GetByName(string name, CancellationToken cancellationToken);
}

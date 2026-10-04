using System.Threading;
using System.Threading.Tasks;
using SupportToolsServerCore.Domain.Sync;

namespace SupportToolsServerCore.Domain.Runtimes;

public interface IRuntimeRepository : ICrudRepository<Runtime, RuntimeId>
{
    //სახელი რეგისტრის გარეშე ედრება (OrdinalIgnoreCase), ბაზის collation-ისგან დამოუკიდებლად
    Task<Runtime?> GetByName(string name, CancellationToken cancellationToken);
}

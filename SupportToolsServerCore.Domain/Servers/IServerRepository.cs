using System.Threading;
using System.Threading.Tasks;
using SupportToolsServerCore.Domain.Sync;

namespace SupportToolsServerCore.Domain.Servers;

public interface IServerRepository : ICrudRepository<Server, ServerId>
{
    //სახელი რეგისტრის გარეშე ედრება (OrdinalIgnoreCase), ბაზის collation-ისგან დამოუკიდებლად
    Task<Server?> GetByName(string name, CancellationToken cancellationToken);
}

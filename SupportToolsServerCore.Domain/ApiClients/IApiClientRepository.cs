using System.Threading;
using System.Threading.Tasks;
using SupportToolsServerCore.Domain.Sync;

namespace SupportToolsServerCore.Domain.ApiClients;

public interface IApiClientRepository : ICrudRepository<ApiClient, ApiClientId>
{
    //სახელი რეგისტრის გარეშე ედრება (OrdinalIgnoreCase), ბაზის collation-ისგან დამოუკიდებლად
    Task<ApiClient?> GetByName(string name, CancellationToken cancellationToken);
}

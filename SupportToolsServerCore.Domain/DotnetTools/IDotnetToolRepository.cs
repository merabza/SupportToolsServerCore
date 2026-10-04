using System.Threading;
using System.Threading.Tasks;
using SupportToolsServerCore.Domain.Sync;

namespace SupportToolsServerCore.Domain.DotnetTools;

public interface IDotnetToolRepository : ICrudRepository<DotnetTool, DotnetToolId>
{
    //სახელი რეგისტრის გარეშე ედრება (OrdinalIgnoreCase), ბაზის collation-ისგან დამოუკიდებლად
    Task<DotnetTool?> GetByName(string name, CancellationToken cancellationToken);
}

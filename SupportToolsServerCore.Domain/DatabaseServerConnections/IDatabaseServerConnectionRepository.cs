using System.Threading;
using System.Threading.Tasks;
using SupportToolsServerCore.Domain.Sync;

namespace SupportToolsServerCore.Domain.DatabaseServerConnections;

public interface IDatabaseServerConnectionRepository : ICrudRepository<DatabaseServerConnection,
    DatabaseServerConnectionId>
{
    //სახელი რეგისტრის გარეშე ედრება (OrdinalIgnoreCase), ბაზის collation-ისგან დამოუკიდებლად. კავშირი folders
    //set-ებით და თვალყურის დევნების გარეშე იკითხება
    Task<DatabaseServerConnection?> GetByName(string name, CancellationToken cancellationToken);

    //განახლებისთვის: კავშირი folders set-ებით და თვალყურის დევნებით იკითხება, რომ შენახვისას ჩანაცვლებული ნაკრებები
    //წაიშალოს (CLAUDE.md, Registry conventions)
    Task<DatabaseServerConnection?> GetByNameForUpdate(string name, CancellationToken cancellationToken);
}

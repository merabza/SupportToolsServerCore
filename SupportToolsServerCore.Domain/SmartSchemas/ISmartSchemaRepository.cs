using System.Threading;
using System.Threading.Tasks;
using SupportToolsServerCore.Domain.Sync;

namespace SupportToolsServerCore.Domain.SmartSchemas;

public interface ISmartSchemaRepository : ICrudRepository<SmartSchema, SmartSchemaId>
{
    //სახელი რეგისტრის გარეშე ედრება (OrdinalIgnoreCase), ბაზის collation-ისგან დამოუკიდებლად. სქემა დეტალებით და
    //თვალყურის დევნების გარეშე იკითხება
    Task<SmartSchema?> GetByName(string name, CancellationToken cancellationToken);

    //განახლებისთვის: სქემა დეტალებით და თვალყურის დევნებით იკითხება, რომ შენახვისას ჩანაცვლებული დეტალები წაიშალოს
    //(CLAUDE.md, Registry conventions)
    Task<SmartSchema?> GetByNameForUpdate(string name, CancellationToken cancellationToken);
}

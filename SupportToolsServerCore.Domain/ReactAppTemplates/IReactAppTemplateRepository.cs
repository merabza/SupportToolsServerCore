using System.Threading;
using System.Threading.Tasks;
using SupportToolsServerCore.Domain.Sync;

namespace SupportToolsServerCore.Domain.ReactAppTemplates;

public interface IReactAppTemplateRepository : ICrudRepository<ReactAppTemplate, ReactAppTemplateId>
{
    //სახელი რეგისტრის გარეშე ედრება (OrdinalIgnoreCase), ბაზის collation-ისგან დამოუკიდებლად
    Task<ReactAppTemplate?> GetByName(string name, CancellationToken cancellationToken);
}

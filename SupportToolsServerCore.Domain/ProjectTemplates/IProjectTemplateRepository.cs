using System.Threading;
using System.Threading.Tasks;
using SupportToolsServerCore.Domain.Sync;

namespace SupportToolsServerCore.Domain.ProjectTemplates;

public interface IProjectTemplateRepository : ICrudRepository<ProjectTemplate, ProjectTemplateId>
{
    //სახელი რეგისტრის გარეშე ედრება (OrdinalIgnoreCase), ბაზის collation-ისგან დამოუკიდებლად
    Task<ProjectTemplate?> GetByName(string name, CancellationToken cancellationToken);
}

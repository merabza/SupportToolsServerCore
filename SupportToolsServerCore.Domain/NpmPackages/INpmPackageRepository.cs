using System.Threading;
using System.Threading.Tasks;
using SupportToolsServerCore.Domain.Sync;

namespace SupportToolsServerCore.Domain.NpmPackages;

public interface INpmPackageRepository : ICrudRepository<NpmPackage, NpmPackageId>
{
    //სახელი რეგისტრის გარეშე ედრება (OrdinalIgnoreCase), ბაზის collation-ისგან დამოუკიდებლად
    Task<NpmPackage?> GetByName(string name, CancellationToken cancellationToken);
}

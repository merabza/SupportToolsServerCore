using System.Threading;
using System.Threading.Tasks;
using SupportToolsServerCore.Domain.Sync;

namespace SupportToolsServerCore.Domain.DeploymentEnvironments;

public interface IDeploymentEnvironmentRepository : ICrudRepository<DeploymentEnvironment, DeploymentEnvironmentId>
{
    //სახელი რეგისტრის გარეშე ედრება (OrdinalIgnoreCase), ბაზის collation-ისგან დამოუკიდებლად
    Task<DeploymentEnvironment?> GetByName(string name, CancellationToken cancellationToken);
}

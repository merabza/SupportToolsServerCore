using System.Threading;
using System.Threading.Tasks;
using SupportToolsServerCore.Domain.Sync;

namespace SupportToolsServerCore.Domain.Projects;

public interface IProjectRepository : ICrudRepository<Project, ProjectId>
{
    //სახელი რეგისტრის გარეშე ედრება (OrdinalIgnoreCase), ბაზის collation-ისგან დამოუკიდებლად. პროექტი შვილებით და
    //თვალყურის დევნების გარეშე იკითხება
    Task<Project?> GetByName(string name, CancellationToken cancellationToken);

    //განახლებისთვის: პროექტი შვილებით და თვალყურის დევნებით იკითხება, რომ შენახვისას ჩანაცვლებული შვილები წაიშალოს
    //(CLAUDE.md, Registry conventions)
    Task<Project?> GetByNameForUpdate(string name, CancellationToken cancellationToken);
}

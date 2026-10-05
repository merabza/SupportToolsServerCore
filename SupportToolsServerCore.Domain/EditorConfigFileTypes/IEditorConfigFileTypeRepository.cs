using System.Threading;
using System.Threading.Tasks;
using SupportToolsServerCore.Domain.Sync;

namespace SupportToolsServerCore.Domain.EditorConfigFileTypes;

public interface IEditorConfigFileTypeRepository : ICrudRepository<EditorConfigFileType, EditorConfigFileTypeId>
{
    //სახელი რეგისტრის გარეშე ედრება (OrdinalIgnoreCase), ბაზის collation-ისგან დამოუკიდებლად. პროექტის
    //EditorConfigPatternName ამით იძებნება
    Task<EditorConfigFileType?> GetByName(string name, CancellationToken cancellationToken);
}

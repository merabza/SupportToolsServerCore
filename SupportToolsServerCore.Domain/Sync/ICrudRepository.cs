using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.Sync;

public interface ICrudRepository<T, TId> where T : Entity<TId> where TId : notnull
{
    Task<List<T>> GetAll(CancellationToken cancellationToken);
    void Delete(T o);
    void Add(T crudEntity);
    void Update(T crudEntity);
}

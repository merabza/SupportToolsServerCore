namespace SupportToolsServerCore.Domain.Sync;

public interface ICrudEntity
{
    bool IsSameById(ICrudEntity other);
}

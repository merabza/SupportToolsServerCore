using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.DatabaseServerConnections;

//ბაზის სერვერის ფოლდერების ნაკრები: სახელი (კლიენტის dictionary-ის key, მაგალითად Default; კავშირის შიგნით უნიკალურია)
//და ბექაპის, მონაცემებისა და ლოგის ფოლდერები. გზები DB სერვერზეა, ამიტომ არ გარდაიქმნება (README §4.4).
//ნაკრები აგრეგატის შვილია, ამიტომ საკუთარი ვერსია არ აქვს (README G7)
public sealed class DatabaseFoldersSet : Entity<DatabaseFoldersSetId>
{
    public const int NameMaxLength = 50;

    //Windows-ის MAX_PATH
    public const int FolderMaxLength = 260;

    public DatabaseFoldersSet(DatabaseFoldersSetId id, string name, string? backup, string? data, string? dataLog) :
        base(id)
    {
        Name = name;
        Backup = backup;
        Data = data;
        DataLog = dataLog;
    }

    public string Name { get; private set; }
    public string? Backup { get; private set; }
    public string? Data { get; private set; }
    public string? DataLog { get; private set; }

    public static DatabaseFoldersSet Create(string name, string? backup, string? data, string? dataLog)
    {
        return new DatabaseFoldersSet(DatabaseFoldersSetId.CreateUnique(), name, backup, data, dataLog);
    }
}

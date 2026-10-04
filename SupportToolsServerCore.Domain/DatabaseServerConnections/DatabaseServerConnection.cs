using System.Collections.Generic;
using SupportToolsServerCore.Domain.ApiClients;
using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.DatabaseServerConnections;

//მონაცემთა ბაზის სერვერთან კავშირი, SupportToolsParameters.DatabaseServerConnections-ის ჩანაწერი.
//DatabaseServerProvider კლიენტის EDatabaseProvider-ის სახელია; სერვერი enum-ს არ იცნობს და მხოლოდ სიგრძეს ამოწმებს.
//DbWebAgentId ბაზასთან დამაკავშირებელი ვებაგენტის ApiClient-ია (კონტრაქტში DbWebAgentName). ServerUser და ServerPass
//საიდუმლოა: ღიად ინახება (README G2), მაგრამ არსად იბეჭდება
public sealed class DatabaseServerConnection : VersionedEntity<DatabaseServerConnectionId>
{
    public const int NameMaxLength = 100;
    public const int DatabaseServerProviderMaxLength = 50;
    public const int RemoteDbConnectionNameMaxLength = 100;
    public const int ServerAddressMaxLength = 256;

    //SQL Server-ის sysname
    public const int ServerUserMaxLength = 128;

    public const int ServerPassMaxLength = 256;

    private readonly List<DatabaseFoldersSet> _databaseFoldersSets = [];

    public DatabaseServerConnection(DatabaseServerConnectionId id, string name, string databaseServerProvider,
        ApiClientId? dbWebAgentId, string? remoteDbConnectionName, string? serverAddress,
        bool windowsNtIntegratedSecurity, string? serverUser, string? serverPass, bool trustServerCertificate,
        int connectionTimeOut, bool encrypt, int version) : base(id, version)
    {
        Name = name;
        DatabaseServerProvider = databaseServerProvider;
        DbWebAgentId = dbWebAgentId;
        RemoteDbConnectionName = remoteDbConnectionName;
        ServerAddress = serverAddress;
        WindowsNtIntegratedSecurity = windowsNtIntegratedSecurity;
        ServerUser = serverUser;
        ServerPass = serverPass;
        TrustServerCertificate = trustServerCertificate;
        ConnectionTimeOut = connectionTimeOut;
        Encrypt = encrypt;
    }

    public string Name { get; private set; }
    public string DatabaseServerProvider { get; private set; }
    public ApiClientId? DbWebAgentId { get; private set; }

    //ვებაგენტის მხარეს ბაზასთან დამაკავშირებელი სახელი
    public string? RemoteDbConnectionName { get; private set; }

    public string? ServerAddress { get; private set; }
    public bool WindowsNtIntegratedSecurity { get; private set; }
    public string? ServerUser { get; private set; }
    public string? ServerPass { get; private set; }
    public bool TrustServerCertificate { get; private set; }
    public int ConnectionTimeOut { get; private set; }
    public bool Encrypt { get; private set; }
    public IReadOnlyList<DatabaseFoldersSet> DatabaseFoldersSets => _databaseFoldersSets;

    public static DatabaseServerConnection Create(string name, string databaseServerProvider,
        ApiClientId? dbWebAgentId, string? remoteDbConnectionName, string? serverAddress,
        bool windowsNtIntegratedSecurity, string? serverUser, string? serverPass, bool trustServerCertificate,
        int connectionTimeOut, bool encrypt, IEnumerable<DatabaseFoldersSet> databaseFoldersSets)
    {
        var databaseServerConnection = new DatabaseServerConnection(DatabaseServerConnectionId.CreateUnique(), name,
            databaseServerProvider, dbWebAgentId, remoteDbConnectionName, serverAddress, windowsNtIntegratedSecurity,
            serverUser, serverPass, trustServerCertificate, connectionTimeOut, encrypt, EntityVersion.Initial);
        databaseServerConnection._databaseFoldersSets.AddRange(databaseFoldersSets);
        return databaseServerConnection;
    }

    //რედაქტირება მთელ აგრეგატს ანაცვლებს (README G7): folders set-ები ახლით იცვლება და ვერსია იზრდება.
    //სახელის შეცვლა (მაგალითად, მხოლოდ რეგისტრის) იგივე ჩანაწერის განახლებაა
    public void Update(string name, string databaseServerProvider, ApiClientId? dbWebAgentId,
        string? remoteDbConnectionName, string? serverAddress, bool windowsNtIntegratedSecurity, string? serverUser,
        string? serverPass, bool trustServerCertificate, int connectionTimeOut, bool encrypt,
        IEnumerable<DatabaseFoldersSet> databaseFoldersSets)
    {
        List<DatabaseFoldersSet> newDatabaseFoldersSets = [.. databaseFoldersSets];
        Name = name;
        DatabaseServerProvider = databaseServerProvider;
        DbWebAgentId = dbWebAgentId;
        RemoteDbConnectionName = remoteDbConnectionName;
        ServerAddress = serverAddress;
        WindowsNtIntegratedSecurity = windowsNtIntegratedSecurity;
        ServerUser = serverUser;
        ServerPass = serverPass;
        TrustServerCertificate = trustServerCertificate;
        ConnectionTimeOut = connectionTimeOut;
        Encrypt = encrypt;
        _databaseFoldersSets.Clear();
        _databaseFoldersSets.AddRange(newDatabaseFoldersSets);
        IncrementVersion();
    }
}

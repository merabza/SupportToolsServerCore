using SupportToolsServerCore.Domain.FileStorages;
using Xunit;

namespace SupportToolsServerCore.Tests.FileStorages;

//The user name and the password are made up
public sealed class FileStorageTests
{
    [Fact]
    public void Constructor_SetsTheValues_AndRaisesNoDomainEvent()
    {
        var id = FileStorageId.CreateUnique();

        var fileStorage = new FileStorage(id, "Exchange", "ftp://ftp.example.com/exchange/", "made-up-user",
            "made-up-password", 255, 4, 1, 5);

        Assert.Equal(id, fileStorage.Id);
        Assert.Equal("Exchange", fileStorage.Name);
        Assert.Equal("ftp://ftp.example.com/exchange/", fileStorage.FileStoragePath);
        Assert.Equal("made-up-user", fileStorage.UserName);
        Assert.Equal("made-up-password", fileStorage.Password);
        Assert.Equal(255, fileStorage.FileNameMaxLength);
        Assert.Equal(4, fileStorage.FileSizeSplitPositionInRow);
        Assert.Equal(1, fileStorage.FtpSiteLsFileOffset);
        Assert.Equal(5, fileStorage.Version);
        Assert.Empty(fileStorage.DomainEvents);
    }

    [Fact]
    public void Create_ReturnsANewFileStorageWithAUniqueIdAndTheFirstVersion()
    {
        FileStorage first = FileStorage.Create("LocalBak", @"D:\Bak", null, null, 0, 0, 0);
        FileStorage second = FileStorage.Create("LocalBak", @"D:\Bak", null, null, 0, 0, 0);

        Assert.NotEqual(first.Id, second.Id);
        Assert.Equal("LocalBak", first.Name);
        Assert.Equal(@"D:\Bak", first.FileStoragePath);
        Assert.Null(first.UserName);
        Assert.Null(first.Password);
        Assert.Equal(0, first.FileNameMaxLength);
        Assert.Equal(0, first.FileSizeSplitPositionInRow);
        Assert.Equal(0, first.FtpSiteLsFileOffset);
        Assert.Equal(1, first.Version);
        Assert.Empty(first.DomainEvents);
    }

    [Fact]
    public void Update_ChangesTheValuesKeepingTheIdAndIncrementsTheVersion()
    {
        var id = FileStorageId.CreateUnique();
        var fileStorage = new FileStorage(id, "Exchange", "ftp://ftp.example.com/a/", "user-a", "password-a", 255, 4,
            1, 2);

        fileStorage.Update("EXCHANGE", "ftp://ftp.example.com/b/", "user-b", "password-b", 100, 5, 2);

        Assert.Equal(id, fileStorage.Id);
        Assert.Equal("EXCHANGE", fileStorage.Name);
        Assert.Equal("ftp://ftp.example.com/b/", fileStorage.FileStoragePath);
        Assert.Equal("user-b", fileStorage.UserName);
        Assert.Equal("password-b", fileStorage.Password);
        Assert.Equal(100, fileStorage.FileNameMaxLength);
        Assert.Equal(5, fileStorage.FileSizeSplitPositionInRow);
        Assert.Equal(2, fileStorage.FtpSiteLsFileOffset);
        Assert.Equal(3, fileStorage.Version);

        fileStorage.Update("EXCHANGE", null, null, null, 0, 0, 0);
        Assert.Null(fileStorage.FileStoragePath);
        Assert.Null(fileStorage.UserName);
        Assert.Null(fileStorage.Password);
        Assert.Equal(4, fileStorage.Version);
    }

    [Fact]
    public void Ids_WithTheSameValueAreEqual()
    {
        FileStorageId id = FileStorageId.CreateUnique();

        Assert.Equal(id, new FileStorageId(id.Value));
        Assert.NotEqual(id, FileStorageId.CreateUnique());
    }
}

using System;
using System.Security.Cryptography;
using SupportToolsServerCore.Domain.StoredFiles;
using Xunit;

namespace SupportToolsServerCore.Tests.StoredFiles;

public sealed class StoredFileTests
{
    private const string FilePath = @"D:\1WorkSecurity\AppA\PAZISI\Prod\appsettings.json";

    private static readonly DateTime CreatedAt = new(2026, 10, 6, 8, 15, 30, DateTimeKind.Utc);
    private static readonly DateTime UpdatedAt = new(2026, 10, 7, 9, 0, 0, DateTimeKind.Utc);

    [Fact]
    public void Constructor_SetsTheValues_AndRaisesNoDomainEvent()
    {
        var id = StoredFileId.CreateUnique();

        var storedFile = new StoredFile(id, FilePath, "{}", "ABC", 2, CreatedAt, 5);

        Assert.Equal(id, storedFile.Id);
        Assert.Equal(FilePath, storedFile.Path);
        Assert.Equal("{}", storedFile.Content);
        Assert.Equal("ABC", storedFile.Sha256);
        Assert.Equal(2, storedFile.Length);
        Assert.Equal(CreatedAt, storedFile.UpdatedAtUtc);
        Assert.Equal(5, storedFile.Version);
        Assert.Empty(storedFile.DomainEvents);
    }

    [Fact]
    public void Create_ReturnsANewFileWithAUniqueIdAndTheFirstVersion()
    {
        StoredFile first = StoredFile.Create(FilePath, "abc", CreatedAt);
        StoredFile second = StoredFile.Create(FilePath, "abc", CreatedAt);

        Assert.NotEqual(first.Id, second.Id);
        Assert.Equal(FilePath, first.Path);
        Assert.Equal("abc", first.Content);
        Assert.Equal(CreatedAt, first.UpdatedAtUtc);
        Assert.Equal(1, first.Version);
        Assert.Empty(first.DomainEvents);
    }

    //Known SHA-256 values: a BOM or another encoding would change them. The hash is upper case hex
    [Theory]
    [InlineData("", "E3B0C44298FC1C149AFBF4C8996FB92427AE41E4649B934CA495991B7852B855")]
    [InlineData("abc", "BA7816BF8F01CFEA414140DE5DAE2223B00361A396177A9CB410FF61F20015AD")]
    [InlineData("ა", "A651C13CCC628F7D626C22D6BEA0C515BDA2F6CA8F9933AB9331C9967EFDEE5A")]
    [InlineData("{\"a\":1}\r\n", "34CA028EB53BBC3BA8F2391662E32C658B6AEB2FB3B47C583CB845C70E01F47E")]
    public void Create_ComputesTheSha256OfTheUtf8BytesOfTheContent(string content, string sha256)
    {
        StoredFile storedFile = StoredFile.Create(FilePath, content, CreatedAt);

        Assert.Equal(sha256, storedFile.Sha256);
        Assert.Equal(StoredFile.Sha256Length, storedFile.Sha256.Length);
    }

    //The Georgian letter is one UTF-16 character and three UTF-8 bytes; the line break keeps its \r
    [Theory]
    [InlineData("", 0)]
    [InlineData("abc", 3)]
    [InlineData("ა", 3)]
    [InlineData("{\"a\":1}\r\n", 9)]
    public void Create_CountsTheUtf8BytesOfTheContent(string content, int length)
    {
        StoredFile storedFile = StoredFile.Create(FilePath, content, CreatedAt);

        Assert.Equal(length, storedFile.Length);
    }

    [Fact]
    public void Create_HashesTheUtf8BytesOfTheGeorgianLetter()
    {
        StoredFile storedFile = StoredFile.Create(FilePath, "ა", CreatedAt);

        Assert.Equal(Convert.ToHexString(SHA256.HashData([0xE1, 0x83, 0x90])), storedFile.Sha256);
    }

    [Fact]
    public void Update_ChangesTheValuesKeepingTheIdAndIncrementsTheVersion()
    {
        var id = StoredFileId.CreateUnique();
        var storedFile = new StoredFile(id, FilePath, "abc", "ABC", 3, CreatedAt, 2);

        storedFile.Update(FilePath.ToUpperInvariant(), "ა", UpdatedAt);

        Assert.Equal(id, storedFile.Id);
        Assert.Equal(FilePath.ToUpperInvariant(), storedFile.Path);
        Assert.Equal("ა", storedFile.Content);
        Assert.Equal("A651C13CCC628F7D626C22D6BEA0C515BDA2F6CA8F9933AB9331C9967EFDEE5A", storedFile.Sha256);
        Assert.Equal(3, storedFile.Length);
        Assert.Equal(UpdatedAt, storedFile.UpdatedAtUtc);
        Assert.Equal(3, storedFile.Version);

        storedFile.Update(FilePath, "", CreatedAt);
        Assert.Equal("E3B0C44298FC1C149AFBF4C8996FB92427AE41E4649B934CA495991B7852B855", storedFile.Sha256);
        Assert.Equal(0, storedFile.Length);
        Assert.Equal(4, storedFile.Version);
        Assert.Empty(storedFile.DomainEvents);
    }

    [Fact]
    public void Ids_WithTheSameValueAreEqual()
    {
        StoredFileId id = StoredFileId.CreateUnique();

        Assert.Equal(id, new StoredFileId(id.Value));
        Assert.NotEqual(id, StoredFileId.CreateUnique());
    }

    [Fact]
    public void Info_IsEqualToAnotherInfoWithTheSameValues()
    {
        var info = new StoredFileInfo(FilePath, "ABC", 3, CreatedAt, 2);

        Assert.Equal(new StoredFileInfo(FilePath, "ABC", 3, CreatedAt, 2), info);
        Assert.NotEqual(info with { Version = 3 }, info);
    }
}

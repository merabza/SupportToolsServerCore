using SupportToolsServerCore.Domain.GitIgnoreFileTypes;
using Xunit;

namespace SupportToolsServerCore.Tests.GitIgnoreFileTypes;

public sealed class GitIgnoreFileTypeTests
{
    //The type has no update method: an update passes a new instance with the stored Id and the stored Version + 1
    [Fact]
    public void Constructor_SetsTheValuesAndTheGivenVersion()
    {
        var id = GitIgnoreFileTypeId.CreateUnique();

        var gitIgnoreFileType = new GitIgnoreFileType(id, "CSharp", "bin/", 6);

        Assert.Equal(id, gitIgnoreFileType.Id);
        Assert.Equal("CSharp", gitIgnoreFileType.Name);
        Assert.Equal("bin/", gitIgnoreFileType.Content);
        Assert.Equal(6, gitIgnoreFileType.Version);
        Assert.Empty(gitIgnoreFileType.DomainEvents);
    }
}

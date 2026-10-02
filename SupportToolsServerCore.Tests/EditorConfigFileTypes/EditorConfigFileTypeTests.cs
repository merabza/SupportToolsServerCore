using SupportToolsServerCore.Domain.EditorConfigFileTypes;
using Xunit;

namespace SupportToolsServerCore.Tests.EditorConfigFileTypes;

public sealed class EditorConfigFileTypeTests
{
    //The type has no update method: an update passes a new instance with the stored Id and the stored Version + 1
    [Fact]
    public void Constructor_SetsTheValuesAndTheGivenVersion()
    {
        var id = EditorConfigFileTypeId.CreateUnique();

        var editorConfigFileType = new EditorConfigFileType(id, "default", "root = true", 2);

        Assert.Equal(id, editorConfigFileType.Id);
        Assert.Equal("default", editorConfigFileType.Name);
        Assert.Equal("root = true", editorConfigFileType.Content);
        Assert.Equal(2, editorConfigFileType.Version);
        Assert.Empty(editorConfigFileType.DomainEvents);
    }
}

using SupportToolsServerCore.Domain.ReactAppTemplates;
using Xunit;

namespace SupportToolsServerCore.Tests.ReactAppTemplates;

public sealed class ReactAppTemplateTests
{
    [Fact]
    public void Constructor_SetsTheValues_AndRaisesNoDomainEvent()
    {
        var id = ReactAppTemplateId.CreateUnique();

        var reactAppTemplate = new ReactAppTemplate(id, "ReduxApp", "redux-typescript", 5);

        Assert.Equal(id, reactAppTemplate.Id);
        Assert.Equal("ReduxApp", reactAppTemplate.Name);
        Assert.Equal("redux-typescript", reactAppTemplate.Template);
        Assert.Equal(5, reactAppTemplate.Version);
        Assert.Empty(reactAppTemplate.DomainEvents);
    }

    [Fact]
    public void Create_ReturnsANewReactAppTemplateWithAUniqueIdAndTheFirstVersion()
    {
        ReactAppTemplate first = ReactAppTemplate.Create("TypeScriptApp", "typescript");
        ReactAppTemplate second = ReactAppTemplate.Create("TypeScriptApp", "typescript");

        Assert.NotEqual(first.Id, second.Id);
        Assert.Equal("TypeScriptApp", first.Name);
        Assert.Equal("typescript", first.Template);
        Assert.Equal(1, first.Version);
        Assert.Empty(first.DomainEvents);
    }

    [Fact]
    public void Update_ChangesTheValuesKeepingTheIdAndIncrementsTheVersion()
    {
        var id = ReactAppTemplateId.CreateUnique();
        var reactAppTemplate = new ReactAppTemplate(id, "ReduxApp", "redux", 2);

        reactAppTemplate.Update("REDUXAPP", "redux-typescript");

        Assert.Equal(id, reactAppTemplate.Id);
        Assert.Equal("REDUXAPP", reactAppTemplate.Name);
        Assert.Equal("redux-typescript", reactAppTemplate.Template);
        Assert.Equal(3, reactAppTemplate.Version);

        reactAppTemplate.Update("REDUXAPP", "cra-template-redux-typescript");
        Assert.Equal("cra-template-redux-typescript", reactAppTemplate.Template);
        Assert.Equal(4, reactAppTemplate.Version);
    }

    [Fact]
    public void Ids_WithTheSameValueAreEqual()
    {
        ReactAppTemplateId id = ReactAppTemplateId.CreateUnique();

        Assert.Equal(id, new ReactAppTemplateId(id.Value));
        Assert.NotEqual(id, ReactAppTemplateId.CreateUnique());
    }
}

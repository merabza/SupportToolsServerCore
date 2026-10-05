using SupportToolsServerCore.Domain.ProjectTemplates;
using SupportToolsServerCore.Domain.ReactAppTemplates;
using Xunit;

namespace SupportToolsServerCore.Tests.ProjectTemplates;

public sealed class ProjectTemplateTests
{
    [Fact]
    public void Constructor_SetsTheValues_AndRaisesNoDomainEvent()
    {
        var id = ProjectTemplateId.CreateUnique();
        ReactAppTemplateId reactTemplateId = ReactAppTemplateId.CreateUnique();

        var projectTemplate = new ProjectTemplate(id, "Reactredux", "Api", "ReactTest", "Rt", true, false, true, false,
            true, false, true, false, true, false, reactTemplateId, 5);

        Assert.Equal(id, projectTemplate.Id);
        Assert.Equal("Reactredux", projectTemplate.Name);
        Assert.Equal("Api", projectTemplate.SupportProjectType);
        Assert.Equal("ReactTest", projectTemplate.TestProjectName);
        Assert.Equal("Rt", projectTemplate.TestProjectShortName);
        Assert.True(projectTemplate.UseDatabase);
        Assert.False(projectTemplate.UseDbPartFolderForDatabaseProjects);
        Assert.True(projectTemplate.UseMenu);
        Assert.False(projectTemplate.UseHttps);
        Assert.True(projectTemplate.UseReact);
        Assert.False(projectTemplate.UseCarcass);
        Assert.True(projectTemplate.UseIdentity);
        Assert.False(projectTemplate.UseReCounter);
        Assert.True(projectTemplate.UseSignalR);
        Assert.False(projectTemplate.UseFluentValidation);
        Assert.Equal(reactTemplateId, projectTemplate.ReactTemplateId);
        Assert.Equal(5, projectTemplate.Version);
        Assert.Empty(projectTemplate.DomainEvents);
    }

    [Fact]
    public void Create_ReturnsANewProjectTemplateWithAUniqueIdAndTheFirstVersion()
    {
        ProjectTemplate first = ProjectTemplate.Create("Console", "Console", "ConsoleTest", "CT", false, true, false,
            true, false, true, false, true, false, true, null);
        ProjectTemplate second = ProjectTemplate.Create("Console", "Console", null, null, false, false, false, false,
            false, false, false, false, false, false, null);

        Assert.NotEqual(first.Id, second.Id);
        Assert.Equal("Console", first.Name);
        Assert.Equal("Console", first.SupportProjectType);
        Assert.Equal("ConsoleTest", first.TestProjectName);
        Assert.Equal("CT", first.TestProjectShortName);
        Assert.False(first.UseDatabase);
        Assert.True(first.UseDbPartFolderForDatabaseProjects);
        Assert.False(first.UseMenu);
        Assert.True(first.UseHttps);
        Assert.False(first.UseReact);
        Assert.True(first.UseCarcass);
        Assert.False(first.UseIdentity);
        Assert.True(first.UseReCounter);
        Assert.False(first.UseSignalR);
        Assert.True(first.UseFluentValidation);
        Assert.Null(first.ReactTemplateId);
        Assert.Equal(1, first.Version);
        Assert.Empty(first.DomainEvents);
        Assert.Null(second.TestProjectName);
        Assert.Null(second.TestProjectShortName);
    }

    [Fact]
    public void Update_ChangesTheValuesKeepingTheIdAndIncrementsTheVersion()
    {
        var id = ProjectTemplateId.CreateUnique();
        var projectTemplate = new ProjectTemplate(id, "reactredux", "Razor", null, null, false, false, false, false,
            false, false, false, false, false, false, null, 2);
        ReactAppTemplateId reactTemplateId = ReactAppTemplateId.CreateUnique();

        projectTemplate.Update("Reactredux", "Api", "ReactTest", "Rt", true, true, true, true, true, true, true, true,
            true, true, reactTemplateId);

        Assert.Equal(id, projectTemplate.Id);
        Assert.Equal("Reactredux", projectTemplate.Name);
        Assert.Equal("Api", projectTemplate.SupportProjectType);
        Assert.Equal("ReactTest", projectTemplate.TestProjectName);
        Assert.Equal("Rt", projectTemplate.TestProjectShortName);
        Assert.True(projectTemplate.UseDatabase);
        Assert.True(projectTemplate.UseDbPartFolderForDatabaseProjects);
        Assert.True(projectTemplate.UseMenu);
        Assert.True(projectTemplate.UseHttps);
        Assert.True(projectTemplate.UseReact);
        Assert.True(projectTemplate.UseCarcass);
        Assert.True(projectTemplate.UseIdentity);
        Assert.True(projectTemplate.UseReCounter);
        Assert.True(projectTemplate.UseSignalR);
        Assert.True(projectTemplate.UseFluentValidation);
        Assert.Equal(reactTemplateId, projectTemplate.ReactTemplateId);
        Assert.Equal(3, projectTemplate.Version);

        projectTemplate.Update("Reactredux", "Console", null, null, false, false, false, false, false, false, false,
            false, false, false, null);
        Assert.Equal("Console", projectTemplate.SupportProjectType);
        Assert.Null(projectTemplate.TestProjectName);
        Assert.Null(projectTemplate.TestProjectShortName);
        Assert.False(projectTemplate.UseDatabase);
        Assert.False(projectTemplate.UseDbPartFolderForDatabaseProjects);
        Assert.False(projectTemplate.UseMenu);
        Assert.False(projectTemplate.UseHttps);
        Assert.False(projectTemplate.UseReact);
        Assert.False(projectTemplate.UseCarcass);
        Assert.False(projectTemplate.UseIdentity);
        Assert.False(projectTemplate.UseReCounter);
        Assert.False(projectTemplate.UseSignalR);
        Assert.False(projectTemplate.UseFluentValidation);
        Assert.Null(projectTemplate.ReactTemplateId);
        Assert.Equal(4, projectTemplate.Version);
    }

    [Fact]
    public void Ids_WithTheSameValueAreEqual()
    {
        ProjectTemplateId id = ProjectTemplateId.CreateUnique();

        Assert.Equal(id, new ProjectTemplateId(id.Value));
        Assert.NotEqual(id, ProjectTemplateId.CreateUnique());
    }
}

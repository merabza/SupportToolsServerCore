using SupportToolsServerCore.Domain.Primitives;
using SupportToolsServerCore.Domain.ReactAppTemplates;

namespace SupportToolsServerCore.Domain.ProjectTemplates;

//პროექტის შაბლონი, კლიენტის AppProjectCreatorAllParameters.Templates-ის ჩანაწერი (TemplateModel): სახელი (dictionary-ის
//key), პროექტის ტიპი, სატესტო პროექტის სახელები და ალმები, რომლებიც შესაქმნელი პროექტის შემადგენლობას განსაზღვრავს.
//SupportProjectType კლიენტის ESupportProjectType-ის სახელია; სერვერი enum-ს არ იცნობს და მხოლოდ სიგრძეს ამოწმებს.
//ReactTemplateId React-ის შაბლონია (ReactAppTemplate, კონტრაქტში სახელით)
public sealed class ProjectTemplate : VersionedEntity<ProjectTemplateId>
{
    public const int NameMaxLength = 100;
    public const int SupportProjectTypeMaxLength = 50;
    public const int TestProjectNameMaxLength = 100;
    public const int TestProjectShortNameMaxLength = 100;

    public ProjectTemplate(ProjectTemplateId id, string name, string supportProjectType, string? testProjectName,
        string? testProjectShortName, bool useDatabase, bool useDbPartFolderForDatabaseProjects, bool useMenu,
        bool useHttps, bool useReact, bool useCarcass, bool useIdentity, bool useReCounter, bool useSignalR,
        bool useFluentValidation, ReactAppTemplateId? reactTemplateId, int version) : base(id, version)
    {
        Name = name;
        SupportProjectType = supportProjectType;
        TestProjectName = testProjectName;
        TestProjectShortName = testProjectShortName;
        UseDatabase = useDatabase;
        UseDbPartFolderForDatabaseProjects = useDbPartFolderForDatabaseProjects;
        UseMenu = useMenu;
        UseHttps = useHttps;
        UseReact = useReact;
        UseCarcass = useCarcass;
        UseIdentity = useIdentity;
        UseReCounter = useReCounter;
        UseSignalR = useSignalR;
        UseFluentValidation = useFluentValidation;
        ReactTemplateId = reactTemplateId;
    }

    public string Name { get; private set; }
    public string SupportProjectType { get; private set; }
    public string? TestProjectName { get; private set; }
    public string? TestProjectShortName { get; private set; }

    //კონსოლის პროექტის ალმები
    public bool UseDatabase { get; private set; }

    public bool UseDbPartFolderForDatabaseProjects { get; private set; }
    public bool UseMenu { get; private set; }

    //Api პროექტის ალმები
    public bool UseHttps { get; private set; }

    public bool UseReact { get; private set; }
    public bool UseCarcass { get; private set; }
    public bool UseIdentity { get; private set; }
    public bool UseReCounter { get; private set; }
    public bool UseSignalR { get; private set; }
    public bool UseFluentValidation { get; private set; }
    public ReactAppTemplateId? ReactTemplateId { get; private set; }

    public static ProjectTemplate Create(string name, string supportProjectType, string? testProjectName,
        string? testProjectShortName, bool useDatabase, bool useDbPartFolderForDatabaseProjects, bool useMenu,
        bool useHttps, bool useReact, bool useCarcass, bool useIdentity, bool useReCounter, bool useSignalR,
        bool useFluentValidation, ReactAppTemplateId? reactTemplateId)
    {
        return new ProjectTemplate(ProjectTemplateId.CreateUnique(), name, supportProjectType, testProjectName,
            testProjectShortName, useDatabase, useDbPartFolderForDatabaseProjects, useMenu, useHttps, useReact,
            useCarcass, useIdentity, useReCounter, useSignalR, useFluentValidation, reactTemplateId,
            EntityVersion.Initial);
    }

    //რედაქტირება ვერსიას ზრდის. სახელის შეცვლა (მაგალითად, მხოლოდ რეგისტრის) იგივე ჩანაწერის განახლებაა
    public void Update(string name, string supportProjectType, string? testProjectName, string? testProjectShortName,
        bool useDatabase, bool useDbPartFolderForDatabaseProjects, bool useMenu, bool useHttps, bool useReact,
        bool useCarcass, bool useIdentity, bool useReCounter, bool useSignalR, bool useFluentValidation,
        ReactAppTemplateId? reactTemplateId)
    {
        Name = name;
        SupportProjectType = supportProjectType;
        TestProjectName = testProjectName;
        TestProjectShortName = testProjectShortName;
        UseDatabase = useDatabase;
        UseDbPartFolderForDatabaseProjects = useDbPartFolderForDatabaseProjects;
        UseMenu = useMenu;
        UseHttps = useHttps;
        UseReact = useReact;
        UseCarcass = useCarcass;
        UseIdentity = useIdentity;
        UseReCounter = useReCounter;
        UseSignalR = useSignalR;
        UseFluentValidation = useFluentValidation;
        ReactTemplateId = reactTemplateId;
        IncrementVersion();
    }
}

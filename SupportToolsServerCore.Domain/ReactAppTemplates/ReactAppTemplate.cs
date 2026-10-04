using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.ReactAppTemplates;

//React აპლიკაციის შაბლონი, SupportToolsParameters.ReactAppTemplates-ის ჩანაწერი: სახელი და create-react-app-ის
//--template მნიშვნელობა, ანუ npm-ის პაკეტის სახელი (typescript, redux-typescript, ...)
public sealed class ReactAppTemplate : VersionedEntity<ReactAppTemplateId>
{
    public const int NameMaxLength = 50;
    public const int TemplateMaxLength = 214;

    public ReactAppTemplate(ReactAppTemplateId id, string name, string template, int version) : base(id, version)
    {
        Name = name;
        Template = template;
    }

    public string Name { get; private set; }
    public string Template { get; private set; }

    public static ReactAppTemplate Create(string name, string template)
    {
        return new ReactAppTemplate(ReactAppTemplateId.CreateUnique(), name, template, EntityVersion.Initial);
    }

    //რედაქტირება ვერსიას ზრდის. სახელის შეცვლა (მაგალითად, მხოლოდ რეგისტრის) იგივე ჩანაწერის განახლებაა
    public void Update(string name, string template)
    {
        Name = name;
        Template = template;
        IncrementVersion();
    }
}

using SupportToolsServerCore.Domain.NpmPackages;
using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.Projects;

//პროექტის front-ის npm პაკეტი (NpmPackage, კონტრაქტში სახელით; კლიენტის FrontNpmPackageNames). პროექტში ერთხელ
//გვხვდება. აგრეგატის შვილია, ამიტომ საკუთარი ვერსია არ აქვს (README G7)
public sealed class ProjectNpmPackage : Entity<ProjectNpmPackageId>
{
    public ProjectNpmPackage(ProjectNpmPackageId id, NpmPackageId npmPackageId) : base(id)
    {
        NpmPackageId = npmPackageId;
    }

    public NpmPackageId NpmPackageId { get; private set; }

    public static ProjectNpmPackage Create(NpmPackageId npmPackageId)
    {
        return new ProjectNpmPackage(ProjectNpmPackageId.CreateUnique(), npmPackageId);
    }
}

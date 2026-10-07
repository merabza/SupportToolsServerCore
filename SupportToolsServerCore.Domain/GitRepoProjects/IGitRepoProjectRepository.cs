using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SupportToolsServerCore.Domain.GitRepos;

namespace SupportToolsServerCore.Domain.GitRepoProjects;

//სკანირების შედეგი რეპოზიტორიის მიხედვით ინახება: ახალი სკანირება ამ რეპოზიტორიის ყველა პროექტს ანაცვლებს
public interface IGitRepoProjectRepository
{
    //ყველა რეპოზიტორიის პროექტები დამოკიდებულებებით, თვალყურის დევნების გარეშე
    Task<List<GitRepoProject>> GetAll(CancellationToken cancellationToken);

    //რეპოზიტორიის შენახული პროექტები დამოკიდებულებებით წასაშლელად, ახლები კი დასამატებლად რეგისტრირდება. handler-ი
    //ორივეს ერთი SaveChangesAsync-ით, ერთ ტრანზაქციაში ინახავს
    Task Replace(GitRepoId gitRepoId, IEnumerable<GitRepoProject> gitRepoProjects,
        CancellationToken cancellationToken);
}

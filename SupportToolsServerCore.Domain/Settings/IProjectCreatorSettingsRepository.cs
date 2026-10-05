using System.Threading;
using System.Threading.Tasks;

namespace SupportToolsServerCore.Domain.Settings;

//ჩანაწერი ერთადერთია (singleton), ამიტომ წაკითხვას გასაღები არ სჭირდება. წაშლა არ არსებობს
public interface IProjectCreatorSettingsRepository
{
    //ერთადერთი ჩანაწერი, ან null, თუ ის ჯერ არ შექმნილა
    Task<ProjectCreatorSettings?> Get(CancellationToken cancellationToken);

    void Add(ProjectCreatorSettings projectCreatorSettings);
    void Update(ProjectCreatorSettings projectCreatorSettings);
}

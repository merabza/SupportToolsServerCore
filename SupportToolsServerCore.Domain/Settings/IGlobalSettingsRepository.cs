using System.Threading;
using System.Threading.Tasks;

namespace SupportToolsServerCore.Domain.Settings;

//ჩანაწერი ერთადერთია (singleton), ამიტომ წაკითხვას გასაღები არ სჭირდება. წაშლა არ არსებობს
public interface IGlobalSettingsRepository
{
    //ერთადერთი ჩანაწერი, ან null, თუ ის ჯერ არ შექმნილა
    Task<GlobalSettings?> Get(CancellationToken cancellationToken);

    void Add(GlobalSettings globalSettings);
    void Update(GlobalSettings globalSettings);
}

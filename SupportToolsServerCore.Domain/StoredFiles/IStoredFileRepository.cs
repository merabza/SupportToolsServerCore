using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SupportToolsServerCore.Domain.StoredFiles;

//ფაილი გზით იძებნება. ICrudRepository-ის GetAll აქ არ არის, რადგან ყველა ფაილის შიგთავსს წაიკითხავდა: სიას
//GetInfos აბრუნებს
public interface IStoredFileRepository
{
    //ყველა ფაილის მეტამონაცემები, შიგთავსის გარეშე
    Task<List<StoredFileInfo>> GetInfos(CancellationToken cancellationToken);

    //გზა რეგისტრის გარეშე ედრება (OrdinalIgnoreCase), ბაზის collation-ისგან დამოუკიდებლად. ფაილი შიგთავსით და
    //თვალყურის დევნების გარეშე იკითხება
    Task<StoredFile?> GetByPath(string path, CancellationToken cancellationToken);

    void Add(StoredFile storedFile);
    void Update(StoredFile storedFile);
    void Delete(StoredFile storedFile);
}

using System.Collections.Generic;
using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.SmartSchemas;

//ჭკვიანი სქემა, SupportToolsParameters.SmartSchemas-ის ჩანაწერი: სახელი (dictionary-ის key), ბოლო ფაილების რაოდენობა,
//რომელიც ყოველთვის რჩება, და დეტალები. სქემით წყდება, რომელი ბექაპის ფაილები შეინახოს და რომელი წაიშალოს.
//დეტალების რიგს მნიშვნელობა არ აქვს, პერიოდის ტიპი კი სქემაში ერთხელ გვხვდება (კლიენტი დეტალებს ტიპით ეძებს)
public sealed class SmartSchema : VersionedEntity<SmartSchemaId>
{
    public const int NameMaxLength = 100;

    private readonly List<SmartSchemaDetail> _details = [];

    public SmartSchema(SmartSchemaId id, string name, int lastPreserveCount, int version) : base(id, version)
    {
        Name = name;
        LastPreserveCount = lastPreserveCount;
    }

    public string Name { get; private set; }
    public int LastPreserveCount { get; private set; }
    public IReadOnlyList<SmartSchemaDetail> Details => _details;

    public static SmartSchema Create(string name, int lastPreserveCount, IEnumerable<SmartSchemaDetail> details)
    {
        var smartSchema =
            new SmartSchema(SmartSchemaId.CreateUnique(), name, lastPreserveCount, EntityVersion.Initial);
        smartSchema._details.AddRange(details);
        return smartSchema;
    }

    //რედაქტირება მთელ აგრეგატს ანაცვლებს (README G7): დეტალები ახლით იცვლება და ვერსია იზრდება.
    //სახელის შეცვლა (მაგალითად, მხოლოდ რეგისტრის) იგივე ჩანაწერის განახლებაა
    public void Update(string name, int lastPreserveCount, IEnumerable<SmartSchemaDetail> details)
    {
        List<SmartSchemaDetail> newDetails = [.. details];
        Name = name;
        LastPreserveCount = lastPreserveCount;
        _details.Clear();
        _details.AddRange(newDetails);
        IncrementVersion();
    }
}

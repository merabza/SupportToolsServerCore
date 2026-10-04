using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.SmartSchemas;

//ჭკვიანი სქემის დეტალი: პერიოდის ტიპი და ამ ტიპის რამდენი პერიოდის ფაილი შეინახოს. პერიოდის ტიპი კლიენტის EPeriodType-ის
//სახელია (მაგალითად, Day); სერვერი enum-ს არ იცნობს და მხოლოდ სიგრძეს ამოწმებს. დეტალი აგრეგატის შვილია, ამიტომ
//საკუთარი ვერსია არ აქვს (README G7)
public sealed class SmartSchemaDetail : Entity<SmartSchemaDetailId>
{
    public const int PeriodTypeMaxLength = 50;

    public SmartSchemaDetail(SmartSchemaDetailId id, string periodType, int preserveCount) : base(id)
    {
        PeriodType = periodType;
        PreserveCount = preserveCount;
    }

    public string PeriodType { get; private set; }
    public int PreserveCount { get; private set; }

    public static SmartSchemaDetail Create(string periodType, int preserveCount)
    {
        return new SmartSchemaDetail(SmartSchemaDetailId.CreateUnique(), periodType, preserveCount);
    }
}

namespace SupportToolsServerCore.Domain.Primitives;

//რეესტრის აგრეგატის ფესვი ვერსიით (optimistic concurrency). Version შექმნისას EntityVersion.Initial-ია და ყოველ
//ცვლილებაზე 1-ით იზრდება: დომენის მეთოდი IncrementVersion-ს იძახებს, ან განახლებისას ახალი ეგზემპლარი შენახული
//Version + 1-ით იქმნება. ბაზაში Version concurrency token-ია, ამიტომ UPDATE და DELETE მხოლოდ მაშინ სრულდება, თუ
//ჩანაწერი წაკითხვის შემდეგ არავის შეუცვლია (CLAUDE.md, Registry conventions)
public abstract class VersionedEntity<TId> : Entity<TId> where TId : notnull
{
    protected VersionedEntity(TId id, int version) : base(id)
    {
        Version = version;
    }

    public int Version { get; private set; }

    protected void IncrementVersion()
    {
        Version++;
    }
}

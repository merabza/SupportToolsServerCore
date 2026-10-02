namespace SupportToolsServerCore.Domain.Primitives;

//ვერსიიანი ჩანაწერის (VersionedEntity) პირველი ვერსია. ბაზის Version სვეტის DEFAULT-იც ესაა, რომ სვეტის დამატებისას
//არსებულმა ჩანაწერებმაც ის მიიღონ
public static class EntityVersion
{
    public const int Initial = 1;
}

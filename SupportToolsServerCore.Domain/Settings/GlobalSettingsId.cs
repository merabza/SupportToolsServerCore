using System;
using System.Collections.Generic;
using SupportToolsServerCore.Domain.Primitives;

namespace SupportToolsServerCore.Domain.Settings;

//GlobalSettings-ის გასაღები. ჩანაწერი ერთადერთია (singleton), ამიტომ გასაღები ფიქსირებულია: ორი ერთდროული პირველი
//შექმნიდან მეორეს ბაზის PK აჩერებს, ცხრილის CHECK კი სხვა გასაღებით ჩაწერას კრძალავს (CLAUDE.md, Registry conventions)
public sealed class GlobalSettingsId : ValueObject
{
    public GlobalSettingsId(Guid value)
    {
        Value = value;
    }

    public static GlobalSettingsId Singleton { get; } = new(new Guid("00000000-0000-0000-0000-000000000001"));

    public Guid Value { get; }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

using System.Collections.Generic;

public class HeroTM : TargetMode
{
    public override List<CombatantView> GetTargets()
    {
        return new() { HeroSystem.instance.heroView };
    }
}

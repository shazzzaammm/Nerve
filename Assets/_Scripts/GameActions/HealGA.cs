using System.Collections.Generic;
public class HealGA : GameAction
{
    

    public int amount;
    public List<CombatantView> targets;
    public CombatantView caster { get; private set; }

    public HealGA(int amount, List<CombatantView> targets, CombatantView caster)
    {
        this.amount = amount;
        this.targets = targets;
        this.caster = caster;
    }

}

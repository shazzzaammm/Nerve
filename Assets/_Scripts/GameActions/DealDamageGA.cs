using System.Collections.Generic;
using UnityEngine;

public class DealDamageGA : GameAction, IHaveCaster
{
    public int amount;
    public List<CombatantView> targets;
    public CombatantView caster { get; private set; }

    public DealDamageGA(int amount, List<CombatantView> targets, CombatantView caster)
    {
        this.amount = amount;
        this.targets = targets;
        this.caster = caster;
    }

}

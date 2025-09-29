using System.Collections.Generic;
using UnityEngine;

public class DealDamageGA : GameAction
{
    public int amount;
    public List<CombatantView> targets;

    public DealDamageGA(int amount, List<CombatantView> targets)
    {
        this.amount = amount;
        this.targets = targets;
    }

}

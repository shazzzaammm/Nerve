using System.Collections.Generic;
using UnityEngine;

public class AddShieldGA : GameAction
{
    public int amount;
    public List<CombatantView> targets;
    public AddShieldGA(int amount, List<CombatantView> targets){
        this.amount = amount;
        this.targets = targets;
    }
}

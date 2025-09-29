using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackHeroGA : GameAction
{
    public EnemyView attacker { get; private set;}
    public List<CombatantView> targets;

    public EnemyAttackHeroGA(EnemyView attacker){
        this.attacker = attacker;
    }

}

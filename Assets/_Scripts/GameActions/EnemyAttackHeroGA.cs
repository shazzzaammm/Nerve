using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackHeroGA : GameAction, IHaveCaster
{
    public EnemyView attacker { get; private set;}

    public CombatantView caster { get; private set; }

    public List<CombatantView> targets;

    public EnemyAttackHeroGA(EnemyView attacker){
        this.attacker = attacker;
        caster = attacker;
    }

}

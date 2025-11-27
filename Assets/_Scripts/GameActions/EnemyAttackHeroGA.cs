using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackHeroGA : GameAction, IHaveCaster
{
    public EnemyView attacker { get; private set;}

    public CombatantView caster { get; private set; }

    public List<CombatantView> targets;
    public int damage;

    public EnemyAttackHeroGA(EnemyView attacker, int damage){
        this.attacker = attacker;
        this.damage = damage;
        caster = attacker;
    }

}

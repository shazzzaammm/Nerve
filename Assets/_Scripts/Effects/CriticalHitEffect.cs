using System.Collections.Generic;
using UnityEngine;

public class CriticalHitEffect : Effect
{
    [SerializeField] private float critChance;
    [SerializeField] private int critDamage, regularDamage;
    public override GameAction GetGameAction(List<CombatantView> targets, CombatantView caster)
    {
        bool crit = Random.Range(0f, 1f) <= critChance;
        return new DealDamageGA(crit ? critDamage : regularDamage, targets, caster);
    }
}

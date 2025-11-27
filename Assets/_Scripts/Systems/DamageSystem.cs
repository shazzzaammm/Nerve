using System;
using System.Collections;
using UnityEngine;

public class DamageSystem : Singleton<DamageSystem>
{
    void OnEnable()
    {
        ActionSystem.AttachPerformer<DealDamageGA>(DealDamagePerformer);
        ActionSystem.AttachPerformer<HealGA>(HealPerformer);
        ActionSystem.AttachPerformer<AddShieldGA>(AddShieldGAPerformer);
    }

    void OnDisable()
    {
        ActionSystem.DetachPerformer<DealDamageGA>();
        ActionSystem.DetachPerformer<HealGA>();
        ActionSystem.DetachPerformer<AddShieldGA>();
    }

    private IEnumerator HealPerformer(HealGA healGA)
    {
        foreach (var target in healGA.targets)
        {
            target.HealDamage(healGA.amount);
        }
        yield return null;
    }
    private IEnumerator DealDamagePerformer(DealDamageGA dealDamageGA)
    {
        foreach (CombatantView target in dealDamageGA.targets)
        {
            target.TakeDamage(dealDamageGA.amount);
            if (target.currentHealth <= 0)
            {
                if (target is EnemyView enemyView)
                {
                    KillEnemyGA killEnemyGA = new(enemyView);
                    ActionSystem.instance.AddReaction(killEnemyGA);
                }
                else
                {
                    // Player dies
                }
            }
        }
        yield return null;
    }

    private IEnumerator AddShieldGAPerformer(AddShieldGA addShieldGA)
    {
        foreach (CombatantView target in addShieldGA.targets)
        {
            target.AddShield(addShieldGA.amount);
        }
        yield return null;
    }
}

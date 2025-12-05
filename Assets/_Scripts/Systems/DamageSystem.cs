using System;
using System.Collections;
using UnityEngine;

public class DamageSystem : Singleton<DamageSystem>
{
    void OnEnable()
    {
        ActionSystem.AttachPerformer<DealDamageGA>(DealDamagePerformer);
        ActionSystem.AttachPerformer<HealGA>(HealPerformer);
    }

    void OnDisable()
    {
        ActionSystem.DetachPerformer<DealDamageGA>();
        ActionSystem.DetachPerformer<HealGA>();
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
                    PlayerKilledGA playerKilledGA = new();
                    ActionSystem.instance.AddReaction(playerKilledGA);
                }
            }
        }
        yield return null;
    }
}

using System.Collections;
using UnityEngine;

public class DamageSystem : Singleton<DamageSystem>
{
    void OnEnable()
    {
        ActionSystem.AttachPerformer<DealDamageGA>(DealDamagePerformer);

    }
    void OnDisable()
    {
        ActionSystem.DetachPerformer<DealDamageGA>();
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
}

using System.Collections;
using UnityEngine;

public class PoisonSystem : Singleton<PoisonSystem>
{
    const int POISON_DAMAGE = 1;
    private void OnEnable()
    {
        ActionSystem.AttachPerformer<ApplyPoisonGA>(ApplyPoisonPerformer);
    }
    private void OnDisable()
    {
        ActionSystem.DetachPerformer<ApplyPoisonGA>();
    }

    private IEnumerator ApplyPoisonPerformer(ApplyPoisonGA applyPoisonGA)
    {
        CombatantView target = applyPoisonGA.target;
        // effects
        ActionSystem.instance.AddReaction(new DealDamageGA(POISON_DAMAGE, new() {target} , null));
        target.RemoveStatusEffect(StatusEffectType.Poison, 1);
        yield return new WaitForSeconds(.2f);

    }
}

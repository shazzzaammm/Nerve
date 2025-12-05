using System.Collections;
using UnityEngine;

public class StatusEffectSystem : Singleton<StatusEffectSystem>
{
    private void OnEnable() {
        ActionSystem.AttachPerformer<AddStatusEffectGA>(AddStatusEffectPerformer);
    }
    private void OnDisable() {
        ActionSystem.DetachPerformer<AddStatusEffectGA>();
    }
    private IEnumerator AddStatusEffectPerformer(AddStatusEffectGA addStatusEffectGA){
        foreach (CombatantView target in addStatusEffectGA.targets)
        {
            if (target != null && target.isActiveAndEnabled)
            target.AddStatusEffect(addStatusEffectGA.statusEffectType, addStatusEffectGA.stackCount);
            yield return null;
        }
    }
}

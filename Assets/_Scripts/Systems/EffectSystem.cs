using System.Collections;
using UnityEngine;

public class EffectSystem : Singleton<EffectSystem>
{
    void OnEnable()
    {
        ActionSystem.AttachPerformer<PerformEffectGA>(PerformEffectPerformer);

    }

    void OnDisable()
    {
        ActionSystem.DetachPerformer<PerformEffectGA>();

    }

    private IEnumerator PerformEffectPerformer(PerformEffectGA performEffectGA)
    {
        GameAction effectAction = performEffectGA.effect.GetGameAction(performEffectGA.targets, HeroSystem.instance.heroView);
        ActionSystem.instance.AddReaction(effectAction);
        yield return null;
    }
}

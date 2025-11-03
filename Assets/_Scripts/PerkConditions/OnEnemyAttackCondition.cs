using System;
using UnityEngine;

public class OnEnemyAttackCondition : PerkCondition
{
    public override bool SubConditionIsMet(GameAction action)
    {
        // attacker above x health or something like that
        return true;
    }

    public override void SubscribeCondition(Action<GameAction> reaction)
    {
        ActionSystem.SubscribeReaction<EnemyAttackHeroGA>(reaction, reactionTiming);
    }

    public override void UnsubscribeCondition(Action<GameAction> reaction)
    {
        ActionSystem.UnsubscribeReaction<EnemyAttackHeroGA>(reaction, reactionTiming);
    }
}

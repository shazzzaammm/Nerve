using System;
using UnityEngine;

public class BossSpawnGridView : GridCellView
{
    bool canExit = false;
    public override bool isWalkable()
    {
        return canExit;
    }

    public override void SetUnit(GridUnit unit)
    {
        base.SetUnit(unit);
        if (unit is HeroGridUnit){
            ExitLevelGA exitLevelGA = new();
            ActionSystem.instance.AddReaction(exitLevelGA);
        }
    }
    
    private void OnEnable() {
        ActionSystem.SubscribeReaction<KillEnemyGA>(killEnemyPostReaction, ReactionTiming.POST);
    }
    private void OnDisable() {
        ActionSystem.UnsubscribeReaction<KillEnemyGA>(killEnemyPostReaction, ReactionTiming.POST);
    }

    private void killEnemyPostReaction(KillEnemyGA killEnemyGA)
    {
        if (killEnemyGA.target.data.Equals(DataSystem.instance.boss)){
            canExit = true;
        }
    }
}

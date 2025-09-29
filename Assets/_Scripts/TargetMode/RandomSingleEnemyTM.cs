using System.Collections.Generic;
using UnityEngine;

public class RandomSingleEnemyTM : TargetMode
{
    public override List<CombatantView> GetTargets()
    {
        CombatantView target = EnemySystem.instance.enemies[Random.Range(0, EnemySystem.instance.enemies.Count)];
        return new(){target};
    }
}

using System.Collections.Generic;

public class TargetedEnemyTM : TargetMode
{
    public override List<CombatantView> GetTargets()
    {
        return new() {ManualTargetingSystem.instance.targetedEnemyView};
    }
}

using UnityEngine;

public class ExitGridCell : GridCellView
{
    public override bool isWalkable()
    {
        return true;
    }

    public override void SetUnit(GridUnit unit)
    {
        base.SetUnit(unit);
        if (unit is HeroGridUnit){
            ExitLevelGA exitLevelGA = new();
            ActionSystem.instance.AddReaction(exitLevelGA);
        }
    }
}

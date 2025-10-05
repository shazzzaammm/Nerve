using UnityEngine;

public class MoveOnGridGA : GameAction
{
    public GridUnit unit;
    public GridCellView destination;
    public MoveOnGridGA(GridUnit unit, GridCellView destination){
        this.unit = unit;
        this.destination = destination;
    }

}

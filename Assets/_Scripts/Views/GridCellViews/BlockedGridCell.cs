using UnityEngine;

public class BlockedGridCell : GridCellView
{
    public override bool isWalkable()
    {
        return false;
    }
}

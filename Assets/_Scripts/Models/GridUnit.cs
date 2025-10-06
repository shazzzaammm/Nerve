using UnityEngine;

public abstract class GridUnit : MonoBehaviour
{
    public string unitName;
    public GridCellView occupiedTile;

    public Vector2 positionOnGrid { get; protected set; }
    
    public void Move(Vector2 destination){
        positionOnGrid = destination;
        transform.position = GridSystem.instance.GetCellAtPosition(positionOnGrid).transform.position;
    }
}

using UnityEngine;

public abstract class GridUnit : MonoBehaviour
{
    public string unitName;
    public GridCellView occupiedTile;

    public Vector2 positionOnGrid { get; protected set; }
    
    public void Move(Vector2 destination){
        positionOnGrid = destination;
        GridCellView cell =GridSystem.instance.GetCellAtPosition(positionOnGrid);
        if (cell != null)
        transform.position = cell.transform.position;
        else Destroy(this.gameObject);
    }

    
    protected void Update(){
        transform.LookAt(Camera.main.transform);
    }
}

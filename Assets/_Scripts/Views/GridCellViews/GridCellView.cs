using UnityEngine;

public abstract class GridCellView : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer highlight;
    public Vector2Int positionOnGrid { get; protected set; }
    public GridUnit occupiedUnit { get; protected set; }
    public abstract bool isWalkable();


    private void OnEnable()
    {
        highlight.enabled = false;
    }
    public void Setup(int x, int y)
    {
        positionOnGrid = new(x, y);
    }

    protected void OnMouseEnter()
    {
        if (isWalkable())
        {
            highlight.enabled = true;
        }
    }
    protected void OnMouseExit()
    {
        if (isWalkable())
        {
            highlight.enabled = false;
        }
    }

    private void OnMouseDown()
    {
        OnClick();
    }

    protected void OnClick()
    {
        if (!Interactions.instance.playerCanMoveOnGrid) return;
        if (!isWalkable()) return;
        if (occupiedUnit != null && occupiedUnit.Equals(GridUnitSystem.instance.hero)) return;
        if (!GridUnitSystem.instance.HeroCanReachCell(this)) return;

        MoveOnGridGA moveOnGridGA = new(GridUnitSystem.instance.hero, this);
        ActionSystem.instance.Perform(moveOnGridGA);
    }


    public void SetUnit(GridUnit unit)
    {
        if (unit.occupiedTile != null) unit.occupiedTile.occupiedUnit = null;
        if (occupiedUnit != null && (unit.Equals(GridUnitSystem.instance.hero) || occupiedUnit.Equals(GridUnitSystem.instance.hero)))
        {
            Debug.Log("Fight!!!");
            EnemyGridUnit enemy = (EnemyGridUnit)(unit is EnemyGridUnit ? unit : occupiedUnit);
            StartMatchGA startMatchGA = new(GridUnitSystem.instance.hero.data, new() { enemy.data });
            ActionSystem.instance.AddReaction(startMatchGA);
        }
        unit.transform.position = transform.position;
        occupiedUnit = unit;
        unit.occupiedTile = this;
    }
}

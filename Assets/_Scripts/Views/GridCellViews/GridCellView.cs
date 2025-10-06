using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public abstract class GridCellView : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer highlight;
    public Vector2Int positionOnGrid { get; protected set; }
    public List<GridUnit> occupiedUnits { get; protected set; } = new();
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
        if (occupiedUnits.Count != 0 && occupiedUnits.Contains(GridUnitSystem.instance.hero)) return;
        if (!GridUnitSystem.instance.HeroCanReachCell(this)) return;

        MoveOnGridGA moveOnGridGA = new(GridUnitSystem.instance.hero, this);
        ActionSystem.instance.Perform(moveOnGridGA);
    }


    public void SetUnit(GridUnit unit)
    {
        unit.occupiedTile?.occupiedUnits.Remove(unit);
        if (occupiedUnits.Count != 0 && (unit.Equals(GridUnitSystem.instance.hero) || occupiedUnits.Contains(GridUnitSystem.instance.hero)))
        {
            Debug.Log("Fight!!!");
            List<EnemyData> enemyDatas = new();
            foreach (GridUnit occupiedUnit in occupiedUnits){
                if (occupiedUnit is EnemyGridUnit enemyUnit) enemyDatas.Add(enemyUnit.data);
            }
            StartMatchGA startMatchGA = new(GridUnitSystem.instance.hero.data, enemyDatas);
            ActionSystem.instance.AddReaction(startMatchGA);
        }
        unit.transform.position = transform.position;
        occupiedUnits.Add(unit);
        unit.occupiedTile = this;
    }
}

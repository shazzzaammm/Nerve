using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridSystem : Singleton<GridSystem>
{
    [SerializeField] private GridCellView walkableCell, blockedCell;

    [SerializeField] private int rows;
    [SerializeField] private int columns;
    [SerializeField] private Vector2 size;
    private Vector2 spacing;
    Dictionary<Vector2, GridCellView> grid;
    void Start()
    {
        Setup();
    }
    
    private void OnEnable() {
        ActionSystem.AttachPerformer<EndMatchGA>(EndMatchGAPerformer);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<EndMatchGA>();
    }

    public void Setup()
    {
        grid = new();
        spacing = new(size.x / columns, size.y / rows);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                int rand = Random.Range(0, 10);
                GridCellView cell = Instantiate(rand > 1 ? walkableCell : blockedCell, transform.position + new Vector3(spacing.x * j, spacing.y * i) - ((Vector3)size * .5f), Quaternion.identity);
                cell.name = "Cell " + j + " " + i;
                cell.Setup(j, i);
                grid.Add(new(j, i), cell);
            }
        }
        GridUnitSystem.instance.Setup(DataSystem.instance.heroes[0], DataSystem.instance.enemies);
    }
    public GridCellView GetCellAtPosition(Vector2 pos)
    {
        if (grid.TryGetValue(pos, out GridCellView cell))
        {
            return cell;
        }
        return null;
    }
    
    public void SetRandomHeroPosition(){
        GridCellView cell = grid.Where(t => t.Key.x < columns / 2 && t.Value.isWalkable()).OrderBy(t => Random.value).First().Value;
        GridUnitSystem.instance.hero.Move(cell.positionOnGrid);
    }

    public void SetRandomEnemyPosition(EnemyGridUnit enemy){
        GridCellView cell = grid.Where(t => t.Key.x > columns / 2 && t.Value.isWalkable()).OrderBy(t => Random.value).First().Value;
        enemy.Move(cell.positionOnGrid);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.softRed;
        Gizmos.DrawWireCube(transform.position, size);
    }

    public void DisableVisuals()
    {
        Interactions.instance.playerCanMoveOnGrid = false;
        foreach (var cell in grid.Values){
            Destroy(cell.gameObject);
        }
        grid.Clear();
    }

    private IEnumerator EndMatchGAPerformer(EndMatchGA endMatchGA){
        Setup();
        
        yield return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridSystem : Singleton<GridSystem>
{
    [SerializeField] private Transform gridParent;
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

    private void OnEnable()
    {
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
                GridCellView cell = Instantiate(rand > 1 ? walkableCell : blockedCell, gridParent.position + new Vector3(spacing.x * j, spacing.y * i) - ((Vector3)size * .5f), Quaternion.identity, gridParent);
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

    public List<GridCellView> GetCellNeighbors(GridCellView cell)
    {
        List<Vector2> directions = new() {
            Vector2.up,
            Vector2.down,
            Vector2.left,
            Vector2.right,
            new(1, 1),
            new(-1, 1),
            new(1, -1),
            new(-1, -1)
        };
        List<GridCellView> neighbors = new();
        foreach (Vector2 dir in directions)
        {
            GridCellView neighbor = GetCellAtPosition(cell.positionOnGrid + dir);
            if (neighbor)
            {
                neighbors.Add(neighbor);
            }
        }
        return neighbors;
    }

    public void SetRandomHeroPosition()
    {
        GridCellView cell = grid.Where(t => t.Key.x < columns / 2 && t.Value.isWalkable()).OrderBy(t => Random.value).First().Value;
        GridUnitSystem.instance.hero.Move(cell.positionOnGrid);
    }

    public void SetRandomEnemyPosition(EnemyGridUnit enemy)
    {
        GridCellView cell = grid.Where(t => t.Key.x > columns / 2 && t.Value.isWalkable()).OrderBy(t => Random.value).First().Value;
        enemy.Move(cell.positionOnGrid);
    }

    private void OnDrawGizmosSelected()
    {
        if (!gridParent) return;
        Gizmos.color = Color.softRed;
        Gizmos.DrawWireCube(gridParent.position, size);
    }

    public void DisableVisuals()
    {
        Interactions.instance.playerCanMoveOnGrid = false;
        foreach (var cell in grid.Values)
        {
            Destroy(cell.gameObject);
        }
        grid.Clear();
    }

    private IEnumerator EndMatchGAPerformer(EndMatchGA endMatchGA)
    {
        Setup();

        yield return null;
    }
}

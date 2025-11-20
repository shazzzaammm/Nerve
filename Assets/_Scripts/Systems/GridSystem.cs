using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore;

public class GridSystem : Singleton<GridSystem>
{
    [SerializeField] private Transform gridParent;
    [SerializeField] private GridCellView walkableCell, blockedCell;
    [SerializeField] private ChestGridCell chestCell;

    [SerializeField] private Vector2 size;

    [SerializeField] private Texture2D[] layouts;
    private Vector2 spacing;
    Dictionary<Vector2, GridCellView> grid;
    List<Vector2> enemySpawnPositions;
    List<Vector2> heroSpawnPostions;
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
        heroSpawnPostions = new();
        enemySpawnPositions = new();
        int randomLayout = Random.Range(0, layouts.Length);
        Dictionary<Vector2Int, TileType> layout = LevelGenerationSystem.instance.Generate(layouts[randomLayout]);
        spacing = new(size.x / layouts[randomLayout].width, size.y / layouts[randomLayout].height);

        int sumX = 0;
        int sumY = 0;

        foreach (Vector2Int pos in layout.Keys)
        {
            sumX += pos.x;
            sumY += pos.y;
        }

        Vector2 center = new(sumX / layout.Keys.Count, sumY / layout.Keys.Count);

        foreach (Vector2Int pos in layout.Keys)
        {
            TileType cellType = layout[pos];
            GridCellView cellPrefab = cellType switch
            {
                TileType.FLOOR => walkableCell,
                TileType.WALL => blockedCell,
                TileType.CHEST_SPAWN => chestCell,
                _ => blockedCell,
            };
            GridCellView cell = Instantiate(cellPrefab, gridParent.position + new Vector3(spacing.x * (pos.x - center.x), spacing.y * (pos.y - center.y)), Quaternion.identity, gridParent);

            if (cellType == TileType.PLAYER_SPAWN)
            {
                heroSpawnPostions.Add(new(pos.x, pos.y));
            }
            if (cellType == TileType.ENEMY_SPAWN)
            {
                enemySpawnPositions.Add(new(pos.x, pos.y));
            }
            cell.name = "Cell " + pos.x + " " + pos.y;
            cell.Setup(pos.x, pos.y);
            grid.Add(new(pos.x, pos.y), cell);
        }

        List<EnemyData> possibleEnemyDatas = DataSystem.instance.enemies;
        List<EnemyData> chosenEnemyDatas = new();
        for (int i = 0; i < enemySpawnPositions.Count; i++)
        {
            EnemyData chosenEnemy = possibleEnemyDatas.GetRandom();
            chosenEnemyDatas.Add(chosenEnemy);
        }
        GridUnitSystem.instance.Setup(DataSystem.instance.heroes[0], chosenEnemyDatas);
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
        int rand = Random.Range(0, heroSpawnPostions.Count);
        Vector2 cell = heroSpawnPostions[rand];
        GridUnitSystem.instance.hero.Move(cell);
    }

    public void SetRandomEnemyPosition(EnemyGridUnit enemy)
    {
        int rand = Random.Range(0, enemySpawnPositions.Count);
        Vector2 cell = enemySpawnPositions[rand];
        enemy.Move(cell);
    }

    public void SetEnemyPosition(EnemyGridUnit enemy, int index)
    {
        if (index < 0) index = 0;
        if (index > enemySpawnPositions.Count) index %= enemySpawnPositions.Count;

        Vector2 cell = enemySpawnPositions[index];
        enemy.Move(cell);
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

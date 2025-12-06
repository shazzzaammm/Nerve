using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : Singleton<GridSystem>
{
    [SerializeField] private Transform gridParent;
    [SerializeField] private GridCellView walkableCell, blockedCell;
    [SerializeField] private ChestGridCell chestCell;
    [SerializeField] private ExitGridCell exitCell;
    [SerializeField] private BossSpawnGridView bossCell;

    [SerializeField] private Vector2 size;

    [SerializeField] private Texture2D[] layouts;
    [SerializeField] private Texture2D subBossLayout, bossLayout;
    private Vector2 spacing;
    Dictionary<Vector2, GridCellView> grid;
    List<Vector2> enemySpawnPositions;
    List<Vector2> heroSpawnPostions;
    private static Vector2 nullVector = Vector2.one * Int32.MaxValue;
    Vector2 bossSpawnPosition = nullVector;
    public bool bossTime {get; private set;} = false;
    private int levelsCleared = 0;

    void Start()
    {
        Setup();
    }

    private void OnEnable()
    {
        ActionSystem.AttachPerformer<EndMatchGA>(EndMatchGAPerformer);
        ActionSystem.AttachPerformer<ExitLevelGA>(ExitLevelGAPerformer);
    }


    private void OnDisable()
    {
        ActionSystem.DetachPerformer<EndMatchGA>();
        ActionSystem.DetachPerformer<ExitLevelGA>();
    }

    public void Setup()
    {
        DestroyLevel();

        grid = new();
        heroSpawnPostions = new();
        enemySpawnPositions = new();
        Texture2D chosenLayout;
        if (levelsCleared == 4)
        {
            chosenLayout = bossLayout;
        }
        else if (levelsCleared == 2)
        {
            chosenLayout = subBossLayout;
        }
        else
        {
            int randomLayout = UnityEngine.Random.Range(0, layouts.Length);
            chosenLayout = layouts[randomLayout];
        }
        Dictionary<Vector2Int, TileType> layout = LevelGenerationSystem.instance.Generate(chosenLayout);
        spacing = new(size.x / chosenLayout.width, size.y / chosenLayout.height);

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
                TileType.EXIT => exitCell,
                TileType.BOSS_SPAWN => bossCell,
                _ => walkableCell,
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
            if (cellType == TileType.BOSS_SPAWN)
            {
                bossSpawnPosition = new(pos.x, pos.y);
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
        bossTime = bossSpawnPosition != nullVector;
        Debug.Log("Bossitime? " + bossTime + " because pos is " + bossSpawnPosition);
        GridUnitSystem.instance.Setup(DataSystem.instance.heroes[0], chosenEnemyDatas, bossTime);
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
        int rand = UnityEngine.Random.Range(0, heroSpawnPostions.Count);
        Vector2 cell = heroSpawnPostions[rand];
        GridUnitSystem.instance.hero.Move(cell);
    }

    public void SetRandomEnemyPosition(EnemyGridUnit enemy)
    {
        int rand = UnityEngine.Random.Range(0, enemySpawnPositions.Count);
        Vector2 cell = enemySpawnPositions[rand];
        enemy.Move(cell);
    }

    public void SetEnemyPosition(EnemyGridUnit enemy, int index)
    {
        if (index < 0) index = 0;
        if (index >= enemySpawnPositions.Count) index %= enemySpawnPositions.Count;

        Vector2 cell = enemySpawnPositions[index];
        enemy.Move(cell);
    }

    public void SetBossPosition(EnemyGridUnit boss)
    {
        if (bossSpawnPosition != nullVector){
            boss.Move(bossSpawnPosition);
        }
        bossSpawnPosition = nullVector;
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
            cell.gameObject.SetActive(false);
        }
    }

    private void EnableVisuals()
    {
        Interactions.instance.playerCanMoveOnGrid = true;
        if (grid.Count == 0)
        {
            Setup();
            return;
        }

        foreach (var cell in grid.Values)
        {
            cell.gameObject.SetActive(true);
        }
    }

    private void DestroyLevel()
    {
        GridUnitSystem.instance.DestroyAllUnits();
        if (grid == null) return;
        foreach (var cell in grid.Values)
        {
            Destroy(cell.gameObject);
        }
        grid.Clear();
    }

    private IEnumerator EndMatchGAPerformer(EndMatchGA endMatchGA)
    {
        EnableVisuals();
        GridUnitSystem.instance.EnableVisuals();

        yield return null;
    }

    private IEnumerator ExitLevelGAPerformer(ExitLevelGA exitLevelGA)
    {
        levelsCleared = (levelsCleared + 1 ) % 11;
        Setup();
        yield return null;
    }
}

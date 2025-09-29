using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    [SerializeField] private GridCellView gridCellPrefab;

    [SerializeField] private int rows;
    [SerializeField] private int columns;
    [SerializeField] private Vector2 size;
    private Vector2 spacing;
    List<List<GridCellView>> grid;
    void Start()
    {
        Setup();
    }

    public void Setup()
    {
        grid = new();
        spacing = new(size.x / columns, size.y / rows);
        for (int i = 0; i < rows; i++)
        {
            grid.Add(new());
            for (int j = 0; j < columns; j++)
            {
                GridCellView cell = Instantiate(gridCellPrefab, transform.position + new Vector3(spacing.x * j, spacing.y * i) - ((Vector3)size * .5f), Quaternion.identity);
                grid[i].Add(cell);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.softRed;
        Gizmos.DrawWireCube(transform.position, size);
    }
}

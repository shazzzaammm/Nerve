using System.Collections.Generic;
using UnityEngine;

public class EnemyGridUnit : GridUnit
{
    public EnemyData data { get; private set; }
    public void Setup(EnemyData data)
    {
        this.data = data;
        gameObject.GetComponent<SpriteRenderer>().sprite = this.data.image;
    }
    protected List<Vector2> directions = new() {
            Vector2.up,
            Vector2.down,
            Vector2.left,
            Vector2.right,
            new(1, 1),
            new(-1, 1),
            new(1, -1),
            new(-1, -1)
};

    protected List<GridCellView> GetPossibleMoves(int distance)
    {
        List<GridCellView> possibleMoves = new();
        foreach (Vector2 dir in directions)
        {
            GridCellView move = GridSystem.instance.GetCellAtPosition((dir * distance) + positionOnGrid);
            if (move && move.isWalkable()) possibleMoves.Add(move);
        }
        return possibleMoves;
    }
    public void MoveRandomly(int distance)
    {
        List<GridCellView> possibleMoves = GetPossibleMoves(distance);
        MoveOnGridGA moveOnGridGA = new(this, possibleMoves.Count > 0 ? possibleMoves.Draw() : occupiedTile);
        ActionSystem.instance.AddReaction(moveOnGridGA);
    }

    public void MoveTowardsPlayer(int distance)
    {
        List<GridCellView> possibleMoves = GetPossibleMoves(distance);
        Vector2 heroPosition = GridUnitSystem.instance.hero.positionOnGrid;
        possibleMoves.Sort((v1, v2) => (v1.positionOnGrid - heroPosition).sqrMagnitude.CompareTo((v2.positionOnGrid - heroPosition).sqrMagnitude));
        MoveOnGridGA moveOnGridGA = new(this, possibleMoves.Count > 0 ? possibleMoves[0] : occupiedTile);
        ActionSystem.instance.AddReaction(moveOnGridGA);
    }
}

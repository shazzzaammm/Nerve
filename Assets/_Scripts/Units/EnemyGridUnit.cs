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
    public void MoveRandomly(int distance)
    {
        List<Vector2> directions = new();
        directions.Add(Vector2.up);
        directions.Add(Vector2.down);
        directions.Add(Vector2.left);
        directions.Add(Vector2.right);

        List<GridCellView> possibleMoves = new();
        foreach (Vector2 dir in directions)
        {
            GridCellView move = GridSystem.instance.GetCellAtPosition((dir * distance) + positionOnGrid);
            if (move && move.isWalkable()) possibleMoves.Add(move);
        }

        MoveOnGridGA moveOnGridGA = new(this, possibleMoves.Draw());
        ActionSystem.instance.AddReaction(moveOnGridGA);
    }
}

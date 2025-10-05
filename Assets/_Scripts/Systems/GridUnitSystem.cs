using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridUnitSystem : Singleton<GridUnitSystem>
{
    public List<EnemyGridUnit> enemies;//{ get; private set; }
    public HeroGridUnit hero;//{ get; private set; }

    public void Setup()
    {
        foreach (EnemyGridUnit enemy in enemies)
        {
            GridSystem.instance.SetRandomEnemyPosition(enemy);
        }
        GridSystem.instance.SetRandomHeroPosition();
        Interactions.instance.playerCanMoveOnGrid = true;
    }
    private void OnEnable()
    {
        ActionSystem.AttachPerformer<MoveOnGridGA>(MoveOnGridGAPerformer);
        ActionSystem.AttachPerformer<GridEnemyTurnGA>(GridEnemyTurnGAPerformer);
    }
    private void OnDisable()
    {
        ActionSystem.DetachPerformer<MoveOnGridGA>();
        ActionSystem.DetachPerformer<GridEnemyTurnGA>();
    }
    public bool HeroCanReachCell(GridCellView cell)
    {
        float distance = Vector2.Distance(cell.positionOnGrid, hero.positionOnGrid);
        distance = Math.Abs(distance);
        return distance <= hero.cellsPerTurn;
    }


    private IEnumerator MoveOnGridGAPerformer(MoveOnGridGA moveOnGridGA)
    {
        moveOnGridGA.unit.Move(moveOnGridGA.destination.positionOnGrid);

        moveOnGridGA.destination.SetUnit(moveOnGridGA.unit);
        
        if (moveOnGridGA.unit.Equals(hero))
        {
            GridEnemyTurnGA gridEnemyTurnGA = new();
            ActionSystem.instance.AddReaction(gridEnemyTurnGA);
            Interactions.instance.playerCanMoveOnGrid = false;
        }
        yield return null;
    }

    private IEnumerator GridEnemyTurnGAPerformer(GridEnemyTurnGA gridEnemyTurnGA)
    {
        foreach (EnemyGridUnit enemy in enemies)
        {
            enemy.MoveRandomly(1);
        }
        Interactions.instance.playerCanMoveOnGrid = true;

        yield return null;
    }

    public void DisableVisuals(){
        Destroy(hero.gameObject);
        hero = null;
        foreach (EnemyGridUnit enemy in enemies){
            Destroy(enemy.gameObject);
        }
        enemies.Clear();
    }
}

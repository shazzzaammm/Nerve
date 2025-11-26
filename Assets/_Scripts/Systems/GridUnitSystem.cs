using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridUnitSystem : Singleton<GridUnitSystem>
{
    public List<EnemyGridUnit> enemies { get; private set; } = new();
    public HeroGridUnit hero { get; private set; }

    public void Setup(HeroData heroData, List<EnemyData> enemyDatas)
    {
        foreach (EnemyData data in enemyDatas)
        {
            EnemyGridUnit enemy = GridUnitCreator.instance.CreateEnemyUnit(data);
            GridSystem.instance.SetRandomEnemyPosition(enemy);
            GridSystem.instance.SetEnemyPosition(enemy, enemies.Count);
            enemies.Add(enemy);
        }
        hero = GridUnitCreator.instance.CreateHeroUnit(heroData);
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
        if (moveOnGridGA.unit != null)
        {
            moveOnGridGA.unit.Move(moveOnGridGA.destination.positionOnGrid);
            moveOnGridGA.destination.SetUnit(moveOnGridGA.unit);

            if (moveOnGridGA.unit.Equals(hero))
            {
                GridEnemyTurnGA gridEnemyTurnGA = new();
                ActionSystem.instance.AddReaction(gridEnemyTurnGA);
                Interactions.instance.playerCanMoveOnGrid = false;
            }
        }
        yield return null;
    }

    private IEnumerator GridEnemyTurnGAPerformer(GridEnemyTurnGA gridEnemyTurnGA)
    {
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            EnemyGridUnit enemy = enemies[i];
            if (((UnityEngine.Object)enemy) == null)
            {
                enemies.Remove(enemy);
                continue;
            }
            if (enemy.isActiveAndEnabled)
                enemy.MoveRandomly(1);
        }
        Interactions.instance.playerCanMoveOnGrid = true;

        yield return null;
    }

    public void DisableVisuals()
    {
        hero.gameObject.SetActive(false);
        foreach (EnemyGridUnit enemy in enemies)
        {
            if (((UnityEngine.Object)enemy) != null) enemy.gameObject.SetActive(false);
        }
    }

    public void EnableVisuals()
    {
        hero.gameObject.SetActive(true);
        foreach (EnemyGridUnit enemy in enemies)
        {
            if (((UnityEngine.Object)enemy) != null) enemy.gameObject.SetActive(true);
        }
    }

    public void DestroyAllUnits()
    {
        if (hero != null)
        {
            Destroy(hero.gameObject);
            hero = null;
        }
        foreach (EnemyGridUnit enemy in enemies)
        {
            if (((UnityEngine.Object)enemy) != null) Destroy(enemy.gameObject);
        }
        enemies.Clear();
    }
}

using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemySystem : Singleton<EnemySystem>
{
    [SerializeField] private EnemyBoardView enemyBoardView;
    public List<EnemyView> enemies => enemyBoardView.enemyViews;
    void OnEnable()
    {
        ActionSystem.AttachPerformer<EnemyTurnGA>(EnemyTurnPerformer);
        ActionSystem.AttachPerformer<EnemyAttackHeroGA>(AttackHeroPerformer);
        ActionSystem.AttachPerformer<KillEnemyGA>(KillEnemyPerformer);
    }

    void OnDisable()
    {
        ActionSystem.DetachPerformer<EnemyTurnGA>();
        ActionSystem.DetachPerformer<EnemyAttackHeroGA>();
        ActionSystem.DetachPerformer<KillEnemyGA>();
    }

    public void Setup(List<EnemyData> enemyDatas)
    {
        foreach (EnemyData data in enemyDatas)
        {
            enemyBoardView.AddEnemy(data);
        }
    }

    private IEnumerator EnemyTurnPerformer(EnemyTurnGA enemyTurnGA)
    {
        foreach (EnemyView enemy in enemyBoardView.enemyViews)
        {
            enemy.PerformTurn();
            if (enemy.GetStatusEffectStacks(StatusEffectType.Poison) > 0)
            {
                ActionSystem.instance.AddReaction(new ApplyPoisonGA(enemy));
            }
        }
        yield return new WaitForSeconds(.5f);
    }

    private IEnumerator AttackHeroPerformer(EnemyAttackHeroGA enemyAttackHeroGA)
    {
        EnemyView attacker = enemyAttackHeroGA.attacker;
        Vector3 originalPosition = attacker.transform.position;
        Tween tween = attacker.transform.DOMove(originalPosition - (originalPosition.normalized * 2f), .15f);
        yield return tween.WaitForCompletion();
        tween = attacker.transform.DOMove(originalPosition, .5f);
        yield return tween.WaitForCompletion();
        DealDamageGA dealDamageGA = new(enemyAttackHeroGA.damage, new() { HeroSystem.instance.heroView }, enemyAttackHeroGA.attacker);
        ActionSystem.instance.AddReaction(dealDamageGA);
    }

    private IEnumerator KillEnemyPerformer(KillEnemyGA killEnemyGA)
    {
        yield return enemyBoardView.RemoveEnemy(killEnemyGA.target);

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyView : CombatantView
{
    [SerializeField] private TMP_Text intentionText;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private List<AutoTargetEffect> effects;
    private int index = 0;


    private void OnEnable()
    {
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
    }


    public void Setup(EnemyData enemyData)
    {
        base.SetupBase(enemyData.health);
        spriteRenderer.sprite = enemyData.image;
        effects = enemyData.effects;
        index = 0;

        UpdateIntentionText(GetGameAction());
    }

    public void PerformTurn()
    {
        ActionSystem.instance.AddReaction(GetGameAction());
    }

    private void EnemyTurnPostReaction(EnemyTurnGA gA)
    {
        index = (index + 1) % effects.Count;
        UpdateIntentionText(GetGameAction());
    }

    private GameAction GetGameAction()
    {
        AutoTargetEffect autoTargetEffect = effects[index];
        TargetMode tm = autoTargetEffect.targetMode;
        List<CombatantView> targets = tm is CasterTM ? new() { this } : tm.GetTargets();
        GameAction gameAction = autoTargetEffect.effect.GetGameAction(targets, this);
        return gameAction;
    }

    private void UpdateIntentionText(GameAction gameAction)
    {
        String prefix;
        String amount = "";

        if (gameAction is DealDamageGA dealDamageGA)
        {
            prefix = "ATK:";
            amount = dealDamageGA.amount.ToString();
        }
        else if (gameAction is AddShieldGA)
        {
            prefix = "Shielding";
        }
        else if (gameAction is HealGA)
        {
            prefix = "Healing";
        }
        else
        {
            prefix = "????";
        }
        intentionText.text = prefix + " " + amount;
    }
}

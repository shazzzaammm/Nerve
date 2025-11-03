using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Perk  
{
    public Sprite image => data.image;
    private readonly PerkData data;
    private readonly PerkCondition condition;
    private readonly AutoTargetEffect effect;
    public Perk(PerkData data){
        this.data = data;
        condition = data.perkCondition;
        effect = data.autoTargetEffect;
    }

    public void OnAdd(){
        condition.SubscribeCondition(Reaction);
    }
    
    public void OnRemove(){
        condition.UnsubscribeCondition(Reaction);
    }

    private void Reaction(GameAction action)
    {
        if(condition.SubConditionIsMet(action)){
            List<CombatantView> targets = new();
            if (data.useActionCasterAsTarget && action is IHaveCaster haveCaster){
                targets.Add(haveCaster.caster);
            }
            
            if (data.useAutoTarget){
                targets.AddRange(effect.targetMode.GetTargets());
            }
            GameAction perkEffectAction = effect.effect.GetGameAction(targets, HeroSystem.instance.heroView);
            Debug.Log("so we are reacting now?");
            ActionSystem.instance.AddReaction(perkEffectAction);
        }
    }

}

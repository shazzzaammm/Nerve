using System.Collections.Generic;
using UnityEngine;

public class HeroSystem : Singleton<HeroSystem>
{
    [field: SerializeField] public HeroView heroView { get; private set; }
    
    public void Setup(HeroData heroData){
        heroView.Setup(heroData);
    }

    public void EnemyTurnPostReaction(){
        if (heroView.GetStatusEffectStacks(StatusEffectType.Poison) > 0){
            ActionSystem.instance.AddReaction(new ApplyPoisonGA(heroView));
        }
    }
}

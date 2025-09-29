using UnityEngine;

public class HeroView : CombatantView
{
    public void Setup(HeroData heroData){
        base.SetupBase(heroData.health);
    }
}

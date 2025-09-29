using System.Collections.Generic;
using UnityEngine;

public class HeroSystem : Singleton<HeroSystem>
{
    [field: SerializeField] public HeroView heroView { get; private set; }
    
    public void Setup(HeroData heroData){
        heroView.Setup(heroData);
    }
}

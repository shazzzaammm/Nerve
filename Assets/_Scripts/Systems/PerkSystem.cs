using System.Collections.Generic;
using UnityEngine;

public class PerkSystem : Singleton<PerkSystem>
{
    public PerkData perkData;
    private readonly List<Perk> perks = new();
    
    void Start(){
        /*
        Perk perk = new(perkData);
        AddPerk(perk);
        */
    }
    public void AddPerk(Perk perk){
        perks.Add(perk);
        perk.OnAdd();
    }

    public void RemovePerk(Perk perk){
        perks.Remove(perk);
        perk.OnRemove();
    }
}

using System.Collections.Generic;
using SerializeReferenceEditor;
using UnityEngine;

public class AddShieldEffect : Effect
{
    [SerializeField] private int shieldAmount;
    public override GameAction GetGameAction(List<CombatantView> targets, CombatantView caster)
    {
        AddShieldGA addShieldGA = new(shieldAmount, targets, caster);
        return addShieldGA;
    }
}

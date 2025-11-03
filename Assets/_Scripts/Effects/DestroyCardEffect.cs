using System.Collections.Generic;
using SerializeReferenceEditor;
using UnityEngine;

public class DestroyCardEffect : Effect
{
    [SerializeReference, SR] DestroyCardMode destroyCardMode;
    public override GameAction GetGameAction(List<CombatantView> targets, CombatantView caster)
    {
        DestroyCardGA destroyCardGA = new(destroyCardMode.GetCard());
        return destroyCardGA;
    }
}

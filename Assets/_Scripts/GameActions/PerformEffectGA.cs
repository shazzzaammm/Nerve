using System.Collections.Generic;
using UnityEngine;

public class PerformEffectGA : GameAction
{
    public Effect effect { get; set; }
    public List<CombatantView> targets  { get; set; }
    public Card card;
    public PerformEffectGA(Effect effect, List<CombatantView> targets, Card card){
        this.effect = effect;
        this.targets = targets == null ? null : new (targets);
        this.card = card;
    }
}

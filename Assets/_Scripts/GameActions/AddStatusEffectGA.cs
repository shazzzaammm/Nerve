using System.Collections.Generic;

public class AddStatusEffectGA : GameAction
{
    public StatusEffectType statusEffectType {get;private set;}
    public int stackCount {get; private set;}
    public List<CombatantView> targets {get; private set;}
    public AddStatusEffectGA(StatusEffectType type, int stacks, List<CombatantView> targets){
        statusEffectType = type;
        stackCount = stacks;
        this.targets = targets;
    }
}

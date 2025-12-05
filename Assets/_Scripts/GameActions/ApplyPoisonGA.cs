public class ApplyPoisonGA : GameAction
{
    public CombatantView target {get; private set;}
    public ApplyPoisonGA(CombatantView target){
        this.target = target;
    }
}

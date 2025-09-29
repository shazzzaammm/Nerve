using UnityEngine;

public class PlayCardGA : GameAction
{
    public EnemyView manualTarget { get; private set; }
    public Card card { get; set; }
    public PlayCardGA(Card card){
        this.card = card;
        this.manualTarget = null;
    }
    public PlayCardGA(Card card, EnemyView manualTarget){
        this.card = card;
        this.manualTarget = manualTarget;
    }
}

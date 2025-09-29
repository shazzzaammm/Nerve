using UnityEngine;

public class DrawCardsGA : GameAction{
    public int amount { get; private set; }

    public DrawCardsGA(int amount){
        this.amount = amount;
    }
}


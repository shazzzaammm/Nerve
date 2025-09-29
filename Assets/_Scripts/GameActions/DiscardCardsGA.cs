using System.Collections.Generic;

public class DiscardCardsGA : GameAction
{
    public int amount;
    public List<Card> cards;

    public DiscardCardsGA(List<Card> cards)
    {
        this.cards = cards;
        this.amount = cards.Count;
    }
}

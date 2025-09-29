using System.Collections.Generic;
using UnityEngine;

public class DiscardButtonUI : MonoBehaviour
{
    public void OnClick()
    {
        List<Card> cards = new();
        List<CardView> cardViews = CardViewSelectSystem.instance.selectedCards;

        if (cardViews.Count == 0) return;

        foreach (CardView cardView in cardViews)
        {
            cards.Add(cardView.card);
        }
        DiscardCardsGA discardCardsGA = new(cards);
        ActionSystem.instance.Perform(discardCardsGA);
    }
}
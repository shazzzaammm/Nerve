using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardViewSelectSystem : Singleton<CardViewSelectSystem>
{
    public readonly List<CardView> selectedCards = new();

    public void ToggleCardSelection(CardView cardView)
    {
        if (selectedCards.Contains(cardView))
        {
            selectedCards.Remove(cardView);
        }
        else
        {
            selectedCards.Add(cardView);
        }
    }

    void OnEnable()
    {
        ActionSystem.SubscribeReaction<DiscardCardsGA>(DiscardCardsReaction, ReactionTiming.POST);
    }

    void OnDisable()
    {
        ActionSystem.UnsubscribeReaction<DiscardCardsGA>(DiscardCardsReaction, ReactionTiming.POST);
    }

    void DiscardCardsReaction(DiscardCardsGA discardCardsGA)
    {
        selectedCards.Clear();
    }

    public void DeselectCardView(CardView cardView)
    {
        if (selectedCards.Contains(cardView))
            selectedCards.Remove(cardView);
    }
}

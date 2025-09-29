using System.Collections.Generic;
using UnityEngine;

public class MatchSetupSystem : Singleton<MatchSetupSystem>
{
    [SerializeField] private HeroData heroData;
    [SerializeField] private List<EnemyData> enemyDatas;
    private List<CardData> deck;

    protected override void Awake()
    {
        base.Awake();
        deck = heroData.deck;
    }
    void Start()
    {
        // StartMatch();
    }

    public void StartMatch()
    {
        HeroSystem.instance.Setup(heroData);
        CardSystem.instance.Setup(heroData.deck);
        EnemySystem.instance.Setup(enemyDatas);
        DrawCardsGA drawCardsGA = new(CardSystem.instance.handSize);
        ActionSystem.instance.Perform(drawCardsGA);
    }
    public void RemoveCardFromDeck(Card card)
    {
        deck.Remove(card.data);
    }
}

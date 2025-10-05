using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchSetupSystem : Singleton<MatchSetupSystem>
{
    [SerializeField] private HeroData heroData;
    [SerializeField] private List<EnemyData> enemyDatas;
    private List<CardData> deck;

    
    private void OnEnable() {
        ActionSystem.AttachPerformer<StartMatchGA>(StartMatchGAPerformer);
    }
    private void OnDisable() {
        
        ActionSystem.DetachPerformer<StartMatchGA>();
        
    }
    private IEnumerator StartMatchGAPerformer(StartMatchGA startMatchGA)
    {
        deck = new List<CardData>(heroData.deck);
        GridSystem.instance.DisableVisuals();
        GridUnitSystem.instance.DisableVisuals();
        heroData = startMatchGA.hero;
        enemyDatas = startMatchGA.enemies;
        HeroSystem.instance.Setup(heroData);
        CardSystem.instance.Setup(heroData.deck);
        EnemySystem.instance.Setup(enemyDatas);
        DrawCardsGA drawCardsGA = new(CardSystem.instance.handSize);
        ActionSystem.instance.AddReaction(drawCardsGA);
        yield return null;
    }
    public void RemoveCardFromDeck(Card card)
    {
        deck.Remove(card.data);
    }
}

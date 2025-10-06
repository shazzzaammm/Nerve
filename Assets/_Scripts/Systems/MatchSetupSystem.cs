using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchSetupSystem : Singleton<MatchSetupSystem>
{
    private HeroData heroData;
    private List<EnemyData> enemyDatas;
    private List<CardData> deck;
    [SerializeField] private GameObject matchUI;
    [SerializeField] private GameObject matchViews;


    private void OnEnable()
    {
        ActionSystem.AttachPerformer<StartMatchGA>(StartMatchGAPerformer);
        ActionSystem.SubscribeReaction<EndMatchGA>(EndMatchGAPreReaction, ReactionTiming.PRE);
    }
    private void OnDisable()
    {
        ActionSystem.DetachPerformer<StartMatchGA>();
    }
    private void EndMatchGAPreReaction(EndMatchGA endMatchGA)
    {
        matchUI.SetActive(false);
        matchViews.SetActive(false);
    }
    private IEnumerator StartMatchGAPerformer(StartMatchGA startMatchGA)
    {
        // data
        heroData = startMatchGA.hero;
        enemyDatas = startMatchGA.enemies;
        deck = new List<CardData>(heroData.deck);

        // visuals
        matchUI.SetActive(true);
        matchViews.SetActive(true);

        // grid
        GridSystem.instance.DisableVisuals();
        GridUnitSystem.instance.DisableVisuals();

        // hero/enemy
        HeroSystem.instance.Setup(heroData);
        EnemySystem.instance.Setup(enemyDatas);

        //cards
        CardSystem.instance.Setup(heroData.deck);
        DrawCardsGA drawCardsGA = new(CardSystem.instance.handSize);
        ActionSystem.instance.AddReaction(drawCardsGA);
        
        // mana
        RefillManaGA refillManaGA = new();
        ActionSystem.instance.AddReaction(refillManaGA);

        yield return null;
    }
    public void RemoveCardFromDeck(Card card)
    {
        deck.Remove(card.data);
    }
}

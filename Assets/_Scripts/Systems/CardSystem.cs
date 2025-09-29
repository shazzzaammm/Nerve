using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class CardSystem : Singleton<CardSystem>
{
    [SerializeField] private HandView handView;
    [SerializeField] private Transform drawPilePoint;
    [SerializeField] private Transform discardPilePoint;

    public int handSize { get; private set; } = 5;
    private readonly List<Card> drawPile = new();
    private readonly List<Card> discardPile = new();
    public readonly List<Card> hand = new();

    public Card playedCard { get; private set; } = null;
    [SerializeField] private Material dissolveShader;



    void OnEnable()
    {
        ActionSystem.AttachPerformer<DrawCardsGA>(DrawCardsPerformer);
        ActionSystem.AttachPerformer<DiscardCardsGA>(DiscardCardsPerformer);
        ActionSystem.AttachPerformer<PlayCardGA>(PlayCardPerformer);
        ActionSystem.AttachPerformer<DestroyCardGA>(DestroyCardPerformer);

        ActionSystem.SubscribeReaction<DiscardCardsGA>(DiscardCardPostReaction, ReactionTiming.POST);
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
    }

    void OnDisable()
    {
        ActionSystem.DetachPerformer<DrawCardsGA>();
        ActionSystem.DetachPerformer<DiscardCardsGA>();
        ActionSystem.DetachPerformer<DestroyCardGA>();
        ActionSystem.DetachPerformer<PlayCardGA>();

        ActionSystem.UnsubscribeReaction<DiscardCardsGA>(DiscardCardPostReaction, ReactionTiming.POST);
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);

    }

    private void EnemyTurnPostReaction(EnemyTurnGA enemyTurnGA)
    {
        int cardsToDraw = handSize - hand.Count;
        if (cardsToDraw <= 0) return;

        DrawCardsGA drawCardsGA = new(cardsToDraw);
        RefillManaGA refillManaGA = new();
        ActionSystem.instance.AddReaction(refillManaGA);
        ActionSystem.instance.AddReaction(drawCardsGA);
    }

    public void Setup(List<CardData> deckData)
    {
        foreach (CardData cardData in deckData)
        {
            Card card = new(cardData);
            drawPile.Add(card);
        }
        DeckUI.instance.UpdateDeckUI(drawPile);
    }

    private IEnumerator PlayCardPerformer(PlayCardGA playCardGA)
    {
        Card card = playCardGA.card;
        hand.Remove(card);
        playedCard = card;
        CardView cardView = handView.RemoveCard(card);
        CardViewSelectSystem.instance.DeselectCardView(cardView);

        SpendManaGA spendManaGA = new(card.cost);
        ActionSystem.instance.AddReaction(spendManaGA);

        if (card.manualTargetEffect != null)
        {
            List<CombatantView> targets = new() { playCardGA.manualTarget };
            PerformEffectGA performEffectGA = new(card.manualTargetEffect, targets, card);
            ActionSystem.instance.AddReaction(performEffectGA);
        }
        foreach (AutoTargetEffect effectWrapper in card.otherEffects)
        {
            List<CombatantView> targets = effectWrapper.targetMode.GetTargets();
            PerformEffectGA performEffectGA = new(effectWrapper.effect, targets, card);
            ActionSystem.instance.AddReaction(performEffectGA);
        }
        yield return DiscardCard(cardView);
    }

    private IEnumerator DrawCardsPerformer(DrawCardsGA drawCardsGA)
    {
        int actualAmount = Mathf.Min(drawCardsGA.amount, drawPile.Count);
        int notDrawnAmount = drawCardsGA.amount - actualAmount;
        for (int i = 0; i < actualAmount; i++)
        {
            yield return DrawCard();
        }
        if (notDrawnAmount > 0)
        {
            yield return RefillDeck();
            for (int i = 0; i < notDrawnAmount; i++)
            {
                yield return DrawCard();
            }
        }
    }


    private IEnumerator DiscardCardsPerformer(DiscardCardsGA discardCardsGA)
    {
        foreach (Card card in discardCardsGA.cards)
        {
            CardView cardView = handView.RemoveCard(card);
            yield return DiscardCard(cardView);
        }
    }

    private IEnumerator DestroyCardPerformer(DestroyCardGA destroyCardGA)
    {
        Card card = destroyCardGA.card;
        discardPile.Remove(card);
        MatchSetupSystem.instance.RemoveCardFromDeck(card);
        yield return null;
    }

    private void DiscardCardPostReaction(DiscardCardsGA discardCardsGA)
    {
        DrawCardsGA drawCardsGA = new(discardCardsGA.amount);
        ActionSystem.instance.AddReaction(drawCardsGA);
    }
    private IEnumerator DrawCard()
    {
        Card card = drawPile.Draw();
        DeckUI.instance.UpdateDeckUI(drawPile);
        hand.Add(card);
        CardView cardView = CardViewCreator.instance.CreateCardView(card, drawPilePoint.position, drawPilePoint.rotation);
        yield return handView.AddCard(cardView);
    }
    private IEnumerator DiscardCard(CardView cardView)
    {
        discardPile.Add(cardView.card);
        cardView.transform.DOScale(Vector3.zero, .15f);
        Tween tween = cardView.transform.DOMove(discardPilePoint.position, .15f);
        yield return tween.WaitForCompletion();
        hand.Remove(cardView.card);
        Destroy(cardView.gameObject);

    }
    private IEnumerator RefillDeck()
    {
        drawPile.AddRange(discardPile);
        DeckUI.instance.UpdateDeckUI(drawPile);
        foreach (Card card in discardPile)
        {
            CardView cardView = CardViewCreator.instance.CreateCardView(card, discardPilePoint.position, discardPilePoint.rotation);
            cardView.transform.DOMove(drawPilePoint.position, .5f);
            cardView.transform.DOScale(Vector3.one * .75f, .2f);
            cardView.transform.DORotate(drawPilePoint.rotation.eulerAngles, .5f).onComplete += () =>
            {
                Destroy(cardView.gameObject);
            };


            yield return new WaitForSeconds(.1f);
        }
        yield return new WaitForSeconds(discardPile.Count * .125f);
        discardPile.Clear();
    }
}

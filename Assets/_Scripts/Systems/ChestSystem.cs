using System.Collections.Generic;
using UnityEngine;

public class ChestSystem : Singleton<ChestSystem>
{
    [SerializeField] private GameObject chestUI;
    [SerializeField] private Transform cardRewardParent;
    [SerializeField] private CardRewardUI cardRewardPrefab;
   private List<CardRewardUI> cardRewards = new();

    public void Setup(List<CardData> cardDatas){
        Interactions.instance.playerCanMoveOnGrid = false;
        foreach(CardData data in cardDatas){
            CardRewardUI cardReward = Instantiate(cardRewardPrefab, cardRewardParent.position, cardRewardParent.rotation, cardRewardParent);
            cardReward.Setup(new(data));
            cardRewards.Add(cardReward);
        }

        chestUI.SetActive(true);
    }
    
    public void ChooseCard(CardRewardUI cardReward){
        Debug.Log("Time to add " + cardReward.card.title + " to the deck");
        AddCardToDeckGA addCardToDeckGA = new(cardReward.card);
        ActionSystem.instance.Perform(addCardToDeckGA);
        TakeDown();
    }

    public void Skip(){
        TakeDown();
    }

    private void TakeDown(){
        chestUI.SetActive(false);
        foreach (CardRewardUI cardReward in cardRewards){
            Destroy(cardReward.gameObject);
        }
        cardRewards.Clear();
        Interactions.instance.playerCanMoveOnGrid = true;
    }
}

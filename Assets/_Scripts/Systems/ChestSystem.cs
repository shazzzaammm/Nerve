using System.Collections.Generic;
using UnityEngine;

public class ChestSystem : Singleton<ChestSystem>
{
    [SerializeField] private GameObject chestUI;
    [SerializeField] private Transform cardRewardParent;
    [SerializeField] private CardRewardUI cardRewardPrefab;
   private List<CardRewardUI> cardRewards = new();

    public void Setup(List<CardData> cardDatas){
        foreach(CardData data in cardDatas){
            CardRewardUI cardReward = Instantiate(cardRewardPrefab, cardRewardParent.position, cardRewardParent.rotation, cardRewardParent);
            cardReward.Setup(new(data));
            cardRewards.Add(cardReward);
        }

        chestUI.SetActive(true);
    }
    
    public void ChooseCard(CardRewardUI cardReward){
        // TODO add card to deck using GA
        Debug.Log("Time to add " + cardReward.card.title + " to the deck");
        Skip();
    }

    public void Skip(){
        chestUI.SetActive(false);
        foreach (CardRewardUI cardReward in cardRewards){
            Destroy(cardReward.gameObject);
        }
        cardRewards.Clear();
    }
}

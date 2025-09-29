using System.Collections.Generic;
using UnityEngine;

public class DeckUI : Singleton<DeckUI>
{
    [SerializeField] private CardDeckUI deckUIPrefab;
    [SerializeField] private Transform grid;
    [SerializeField] private GameObject visualsWrapper;
    public bool isUIActive { get; private set; } = false;


     void Start() {
        SetUIActive(false);
    }
    public void SetUIActive(bool set){
        visualsWrapper.SetActive(set);
        isUIActive = set;
    }
    public void UpdateDeckUI(List<Card> deck){
        for (int i = 0; i < grid.childCount; i++)
        {
            Destroy(grid.GetChild(i).gameObject);
        }
        
        foreach (Card card in deck){
            CardDeckUI deckUI = Instantiate(deckUIPrefab, grid.position, grid.rotation, grid);
            deckUI.Setup(card);
        }
    }
}

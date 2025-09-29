using System.Collections.Generic;
using UnityEngine;

public class Card{

    public readonly CardData data;
    public string title => data.title;
    public string description => data.description;
    public Sprite image => data.image;
    public int cost { get; private set; }
    public Effect manualTargetEffect => data.manualTargetEffect;
    public List<AutoTargetEffect> otherEffects => data.otherEffects;

    public Card(CardData cardData){
        data = cardData;
        cost = data.cost;
    }
}

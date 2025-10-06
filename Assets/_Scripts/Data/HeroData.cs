using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Data/Hero")]
public class HeroData : ScriptableObject
{
    [field: SerializeField] public int health { get; private set; }
    [field: SerializeField] public List<CardData> deck;
    [field: SerializeField] public Sprite image;
}

using System.Collections.Generic;
using SerializeReferenceEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Card")]
public class CardData : ScriptableObject
{
    [field: SerializeField] public string title { get; private set; }
    [field: SerializeField] public string description { get; private set; }
    [field: SerializeField] public int cost { get; private set; }
    [field: SerializeField] public Sprite image { get; private set; }
    [field: SerializeReference, SR] public Effect manualTargetEffect { get; private set; } = null;
    [field: SerializeField] public List<AutoTargetEffect> otherEffects { get; private set; }
}

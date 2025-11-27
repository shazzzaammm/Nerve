using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Enemy")]
public class EnemyData : ScriptableObject
{
    [field: SerializeField] public int health { get; private set; }
    [field: SerializeField] public int initialShield { get; private set; }
    [field: SerializeField] public int attackPower { get; private set; }
    [field: SerializeField] public Sprite image { get; private set; }
    // In order of how it happens
    [field: SerializeField] public List<AutoTargetEffect> effects { get; private set; }
}
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Enemy")]
public class EnemyData : ScriptableObject
{
    [field: SerializeField] public int health { get; private set; }
    [field: SerializeField] public int initialShield { get; private set; }
    [field: SerializeField] public int attackPower { get; private set; }
    [field: SerializeField] public Sprite image { get; private set; }
}
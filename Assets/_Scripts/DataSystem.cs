using System.Collections.Generic;
using UnityEngine;

public class DataSystem : Singleton<DataSystem>
{
    [SerializeField] public List<HeroData> heroes;
    [SerializeField] public List<EnemyData> enemies;
    [SerializeField] public List<CardData> cards;
}

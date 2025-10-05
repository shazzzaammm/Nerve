using System.Collections.Generic;
using UnityEngine;

public class StartMatchGA : GameAction
{
    public List<EnemyData> enemies;
    public HeroData hero;

    public StartMatchGA(HeroData hero, List<EnemyData> enemies)
    {
        this.hero = hero;
        this.enemies = enemies;
    }
}

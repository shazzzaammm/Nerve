using System.Collections.Generic;
using UnityEngine;

public class KillEnemyGA : GameAction
{
    public EnemyView target { get; private set; }
    
    public KillEnemyGA(EnemyView target){
        this.target = target;
    }

}

using UnityEngine;

public class GridUnitCreator : Singleton<GridUnitCreator>
{
    [SerializeField] private HeroGridUnit heroGridUnitPrefab;
    [SerializeField] private EnemyGridUnit enemyGridUnitPrefab;


    public HeroGridUnit CreateHeroUnit(HeroData data){
        HeroGridUnit hero = Instantiate(heroGridUnitPrefab, transform.position, Quaternion.identity);
        hero.Setup(data);
        return hero;
    }

    public EnemyGridUnit CreateEnemyUnit(EnemyData data){
        EnemyGridUnit enemy = Instantiate(enemyGridUnitPrefab, transform.position, Quaternion.identity);
        enemy.Setup(data);
        return enemy;
    }
}

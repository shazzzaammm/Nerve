using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyBoardView : MonoBehaviour
{
    [SerializeField] private List<Transform> slots;
    public List<EnemyView> enemyViews { get; private set; } = new();

    public void AddEnemy(EnemyData enemyData){
        Transform slot = slots[enemyViews.Count];
        EnemyView enemyView = EnemyViewCreator.instance.CreateEnemyView(enemyData, slot.position, slot.rotation);
        enemyView.transform.parent = slot;
        enemyViews.Add(enemyView);
    }
    
    public IEnumerator RemoveEnemy(EnemyView enemyView){
        enemyViews.Remove(enemyView);
        Tween tween = enemyView.transform.DOScale(Vector3.zero, .5f);
        yield return tween.WaitForCompletion();
        Destroy(enemyView.gameObject);
    }
}

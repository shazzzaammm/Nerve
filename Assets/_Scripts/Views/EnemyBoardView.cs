using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyBoardView : MonoBehaviour
{
    [SerializeField] private List<Transform> slots;
    public List<EnemyView> enemyViews { get; private set; } = new();

    public void AddEnemy(EnemyData enemyData){
        if (enemyViews.Count > slots.Count){
            Debug.Log("EnemyBoardView Slots full!!");
            return;
        }
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
        if (enemyViews.Count == 0){
            EndMatchGA endMatchGA = new();
            ActionSystem.instance.AddReaction(endMatchGA);
        }
    }
}

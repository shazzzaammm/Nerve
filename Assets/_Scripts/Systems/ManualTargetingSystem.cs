using UnityEngine;

public class ManualTargetingSystem : Singleton<ManualTargetingSystem>
{
    [SerializeField] private ArrowView arrowView;
    [SerializeField] private LayerMask targetLayerMask;
    public CombatantView targetedEnemyView {get; private set;}
    public void StartTargeting(Vector3 startPosition)
    {
        arrowView.gameObject.SetActive(true);
        arrowView.SetupArrow(startPosition);
    }

    public EnemyView EndTargeting(Vector3 endPosition)
    {
        arrowView.gameObject.SetActive(false);
        RaycastHit2D hit = Physics2D.Raycast(endPosition, Vector3.forward, 10f, targetLayerMask);

        if (hit && hit.collider != null && hit.transform.TryGetComponent<EnemyView>(out EnemyView enemyView))
        {
            targetedEnemyView = enemyView;
            return enemyView;
        }
        targetedEnemyView = null;
        return null;
    }
}

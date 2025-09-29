using UnityEngine;

public class ArrowView : MonoBehaviour
{
    [SerializeField] private GameObject arrowHead;
    [SerializeField] private LineRenderer line;
    private Vector3 startPosition;
    private void Update()
    {
        Vector3 endPosition = MouseUtil.GetMousePositionInWorldSpace();
        Vector3 direction = -(startPosition - arrowHead.transform.position).normalized;
        line.SetPosition(1, endPosition - direction * .5f);
        arrowHead.transform.position = endPosition;
        arrowHead.transform.right = direction;
    }
    public void SetupArrow(Vector3 startPosition){
        this.startPosition = startPosition;
        line.SetPosition(0, this.startPosition);
        line.SetPosition(1, MouseUtil.GetMousePositionInWorldSpace());
    }
}

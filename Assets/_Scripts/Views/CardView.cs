using DG.Tweening;
using TMPro;
using UnityEngine;

public class CardView : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text cost;
    [SerializeField] private SpriteRenderer image;
    [SerializeField] private GameObject wrapper;
    [SerializeField] private GameObject infoWrapper;
    [SerializeField] private LayerMask dropLayer;

    private Vector3 dragStartPosition;
    private Quaternion dragStartRotation;
    private bool selected = false;

    public Card card { get; private set; }

    public void Setup(Card card)
    {
        this.card = card;
        title.text = card.title;
        description.text = card.description;
        cost.text = card.cost.ToString();
        image.sprite = card.image;
    }

    void OnMouseEnter()
    {
        if (!Interactions.instance.PlayerCanHover()) return;
        Vector3 desiredPosition = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
        CardViewHoverSystem.instance.Show(card, desiredPosition);
        wrapper.SetActive(false);
    }

    void OnMouseExit()
    {
        if (!Interactions.instance.PlayerCanHover()) return;
        CardViewHoverSystem.instance.Hide();
        wrapper.SetActive(true);
    }

    void OnMouseDown()
    {
        if (Interactions.instance.PlayerCanSelectCard())
        {
            CardViewSelectSystem.instance.ToggleCardSelection(this);
            selected = !selected;
        }

        if (card.manualTargetEffect != null)
        {
            ManualTargetingSystem.instance.StartTargeting(transform.position);
        }
        else
        {

            if (Interactions.instance.PlayerCanInteract())
            {
                Interactions.instance.playerIsDragging = true;
                wrapper.SetActive(true);
                CardViewHoverSystem.instance.Hide();
                dragStartPosition = transform.position;
                dragStartRotation = transform.rotation;
                transform.position = MouseUtil.GetMousePositionInWorldSpace(-1);
                transform.rotation = Quaternion.identity;
            }
        }
    }

    void OnMouseDrag()
    {
        if (!Interactions.instance.PlayerCanInteract()) return;
        if (card.manualTargetEffect != null) return;
        transform.position = MouseUtil.GetMousePositionInWorldSpace(-1);
    }

    void OnMouseUp()
    {
        if (!Interactions.instance.PlayerCanInteract()) return;
        if (card.manualTargetEffect != null)
        {
            EnemyView target = ManualTargetingSystem.instance.EndTargeting(MouseUtil.GetMousePositionInWorldSpace(-1));
            if (target != null && ManaSystem.instance.PlayerHasEnoughMana(card.cost)){
                PlayCardGA playCardGA = new(card, target);
                ActionSystem.instance.Perform(playCardGA);
            }
        }
        else
        {

            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.forward, 10f, dropLayer);
            if (hit && ManaSystem.instance.PlayerHasEnoughMana(card.cost))
            {
                PlayCardGA playCardGA = new(card);
                ActionSystem.instance.Perform(playCardGA);
            }
            else
            {
                transform.DOMove(dragStartPosition, .1f);
                transform.DORotate(dragStartRotation.eulerAngles, .1f);
            }
            Interactions.instance.playerIsDragging = false;
        }
    }
}

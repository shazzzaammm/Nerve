using UnityEngine;

public class CardViewHoverSystem : Singleton<CardViewHoverSystem>
{
    [SerializeField] private CardView cardViewHover;

    void Start()
    {
        Hide();
    }
    public void Show(Card card, Vector3 position)
    {
        cardViewHover.Setup(card);
        cardViewHover.gameObject.SetActive(true);
        cardViewHover.transform.position = position;
    }

    public void Hide()
    {
        cardViewHover.gameObject.SetActive(false);
    }
}

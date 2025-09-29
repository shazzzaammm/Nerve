using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDeckUI : MonoBehaviour
{
    [SerializeField] private TMP_Text costText;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private Image image;

    public void Setup(Card card)
    {
        costText.text = card.cost.ToString();
        titleText.text = card.title;
        descriptionText.text = card.description;
        image.sprite = card.image;
    }
}

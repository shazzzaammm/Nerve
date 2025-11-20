using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardRewardUI : MonoBehaviour
{
    [SerializeField] private TMP_Text costText;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private Image image;
    [SerializeField] private GameObject informationWrapper;

    private Button button;

    public Card card { get; private set; }

    private void ButtonSetup(){
        button = GetComponent<Button>();
        button.onClick.AddListener(OnMouseDown);
    }
    public void Setup(Card card){
        ButtonSetup();
        this.card = card;
        costText.text = card.cost.ToString();
        titleText.text = card.title;
        descriptionText.text = card.description;
        image.sprite = card.image;
    }

    void OnMouseEnter() {
        informationWrapper.SetActive(true);
    }
    void OnMouseExit() {
        informationWrapper.SetActive(false);
    }

    void OnMouseDown(){
        ChestSystem.instance.ChooseCard(this);
    }

}

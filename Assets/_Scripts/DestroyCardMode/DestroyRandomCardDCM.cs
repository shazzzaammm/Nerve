using System.Collections.Generic;

public class DestroyRandomCardDCM : DestroyCardMode
{
    public override Card GetCard()
    {

        Card randomCard = CardSystem.instance.hand[UnityEngine.Random.Range(0, CardSystem.instance.hand.Count)];
        return randomCard;
    }
}

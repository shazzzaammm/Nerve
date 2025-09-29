using System.Collections.Generic;

public class DestroySelfDCM : DestroyCardMode
{
    public override Card GetCard()
    {
        return CardSystem.instance.playedCard;
    }
}

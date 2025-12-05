using UnityEngine;

public class Interactions : Singleton<Interactions>
{
    public bool playerIsDragging { get; set; }
    public bool playerCanMoveOnGrid { get; set; }
    public bool PlayerCanInteract()
    {
        if (ActionSystem.instance.isPerforming) return false;
        if (DeckUI.instance != null && DeckUI.instance.isUIActive) return false;
        if (UISystem.instance.paused || UISystem.instance.gameOver) return false;
        if (ChestSystem.instance.isChoosingReward) return false;
        return true;
    }
    public bool PlayerCanHover()
    {
        if (playerIsDragging) return false;
        if (!PlayerCanInteract()) return false;
        return true;
    }

    public bool PlayerCanSelectCard()
    {
        if (!PlayerCanInteract()) return false;
        if (playerIsDragging) return false;
        return true;
    }
}

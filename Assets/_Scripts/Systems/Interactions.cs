using UnityEngine;

public class Interactions : Singleton<Interactions>
{
    public bool playerIsDragging { get;  set; }
    public bool PlayerCanInteract(){
        if(ActionSystem.instance.isPerforming) return false;
        if(DeckUI.instance.isUIActive) return false;
        return true;
    }
    
    public bool PlayerCanHover(){
        if (playerIsDragging) return false;
        if (!PlayerCanInteract()) return false;
        return true;
    }

    public bool PlayerCanSelectCard(){
        if (!PlayerCanInteract()) return false;
        if (playerIsDragging) return false;
        return true;
    }
}

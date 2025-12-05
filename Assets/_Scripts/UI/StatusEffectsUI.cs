using System.Collections.Generic;
using UnityEngine;

public class StatusEffectsUI : MonoBehaviour
{
    [SerializeField] private StatusEffectUI statusEffectPrefab;
    [SerializeField] private Sprite armorSprite, poisonSprite;

    private Dictionary<StatusEffectType, StatusEffectUI> statusEffectUIs = new();

    public void UpdateStatusEffectsUI(StatusEffectType statusEffectType, int stackCount)
    {
        if (stackCount == 0)
        {
            if (statusEffectUIs.ContainsKey(statusEffectType))
            {
                StatusEffectUI statusEffectUI = statusEffectUIs[statusEffectType];
                statusEffectUIs.Remove(statusEffectType);
                Destroy(statusEffectUI.gameObject);
            }
            else
            {
                if (!statusEffectUIs.ContainsKey(statusEffectType))
                {
                    StatusEffectUI statusEffectUI = Instantiate(statusEffectPrefab, transform);
                    statusEffectUIs.Add(statusEffectType, statusEffectUI);
                }
                Sprite sprite = GetSpriteByType(statusEffectType);
                statusEffectUIs[statusEffectType].Set(sprite, stackCount);
            }
        }
    }
    
    private Sprite GetSpriteByType(StatusEffectType type){

        return type switch
        {
            StatusEffectType.Armor => armorSprite,
            StatusEffectType.Poison => poisonSprite,
            _ => null
        };
    }
}

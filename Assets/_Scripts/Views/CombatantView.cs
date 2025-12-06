using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CombatantView : MonoBehaviour
{
    [SerializeField] protected TMP_Text healthText;
    [SerializeField] protected StatusEffectsUI statusEffectsUI;
    [SerializeField] protected Slider healthBar;
    private Dictionary<StatusEffectType, int> statusEffects = new();
    public int maxHealth { get; protected set; }
    public int currentHealth { get; protected set; }
    protected void SetupBase(int health)
    {
        maxHealth = currentHealth = health;
        UpdateHealthText();
    }

    protected void UpdateHealthText()
    {
        healthText.text = currentHealth + "/" + maxHealth;
        healthBar.value = (float)currentHealth / maxHealth;
    }

    public void TakeDamage(int amount)
    {
        int oldShield = GetStatusEffectStacks(StatusEffectType.Armor);
        int currentShield = oldShield;
        if (currentShield > 0) currentShield -= amount;
        else currentHealth -= amount;

        if (currentShield < 0)
        {
            currentHealth += currentShield;
            currentShield = 0;
        }
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        RemoveStatusEffect(StatusEffectType.Armor, oldShield - currentShield);
        UpdateHealthText();
        statusEffectsUI.UpdateStatusEffectsUI(StatusEffectType.Armor, currentShield);

        transform.DOShakePosition(.5f, .25f);
    }

    public void HealDamage(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UpdateHealthText();

        transform.DOMoveY(transform.position.y + .5f, .25f).onComplete += () =>
        {
            transform.DOMoveY(transform.position.y - .5f, .25f);
        };
    }

    public void AddStatusEffect(StatusEffectType type, int stackCount)
    {
        if (statusEffects.ContainsKey(type))
        {
            statusEffects[type] += stackCount;
        }
        else
        {
            statusEffects.Add(type, stackCount);
        }
        statusEffectsUI.UpdateStatusEffectsUI(type, GetStatusEffectStacks(type));
    }

    public void RemoveStatusEffect(StatusEffectType type, int stackCount)
    {
        if (statusEffects.ContainsKey(type))
        {
            statusEffects[type] -= stackCount;
            if (statusEffects[type] <= 0)
            {
                statusEffects.Remove(type);
            }
        }
        statusEffectsUI.UpdateStatusEffectsUI(type, GetStatusEffectStacks(type));
    }

    public void ClearStatusEffects()
    {
        var keys = statusEffects.Keys;
        foreach (StatusEffectType type in keys)
        {
            RemoveStatusEffect(type, GetStatusEffectStacks(type));
            statusEffectsUI.UpdateStatusEffectsUI(type, GetStatusEffectStacks(type));
        }
    }
    public int GetStatusEffectStacks(StatusEffectType type)
    {
        if (statusEffects.ContainsKey(type)) return statusEffects[type];
        else return 0;
    }
}

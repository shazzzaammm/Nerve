using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class CombatantView : MonoBehaviour
{
    [SerializeField] protected TMP_Text healthText;
    public int maxHealth { get; protected set; }
    public int currentHealth { get; protected set; }
    protected void SetupBase(int health){
        maxHealth = currentHealth = health;
        UpdateHealthText();
    }
    protected void UpdateHealthText()
    {
        healthText.text = currentHealth + "/" + maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0){
            currentHealth = 0;
        }

        UpdateHealthText();

        transform.DOShakePosition(.5f, .25f);
    }
}

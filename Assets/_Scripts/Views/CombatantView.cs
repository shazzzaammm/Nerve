using DG.Tweening;
using TMPro;
using UnityEngine;

public class CombatantView : MonoBehaviour
{
    [SerializeField] protected TMP_Text healthText, shieldText;
    public int maxHealth { get; protected set; }
    public int currentHealth { get; protected set; }
    public int currentShield { get; protected set; }
    protected void SetupBase(int health)
    {
        maxHealth = currentHealth = health;
        currentShield = 0;
        UpdateHealthText();
        UpdateShieldText();
    }

    protected void SetupBase(int health, int shield)
    {
        maxHealth = currentHealth = health;
        currentShield = shield;
        UpdateHealthText();
        UpdateShieldText();
    }

    protected void UpdateShieldText()
    {
        shieldText.text = currentShield.ToString();
    }
    protected void UpdateHealthText()
    {
        healthText.text = currentHealth + "/" + maxHealth;
    }

    public void TakeDamage(int amount)
    {
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

        UpdateHealthText();
        UpdateShieldText();

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
        UpdateShieldText();

        transform.DOMoveY(transform.position.y + .5f, .25f).onComplete += () =>
        {
            transform.DOMoveY(transform.position.y - .5f, .25f);
        };
    }

    public void AddShield(int amount)
    {
        currentShield += amount;
        UpdateShieldText();
    }
}

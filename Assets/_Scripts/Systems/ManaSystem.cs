using System;
using System.Collections;
using UnityEngine;

public class ManaSystem : Singleton<ManaSystem>
{
    public int maxMana { get; private set; } = 5;
    private int currentMana;
    [SerializeField] private ManaUI manaUI;

    void Start()
    {
        currentMana = maxMana;
        manaUI.UpdateManaText(currentMana);
    }
    private void OnEnable()
    {
        ActionSystem.AttachPerformer<RefillManaGA>(RefillManaPerformer);
        ActionSystem.AttachPerformer<SpendManaGA>(SpendManaPerformer);
    }

    private IEnumerator SpendManaPerformer(SpendManaGA spendManaGA)
    {
        currentMana -= spendManaGA.amount;
        manaUI.UpdateManaText(currentMana);
        yield return null;
    }

    private IEnumerator RefillManaPerformer(RefillManaGA gA)
    {
        currentMana = Math.Max(currentMana, maxMana);
        manaUI.UpdateManaText(currentMana);
        yield return null;
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<RefillManaGA>();
        ActionSystem.DetachPerformer<SpendManaGA>();

    }

    public bool PlayerHasEnoughMana(int cost)
    {
        return currentMana >= cost;
    }


}

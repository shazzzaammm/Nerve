using System.Collections.Generic;
using UnityEngine;

public class ChestGridCell : GridCellView
{
    private bool isOpened = false;
    private List<CardData> cardRewards;

    public override bool isWalkable()
    {
        return !isOpened;
    }

    public override void Setup(int x, int y)
    {
        base.Setup(x, y);
        cardRewards = new();
        for (int i = 0; i < 3; i++)
        {
            cardRewards.Add(DataSystem.instance.cards.GetRandom());
        }
    }
    public override void SetUnit(GridUnit unit)
    {
        base.SetUnit(unit);
        if (!isOpened && unit is HeroGridUnit hero)
        {
            ChestSystem.instance.Setup(cardRewards);
            isOpened = true;
        }
    }
}

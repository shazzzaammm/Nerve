
using System;
using UnityEngine;

public class HeroGridUnit : GridUnit
{
    public int cellsPerTurn;
    public HeroData data { get; private set; }

    public void Setup(HeroData data)
    {
        this.data = data;
        GetComponent<SpriteRenderer>().sprite = this.data.image;
    }
}

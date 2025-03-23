using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ItemState
{
    public int Id;
    public int Amount;

    public ItemState(int id, int amount)
    {
        Id = id;
        Amount = amount;
    }
}

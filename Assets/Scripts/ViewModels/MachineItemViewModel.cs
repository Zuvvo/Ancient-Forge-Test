using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MachineItemViewModel
{
    public Sprite Sprite { get; private set; }
    public MachineItemViewModel(ItemDataContainer itemDataContainer)
    {
        Sprite = itemDataContainer.ItemData.Sprite;
    }
}

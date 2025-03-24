using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public int Id;
    public ItemType Type;
    public string Name;
    public string Description;
    public Sprite Sprite;
    public List<PerksDataContainer> Perks;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Perk", menuName = "ScriptableObjects/Perk")]
public class PerksDataContainer : ScriptableObject
{
    public PerkType PerkType;
    public float Value;
}

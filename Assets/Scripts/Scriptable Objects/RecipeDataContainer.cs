using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeData", menuName = "ScriptableObjects/RecipeData")]
public class RecipeDataContainer : ScriptableObject
{
    public float Time;
    public float SuccessRate;
    public ItemDataContainer[] Ingredients;
    public ItemDataContainer Output;
}

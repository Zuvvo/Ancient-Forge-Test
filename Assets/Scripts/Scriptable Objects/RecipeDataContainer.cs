using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeData", menuName = "ScriptableObjects/Recipe data")]
public class RecipeDataContainer : ScriptableObject
{
    public float Time;
    public float SuccessRate;
    public ItemDataContainer[] Ingredients;
    public ItemDataContainer Output;
}

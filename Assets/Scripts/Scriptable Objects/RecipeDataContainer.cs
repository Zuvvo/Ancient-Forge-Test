using UnityEngine;

[CreateAssetMenu(fileName = "RecipeData", menuName = "ScriptableObjects/Recipe data")]
public class RecipeDataContainer : ScriptableObject
{
    public float Time;
    public float SuccessRate;
    public ItemDataContainer[] Ingredients;
    public ItemDataContainer Output;

    public ItemState[] IngredientsToItemsStateChange()
    {
        ItemState[] result = new ItemState[Ingredients.Length];
        for (int i = 0; i < Ingredients.Length; i++)
        {
            result[i] = new ItemState() { Amount = -1, Id = Ingredients[i].ItemData.Id };
        }

        return result;
    }

    public ItemState OutputToItemsStateChange()
    {
        return new ItemState() { Amount = 1, Id = Output.ItemData.Id };
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RecipeViewModel
{
    public float Time { get; private set; }
    public float SuccessRate { get; private set; }
    public MachineItemViewModel[] Ingredients { get; private set; }
    public MachineItemViewModel Output { get; private set; }

    public RecipeViewModel(RecipeDataContainer recipeDataContainer)
    {
        Time = recipeDataContainer.Time;
        SuccessRate = recipeDataContainer.SuccessRate;
        Ingredients = new MachineItemViewModel[recipeDataContainer.Ingredients.Length];
        for (int i = 0; i < recipeDataContainer.Ingredients.Length; i++)
        {
            Ingredients[i] = new MachineItemViewModel(recipeDataContainer.Ingredients[i]);
        }
        Output = new MachineItemViewModel(recipeDataContainer.Output);
    }

    public RecipeDataContainer RecipeDataContainer { get; }
}

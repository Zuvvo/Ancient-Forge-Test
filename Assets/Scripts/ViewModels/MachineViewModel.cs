using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MachineViewModel
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public RecipeViewModel[] Recipes { get; private set; }

    public MachineViewModel(MachineDataContainer dataContainer, int index)
    {
        Id = index;
        Name = dataContainer.Name;
        Recipes = new RecipeViewModel[dataContainer.RecipesDataContainer.Length];

        for (int i = 0; i < dataContainer.RecipesDataContainer.Length; i++)
        {
            Recipes[i] = new RecipeViewModel(dataContainer.RecipesDataContainer[i]);
        }
    }
}

[System.Serializable]
public class RecipeViewModel
{
    public int Id { get; private set; }
    public float Time { get; private set; }
    public float SuccessRate { get; private set; }
    public MachineItemViewModel[] Ingredients { get; private set; }
    public MachineItemViewModel Output { get; private set; }

    public RecipeViewModel(RecipeDataContainer recipeDataContainer, int index)
    {
        Id = index;
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

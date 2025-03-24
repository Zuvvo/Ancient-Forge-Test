using UnityEngine;

[CreateAssetMenu(fileName = "Machine", menuName = "ScriptableObjects/Machine")]
public class MachineDataContainer : ScriptableObject
{
    public string Name;
    public RecipeDataContainer[] RecipesDataContainer;
    public EQuest UnlockedByQuest;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Machine", menuName = "ScriptableObjects/Machine")]
public class MachineDataContainer : ScriptableObject
{
    public RecipeDataContainer[] RecipesDataContainer;
    public EQuest UnlockedByQuest;
}

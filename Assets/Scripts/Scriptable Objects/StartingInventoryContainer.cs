using UnityEngine;

[CreateAssetMenu(fileName = "Starting Items", menuName = "ScriptableObjects/Starting items")]
public class StartingInventoryContainer : ScriptableObject
{
    public StartItemData[] StartItemsData;
}
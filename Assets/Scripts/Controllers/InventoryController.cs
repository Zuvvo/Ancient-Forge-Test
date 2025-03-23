using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class InventoryController : MonoBehaviour
{
    public event Action<ItemState> OnItemStateChanged;

    [SerializeField] private string _itemsPath;
    [SerializeField] private string _startingItemsPath;

    private Dictionary<int, ItemState> _inventoryState = new();
    private UiInventoryScreen _uiScreen;
    private UiPerksScreen _uiPerksScreen;
    private ItemData[] _itemsData;

    private void Awake()
    {
        _itemsData = Resources.LoadAll<ItemDataContainer>(_itemsPath)
            .Select(c => c.ItemData)
            .OrderBy(item => item.Id)
            .ToArray();

        InitStartInventory();

        _uiScreen = _UiManager.Instance.GetScreenRefOfType(EScreenType.Inventory) as UiInventoryScreen;
        _uiScreen.Setup(this);

        _uiPerksScreen = _UiManager.Instance.GetScreenRefOfType(EScreenType.Perks) as UiPerksScreen;
        _uiPerksScreen.Setup(this);

        UpdateWholeInventory();
    }

    private void InitStartInventory()
    {
        var startInventoryContainer = Resources.Load<StartingInventoryContainer>(_startingItemsPath);
        foreach (StartItemData startItemData in startInventoryContainer.StartItemsData)
        {
            int amount = Random.Range(1, 100) <= startItemData.Chance * 100 ? Random.Range(startItemData.Min, startItemData.Max) : 0;
            int id = startItemData.ItemDataContainer.ItemData.Id;

            if (_inventoryState.ContainsKey(id) == false && amount > 0)
            {
                _inventoryState.Add(id, new ItemState() { Id = id, Amount = amount });
            }
        }
    }

    private void UpdateWholeInventory()
    {
        foreach (var kvp in _inventoryState)
        {
            CallOnItemStateChanged(kvp.Value);
        }
    }

    public void CallOnItemStateChanged(ItemState itemState)
    {
        OnItemStateChanged?.Invoke(itemState);
    }

    public ItemData GetItemData(int id)
    {
        return _itemsData.FirstOrDefault(item => item.Id == id);
    }

    public int GetResourceCount(int id)
    {
        if (_inventoryState.ContainsKey(id) == false)
            return 0;

        return _inventoryState[id].Amount;
    }

    public float GetCurrentPerkValue(PerkType perkType)
    {
        foreach (var kvp in _inventoryState)
        {
            if (kvp.Value.Amount > 0)
            {
                PerksDataContainer perkData = _itemsData.First(x => x.Id == kvp.Key)?.Perks.Find(x => x.PerkType == perkType);
                if(perkData != null)
                {
                    return perkData.Value;
                }
            }
        }
        return 0;
    }

    public void ChangeItemsState(params ItemState[] items)
    {
        for (int i = 0; i < items.Length; i++)
        {
            ItemState change = items[i];
            if (_inventoryState.ContainsKey(change.Id) == false)
            {
                _inventoryState.Add(change.Id, new ItemState(change.Id, 0));
            }
            ItemState currentItemState = _inventoryState[change.Id];
            ItemState newItemState =  new ItemState(currentItemState.Id, currentItemState.Amount + change.Amount);
            _inventoryState[currentItemState.Id] = newItemState;
            CallOnItemStateChanged(newItemState);
        }
    }
}
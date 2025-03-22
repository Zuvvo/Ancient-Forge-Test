using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiInventoryScreen : UiScreen
{
    [SerializeField] private UiItem _uiItemPrefab;
    [SerializeField] private RectTransform _itemsHolder;

    private Dictionary<int, UiItem> _uiItems = new();
    private InventoryController _inventoryController;

    public void Setup(InventoryController inventoryController)
    {
        _inventoryController = inventoryController;
        _inventoryController.OnItemStateChanged += OnItemStateChanged;
    }

    private void OnItemStateChanged(ItemState state)
    {
        if(_uiItems.TryGetValue(state.Id, out UiItem uiItem))
        {
            uiItem.UpdateState(state);
        }
        else
        {
            uiItem = Instantiate(_uiItemPrefab, _itemsHolder);
            _uiItems.Add(state.Id, uiItem);

            uiItem.Setup(_inventoryController.GetItemData(state.Id));
            uiItem.UpdateState(state);
        }
    }
}

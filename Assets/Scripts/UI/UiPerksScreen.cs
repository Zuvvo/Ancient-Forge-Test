using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiPerksScreen : UiScreen
{
    [SerializeField] private TextMeshProUGUI _successRatePerkText;
    [SerializeField] private TextMeshProUGUI _timeReductionPerkText;

    private InventoryController _inventoryController;


    public void Setup(InventoryController inventoryController)
    {
        _inventoryController = inventoryController;
        _inventoryController.OnItemStateChanged += OnItemStateChanged;
    }

    private void OnItemStateChanged(ItemState state)
    {
        UpdatePerksText();
    }

    private void UpdatePerksText()
    {
        float successRateBonus = _inventoryController.GetCurrentPerkValue(PerkType.SuccessRateBonus);
        float timeReductionBonus = _inventoryController.GetCurrentPerkValue(PerkType.TimeReduce);

        gameObject.SetActive(successRateBonus > 0 || timeReductionBonus > 0);

        _successRatePerkText.text = successRateBonus > 0 ? $"Bonus success rate: {successRateBonus * 100}%" : string.Empty;
        _timeReductionPerkText.text = timeReductionBonus > 0 ? $"Crafting time reduciton: {timeReductionBonus}s" : string.Empty;
    }
}

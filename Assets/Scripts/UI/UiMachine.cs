using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiMachine : MonoBehaviour
{
    [SerializeField] private Image _progressImage;
    [SerializeField] private UiRecipe _uiRecipePrefab;
    [SerializeField] private RectTransform _recipesHolder;
    [SerializeField] private TextMeshProUGUI _machineName;

    private MachineViewModel _machineViewModel;
    private List<UiRecipe> _uiRecipes = new();
    private MachinesController _machinesController;
    private InventoryController _inventoryController;

    public void Setup(MachineViewModel machineViewModel, MachinesController machinesController, InventoryController inventoryController)
    {
        _machinesController = machinesController;
        _inventoryController = inventoryController;

        for (int i = 0; i < machineViewModel.Recipes.Length; i++)
        {
            UiRecipe uiRecipe = Instantiate(_uiRecipePrefab, _recipesHolder);
            uiRecipe.Setup(this, machineViewModel.Recipes[i]);
            _uiRecipes.Add(uiRecipe);
        }

        _machineViewModel = machineViewModel;
        _machineViewModel.StateDataBinded.DataChanged += OnDataChanged;
        _machineViewModel.StateDataBinded.ProgressChanged += OnProgressChanged;
        _inventoryController.OnItemStateChanged += OnItemStateChanged;

        _machineName.text = machineViewModel.Name;
    }

    private void OnItemStateChanged(ItemState state)
    {
        UpdateStartButtonsInteractableState();
    }

    public void OnRecipeStartClicked(int recipeIndex)
    {
        _machinesController.StartMachine(_machineViewModel.Id, recipeIndex);
    }

    private void OnProgressChanged()
    {
        _progressImage.fillAmount = _machineViewModel.StateDataBinded.Progress;
    }

    private void OnDataChanged()
    {
        gameObject.SetActive(_machineViewModel.StateDataBinded.IsUnlocked);
        UpdateStartButtonsInteractableState();
    }

    private void UpdateStartButtonsInteractableState()
    {
        for (int i = 0; i < _uiRecipes.Count; i++)
        {
            bool isRunning = _machineViewModel.StateDataBinded.IsRunning;
            bool hasEnoughResources = _machinesController.HasResourcesForRecipe(_machineViewModel.Id, _machineViewModel.Recipes[i].Id);
            _uiRecipes[i].SetButtonInteractableState(isRunning == false && hasEnoughResources);
        }
    }

    private void OnDestroy()
    {
        _machineViewModel.StateDataBinded.DataChanged -= OnDataChanged;
        _machineViewModel.StateDataBinded.ProgressChanged -= OnProgressChanged;
        _inventoryController.OnItemStateChanged -= OnItemStateChanged;
    }
}

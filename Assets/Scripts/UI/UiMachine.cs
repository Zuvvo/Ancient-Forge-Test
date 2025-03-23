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

    public void Setup(MachineViewModel machineViewModel, MachinesController machinesController)
    {
        for (int i = 0; i < machineViewModel.Recipes.Length; i++)
        {
            UiRecipe uiRecipe = Instantiate(_uiRecipePrefab, _recipesHolder);
            uiRecipe.Setup(this, machineViewModel.Recipes[i]);
            _uiRecipes.Add(uiRecipe);
        }

        _machineViewModel = machineViewModel;
        _machineViewModel.StateDataBinded.DataChanged += OnDataChanged;
        _machineViewModel.StateDataBinded.ProgressChanged += OnProgressChanged;

        _machineName.text = machineViewModel.Name;

        _machinesController = machinesController;
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
        for (int i = 0; i < _uiRecipes.Count; i++)
        {
            _uiRecipes[i].SetButtonInteractableState(_machineViewModel.StateDataBinded.IsRunning == false);
        }
    }

    private void OnDestroy()
    {
        _machineViewModel.StateDataBinded.DataChanged -= OnDataChanged;
        _machineViewModel.StateDataBinded.ProgressChanged -= OnProgressChanged;
    }
}

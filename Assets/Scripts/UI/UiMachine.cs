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

    private List<UiRecipe> _uiRecipes = new();

    public void Setup(MachineViewModel machineViewModel)
    {
        for (int i = 0; i < machineViewModel.Recipes.Length; i++)
        {
            UiRecipe uiRecipe = Instantiate(_uiRecipePrefab, _recipesHolder);
            uiRecipe.Setup(machineViewModel.Recipes[i]);
            _uiRecipes.Add(uiRecipe);
        }

        _machineName.text = machineViewModel.Name;
    }
}

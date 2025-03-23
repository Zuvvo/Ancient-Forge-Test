using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachinesController : MonoBehaviour
{
    [SerializeField] private string _machinesPath;
    [SerializeField] private InventoryController _inventoryController;

    private UiMachinesScreen _uiScreen;
    private MachineDataContainer[] _machineDataContainers;
    private Dictionary<int, MachineState> _machineStates = new();

    private void Awake()
    {
        _machineDataContainers = Resources.LoadAll<MachineDataContainer>(_machinesPath);

        _uiScreen = _UiManager.Instance.GetScreenRefOfType(EScreenType.Machines) as UiMachinesScreen;
        _uiScreen.Setup(this, GetViewModels(_machineDataContainers));

        UpdateMachineStates();

    }

    private void UpdateMachineStates()
    {
        foreach (var kvp in _machineStates)
        {
            kvp.Value.UpdateState();
        }
    }

    private MachineViewModel[] GetViewModels(MachineDataContainer[] machineDataContainers)
    {
        MachineViewModel[] viewModels = new MachineViewModel[machineDataContainers.Length];
        for (int i = 0; i < machineDataContainers.Length; i++)
        {
            MachineState machineState = new MachineState(machineDataContainers[i], i);
            _machineStates.Add(i, machineState);

            viewModels[i] = new MachineViewModel(machineDataContainers[i], machineState);
        }
        return viewModels;
    }

    public void StartMachine(int machineId, int recipeId)
    {
        MachineState machine = _machineStates[machineId];
        if(machine.IsUnlocked && machine.IsRunning == false)
        {
            StartCoroutine(RunMachine(machine, recipeId));
        }
    }

    private IEnumerator RunMachine(MachineState machineState, int recipeId)
    {
        machineState.StartMachine();
        RecipeDataContainer recipeData = GetRecipeDataContainer(machineState.Id, recipeId);
        _inventoryController.ChangeItemsState(recipeData.IngredientsToItemsStateChange());
        float timer = 0;
        while(timer < recipeData.Time)
        {
            timer += Time.deltaTime;
            yield return null;
            machineState.UpdateProgress(timer /  recipeData.Time);
        }

        _inventoryController.ChangeItemsState(recipeData.OutputToItemsStateChange());

        machineState.FinishWork();
    }

    private RecipeDataContainer GetRecipeDataContainer(int machineId, int recipeId)
    {
        return _machineDataContainers[machineId].RecipesDataContainer[recipeId];
    }
}

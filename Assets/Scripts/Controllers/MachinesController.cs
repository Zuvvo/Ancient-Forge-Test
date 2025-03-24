using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class MachinesController : MonoBehaviour
{
    [SerializeField] private string _machinesPath;
    [SerializeField] private InventoryController _inventoryController;
    [SerializeField] private QuestsController _questController;

    private UiMachinesScreen _uiScreen;
    private MachineDataContainer[] _machineDataContainers;
    private Dictionary<int, MachineState> _machineStates = new();

    private void Awake()
    {
        _machineDataContainers = Resources.LoadAll<MachineDataContainer>(_machinesPath);

        _uiScreen = _UiManager.Instance.GetScreenRefOfType(EScreenType.Machines) as UiMachinesScreen;
        _uiScreen.Setup(this, _inventoryController, GetViewModels(_machineDataContainers));

        _questController.OnQuestStateChanged += OnQuestStateChanged;

        UpdateMachineStates();

    }

    private void OnDestroy()
    {
        _questController.OnQuestStateChanged -= OnQuestStateChanged;
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

    public bool HasResourcesForRecipe(int machineId, int recipeId)
    {
        var data = GetRecipeDataContainer(machineId, recipeId);
        for (int i = 0; i < data.Ingredients.Length; i++)
        {
            if (_inventoryController.GetResourceCount(data.Ingredients[i].ItemData.Id) <= 0)
            {
                return false;
            }
        }

        return true;

    }

    private IEnumerator RunMachine(MachineState machineState, int recipeId)
    {
        machineState.StartMachine();
        RecipeDataContainer recipeData = GetRecipeDataContainer(machineState.Id, recipeId);
        _inventoryController.ChangeItemsState(recipeData.IngredientsToItemsStateChange());
        float timer = 0;
        float time = recipeData.Time - _inventoryController.GetCurrentPerkValue(PerkType.TimeReduce);
        while(timer < time)
        {
            timer += Time.deltaTime;
            yield return null;
            machineState.UpdateProgress(timer / time);
        }

        float successRate = recipeData.SuccessRate + _inventoryController.GetCurrentPerkValue(PerkType.SuccessRateBonus);

        bool success = Random.Range(1, 100) <= successRate * 100;
        if (success)
        {
            _inventoryController.ChangeItemsState(recipeData.OutputToItemsStateChange());
        }

        machineState.FinishWork();
    }

    private void OnQuestStateChanged(QuestState state)
    {
        for (int i = 0; i < _machineDataContainers.Length; i++)
        {
            if(_machineDataContainers[i].UnlockedByQuest == state.Quest && _questController.IsQuestCompleted(state.Quest) && _machineStates[i].IsUnlocked == false)
            {
                _machineStates[i].Unlock();
            }
        }
    }

    private RecipeDataContainer GetRecipeDataContainer(int machineId, int recipeId)
    {
        return _machineDataContainers[machineId].RecipesDataContainer[recipeId];
    }
}

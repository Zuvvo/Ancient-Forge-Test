using System.Collections.Generic;
using UnityEngine;

public class UiMachinesScreen : UiScreen
{
    [SerializeField] private UiMachine _uiMachinePrefab;
    [SerializeField] private RectTransform _machinesHolder;

    private MachinesController _machinesController;
    private InventoryController _inventoryController;
    private List<UiMachine> _uiMachines = new();

    public void Setup(MachinesController machinesController, InventoryController inventoryController, MachineViewModel[] machineViewModels)
    {
        _machinesController = machinesController;
        _inventoryController = inventoryController;

        for (int i = 0; i < machineViewModels.Length; i++)
        {
            UiMachine uiMachine = Instantiate(_uiMachinePrefab, _machinesHolder);
            uiMachine.Setup(machineViewModels[i], _machinesController, _inventoryController);
            _uiMachines.Add(uiMachine);
        }
    }
}

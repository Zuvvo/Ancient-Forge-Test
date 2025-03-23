using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachinesController : MonoBehaviour
{
    [SerializeField] private string _machinesPath;

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
}

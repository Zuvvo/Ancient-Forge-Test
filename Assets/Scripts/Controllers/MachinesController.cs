using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachinesController : MonoBehaviour
{
    [SerializeField] private string _machinesPath;

    private UiMachinesScreen _uiScreen;
    private MachineDataContainer[] _machineDataContainers;

    private void Awake()
    {
        _machineDataContainers = Resources.LoadAll<MachineDataContainer>(_machinesPath);

        _uiScreen = _UiManager.Instance.GetScreenRefOfType(EScreenType.Machines) as UiMachinesScreen;
        _uiScreen.Setup(this, GetViewModels(_machineDataContainers));

    }

    private MachineViewModel[] GetViewModels(MachineDataContainer[] machineDataContainers)
    {
        MachineViewModel[] viewModels = new MachineViewModel[machineDataContainers.Length];
        for (int i = 0; i < machineDataContainers.Length; i++)
        {
            viewModels[i] = new MachineViewModel(machineDataContainers[i], i);
        }
        return viewModels;
    }
}

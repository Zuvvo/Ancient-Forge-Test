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
        return Array.ConvertAll(machineDataContainers, data => new MachineViewModel(data));
    }
}

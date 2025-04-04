using System;

public class MachineState
{
    public int Id { get; private set; }
    public bool IsRunning { get; private set; }
    public float Progress { get; private set; }
    public int RecipeIdRunning { get; private set; } = -1;
    public bool IsUnlocked { get; private set; }

    public event Action DataChanged;
    public event Action ProgressChanged;

    public MachineState(MachineDataContainer machineDataContainer, int id)
    {
        Id = id;
        IsUnlocked = machineDataContainer.UnlockedByQuest == EQuest.None;
    }

    public void StartMachine()
    {
        IsRunning = true;
        DataChanged?.Invoke();
    }

    public void FinishWork()
    {
        Progress = 0;
        RecipeIdRunning = -1;
        IsRunning = false;
        UpdateState();
    }

    public void UpdateProgress(float progress)
    {
        Progress = progress;
        ProgressChanged?.Invoke();
    }

    public void UpdateState()
    {
        DataChanged?.Invoke();
        ProgressChanged?.Invoke();
    }

    public void Unlock()
    {
        IsUnlocked = true;
        DataChanged?.Invoke();
    }
}

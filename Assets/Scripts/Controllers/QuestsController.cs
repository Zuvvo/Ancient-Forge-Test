using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestsController : MonoBehaviour
{
    public event Action<QuestState> OnQuestStateChanged;

    [SerializeField] private string _questsPath;
    [SerializeField] private InventoryController _inventoryController;

    private List<QuestData> _data = new();
    private Dictionary<EQuest, QuestState> _questsState = new();
    private UiQuestsScreen _uiScreen;

    private void Awake()
    {
        var containers = Resources.LoadAll<QuestDataContainer>(_questsPath);

        for (int i = 0; i < containers.Length; i++)
        {
            _data.Add(containers[i].QuestData);
            _questsState.Add(containers[i].QuestData.EQuest, new QuestState() { Progress = 0, Quest = containers[i].QuestData.EQuest });
        }

        _uiScreen = _UiManager.Instance.GetScreenRefOfType(EScreenType.Quests) as UiQuestsScreen;
        _uiScreen.Setup(this, _data);

        _inventoryController.OnItemStateChanged += OnItemStateChanged;

    }

    private void OnItemStateChanged(ItemState itemState)
    {
        foreach (var kvp in _questsState)
        {
            float progress = 0;
            QuestRequirement[] requirements = _data.Find(x => x.EQuest == kvp.Key).Requirements;
            for (int i = 0; i < requirements.Length; i++)
            {
                float val = _inventoryController.GetResourceCount(requirements[i].DataContainer.ItemData.Id) / (float)requirements[i].Amount;
                progress += Mathf.Clamp01(val);
            }
            progress /= requirements.Length;
            kvp.Value.Progress = progress;

            OnQuestStateChanged?.Invoke(kvp.Value);
        }
    }

    public bool IsQuestCompleted(EQuest quest)
    {
        return _questsState[quest].Progress >= 1;
    }
}

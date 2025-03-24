using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiQuestsScreen : UiScreen
{
    [SerializeField] private UiQuest _uiQuestPrefab;
    [SerializeField] private RectTransform _questsHolder;

    private List<UiQuest> _quests = new();
    private QuestsController _questController;

    public void Setup(QuestsController questsController, List<QuestData> quests)
    {

        _questController = questsController;

        for (int i = 0; i < quests.Count; i++)
        {
            UiQuest uiQuest = Instantiate(_uiQuestPrefab, _questsHolder);
            uiQuest.Setup(quests[i]);
            _quests.Add(uiQuest);
        }

        _questController.OnQuestStateChanged += OnQuestStateChanged;
    }

    private void OnQuestStateChanged(QuestState state)
    {
        var uiQuest = _quests.Find(x => x.Quest == state.Quest);
        uiQuest?.UpdateState(state);
    }

    private void OnDestroy()
    {
        _questController.OnQuestStateChanged -= OnQuestStateChanged;
    }
}

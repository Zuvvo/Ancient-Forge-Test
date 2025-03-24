using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiQuest : MonoBehaviour
{
    public EQuest Quest { get; private set; }

    [SerializeField] private Image _progressImage;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;

    public void Setup(QuestData data)
    {
        Quest = data.EQuest;
        _nameText.text = data.Name;
        _descriptionText.text = data.Description;
    }

    public void UpdateState(QuestState state)
    {
        _progressImage.fillAmount = state.Progress;
    }
}

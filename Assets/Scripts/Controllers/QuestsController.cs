using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestsController : MonoBehaviour
{
    [SerializeField] private string _questsPath;

    private List<QuestData> _data;

    private void Awake()
    {
        var containers = Resources.LoadAll<QuestDataContainer>(_questsPath);

        for (int i = 0; i < containers.Length; i++)
        {
            _data.Add(containers[i].QuestData);
        }

    }

}

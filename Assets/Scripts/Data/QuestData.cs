using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestData
{
    public string Name;
    public string Description;
    public EQuest EQuest;
    public QuestRequirement[] Requirements;
}

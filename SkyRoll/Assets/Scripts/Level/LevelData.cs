using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Level Data")]
public class LevelData : ScriptableObject
{
    //level details
    public string levelName;
    public int levelNo;
    public int levelID;
    public int levelLenth;
    public bool isCompleted;
    public bool hasAchievement;
    public int gemCount;
}

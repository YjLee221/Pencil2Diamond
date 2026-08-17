using UnityEngine;

public enum WorkShopLevelType
{
    Tutorial,
    Starter,
    Starting,
    Advanced,
    Professional,
    Master,
    MaxLevel
}

[System.Serializable]
public struct WorkShopLevelInfo
{
    public WorkShopLevelType levelType;
    public string displayName;
}

[CreateAssetMenu(fileName = "WorkShopData", menuName = "ScriptableObjects/WorkShopData")]
public class WorkShopData : ScriptableObject
{
    [Header("공방 정보")]
    [SerializeField] WorkShopLevelInfo[] workshopLevels;

    public string GetWorkShopLevelName(WorkShopLevelType levelType)
    {
        foreach (var levelInfo in workshopLevels)
        {
            if (levelInfo.levelType == levelType)
            {
                return levelInfo.displayName;
            }
        }
        return "Unknown Level";
    }
}

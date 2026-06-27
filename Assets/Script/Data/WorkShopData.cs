using UnityEngine;

public enum WorkShopLevelType
{
    TUTORIAL_WorkShop,
    STARTER_WorkShop,
    STARTING_WorkShop,
    ADVANCED_WorkShop,
    PROFESSIONAL_WorkShop,
    MASTER_WorkShop,
    MAX_WorkShop_LEVEL
}

public struct WorkShopLevelInfo
{
    public WorkShopLevelType levelType;
    public string displayName;
}

[CreateAssetMenu(fileName = "WorkShopData", menuName = "Scriptable Objects/WorkShop Data")]
public class WorkShopData : ScriptableObject
{

}

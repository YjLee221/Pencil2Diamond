using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    [Header("플레이어 정보")]
    public int currentWorkshopLevel = 0;

    [Header("재화 정보")]
    public int unSharpenedPencilCount = 0;
    public int sharpeningPencilCount = 0;
    public int sharpenedPencilCount = 0;

    public int graphiteCount = 0;
    public int diamondCount = 0;
    public int coinCount = 0;
}

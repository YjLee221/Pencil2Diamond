using UnityEngine;

[System.Serializable]
public struct GraphiteState
{
    [Range(0.0f, 1.0f)] public float remainingHpPercent; // HP가 이 퍼센트 이하일 때 이 상태로 전환
    public Sprite graphiteSprite; // 특정 상태에서 보여줄 이미지
}

public enum GraphiteType
{
    INTRO_GRAPHITE,        // 게임 시작 시 연습용 흑연
    TWOB_GRAPHITE,
    FOURB_GRAPHITE,
    GOLDEN_GRAPHITE,
    DIAMOND_GRAPHITE,     // 과금용 흑연
    MAX_GRAPHITE_TYPE     // 흑연 종류의 최대값 (새로운 흑연 추가 시 이 값도 업데이트 필요)
}

[CreateAssetMenu(fileName = "NewGraphiteData", menuName = "ScriptableObjects/GraphiteData")]
public class GraphiteData : ScriptableObject
{
    [Header("흑연 정보")]
    [SerializeField] string graphiteName; // 흑연 이름
    [SerializeField] GraphiteType graphiteType; // 흑연 종류
    [SerializeField] int maxGraphiteHp = 15; // 흑연의 최대 HP

    [Header("흑연 상태별 이미지")]
    [SerializeField] GraphiteState[] imgGraphiteStates; // 흑연의 HP 단계별로 보여줄 이미지 설정

    public string GraphiteName => graphiteName;
    public GraphiteType GraphiteType { get { return graphiteType; } }
    public int MaxGraphiteHp => maxGraphiteHp;
    public GraphiteState[] ImgGraphiteStates { get { return imgGraphiteStates; } }
}
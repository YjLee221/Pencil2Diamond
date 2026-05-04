using UnityEngine;

// 연필의 각 단계별 HP와 해당 단계에서 보여줄 스프라이트를 저장하는 클래스
[System.Serializable]
public struct PencilState
{
    [Range(0.0f, 1.0f)] public float remainingHpPercent; // HP가 이 퍼센트 이하일 때 이 상태로 전환
    public Sprite pencilSprite; // 특정 상태에서 보여줄 이미지
}

public enum PencilType
{
    INTRO_PENCIL,        // 게임 시작 시 연습용 연필
    TWOB_PENCIL,    
    FOURB_PENCIL,
    GOLDEN_PENCIL,       
    DIAMOND_PENCIL,     // 과금용 연필
    MAX_PENCIL_TYPE     // 연필 종류의 최대값 (새로운 연필 추가 시 이 값도 업데이트 필요)
}

// 유니티 프로젝트에서 연필의 상태를 설정할 수 있도록 팝업메뉴 만들기
[CreateAssetMenu(fileName = "PencilData", menuName = "ScriptableObjects/PencilData")]
public class PencilData : ScriptableObject
{
    [Header("연필 정보")]
    [SerializeField] string pencilName; // 연필 이름
    [SerializeField] PencilType pencilType; // 연필 종류
    [SerializeField] int maxPencilHp = 100; // 연필의 최대 HP

    [Header("연필 상태별 이미지")]
    [SerializeField] PencilState[] imgPencilStates; // 연필의 HP 단계별로 보여줄 이미지 설정

    // 외부에서 읽을 수 있도록 프로퍼티로 공개
    public string PencilName => pencilName;
    public PencilType PencilType { get { return pencilType; } }
    public int MaxPencilHp => maxPencilHp;
    public PencilState[] PencilStates { get { return imgPencilStates; } }
}


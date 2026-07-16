using UnityEngine;
public struct DiamondState
{
    //[Range(0.0f, 1.0f)] public float remainingHpPercent; // HP가 이 퍼센트 이하일 때 이 상태로 전환
    public Sprite diamondSprite; // 단계별 다이아몬드 이미지
}

public enum DiamondType
{
    IntroDiamond,
    TwoBDiamond,
    FourBDiamond,
    GoldenDiamond,
    DiamondDiamond,
    MaxDiamondType
}

[CreateAssetMenu(fileName = "NewDiamondData", menuName = "ScriptableObjects/DiamondData")]
public class DiamondData : ScriptableObject
{
    [Header("다이아몬드 정보")]
    [SerializeField] string diamondName; // 다이아몬드 이름
    [SerializeField] DiamondType diamondType; // 다이아몬드 종류
    [SerializeField] int sellPriceForDiamond; // 다이아몬드 가격
    [SerializeField] Sprite diamondSprite; // 다이아몬드별 이미지

    //[SerializeField] DiamondData diamondData;

    public string DiamondName => diamondName;
    public DiamondType DiamondType => diamondType;
    public int SellPriceForDiamond => sellPriceForDiamond;
    public Sprite DiamondSprite => diamondSprite;
}

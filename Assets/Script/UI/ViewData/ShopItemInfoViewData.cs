using UnityEngine;

public class ShopItemInfoViewData
{
    public PencilType PencilType { get; }
    public string PencilName { get; }
    public int PencilPrice{ get; }
    public string PencilShortInfo { get; }
    public string PencilDetailInfo { get; }

    public ShopItemInfoViewData(PencilType pencilType, string pencilName, int pencilPrice, string shortInfo, string detailInfo)
    {
        PencilType = pencilType;
        PencilPrice = pencilPrice;
        PencilName = pencilName;
        PencilShortInfo = shortInfo;
        PencilDetailInfo = detailInfo;
    }
}
using UnityEngine;

public class ShopItemInfoViewData
{
    public string PencilName { get; }
    public string PencilShortInfo { get; }
    public string PencilDetailInfo { get; }

    public ShopItemInfoViewData(string pencilName, string shortInfo, string detailInfo)
    {
        PencilName = pencilName;
        PencilShortInfo = shortInfo;
        PencilDetailInfo = detailInfo;
    }
}
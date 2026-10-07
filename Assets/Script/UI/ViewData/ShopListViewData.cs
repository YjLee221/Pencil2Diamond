using UnityEngine;
using UnityEngine.UI;

public class ShopListViewData
{
    public PencilType PencilType { get; }
    public string PencilName { get; }
    public int UserPencilCount { get; }
    public int PencilPrice { get; }
    public Sprite PencilSprite { get; }
    public string PencilShortInfo { get; }
    public string PencilDetailInfo { get; }

    public ShopListViewData(PencilType pencilType, string pencilName, int userPencilCount, int pencilPrice, Sprite pencilSprite, string shortInfo, string detailInfo)
    {
        PencilType = pencilType;
        PencilName = pencilName;
        UserPencilCount = userPencilCount;
        PencilPrice = pencilPrice;
        PencilSprite = pencilSprite;
        PencilShortInfo = shortInfo;
        PencilDetailInfo = detailInfo;
    }
}
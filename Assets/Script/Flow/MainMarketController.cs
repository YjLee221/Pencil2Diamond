using System;
using System.Collections.Generic;
using UnityEngine;

public class MainMarketController : MonoBehaviour
{
    [SerializeField] MainMarketUI mainMarketUI;
    [SerializeField] ItemDetailInfoUI itemDetailInfoUI;
    [SerializeField] PencilManager pencilManager;

    readonly List<ShopListViewData> pencilItems = new List<ShopListViewData>();

    void Start()
    {
        for (var i = (int)PencilType.TwoBPencil; i < (int)PencilType.MaxPencilType; i++)
        {
            var pencilData = pencilManager.GetPencilData((PencilType)i);
            
            if (pencilData == null || pencilData.PencilStates == null || pencilData.PencilStates.Length == 0
                || pencilData.PencilShortInfo == null || pencilData.PencilDetailInfo == null) continue;

            var shopViewData = new ShopListViewData(pencilData.PencilType, pencilData.PencilName
                , pencilData.SellPriceForPencil, pencilData.PencilStates[0].pencilSprite
                , pencilData.PencilShortInfo, pencilData.PencilDetailInfo);
            
            pencilItems.Add(shopViewData);
        }
        
        mainMarketUI.SettingItemButtons(pencilItems.ToArray());
    }

    void OnEnable()
    {
        mainMarketUI.OnItemButtonClickedEvent += HandleItemButtonClickedEvent;
        itemDetailInfoUI.OnPurchaseButtonClickedEvent += HandlePurchaseButtonClickedEvent;
    }

    void HandleItemButtonClickedEvent(PencilType pencilType)
    {
        var selectedItem = pencilItems.Find(item=>item.PencilType == pencilType);
        if (selectedItem == null) return;
        
        var itemInfo = new ShopItemInfoViewData(selectedItem.PencilName, selectedItem.PencilShortInfo, selectedItem.PencilDetailInfo);
        
        itemDetailInfoUI.ShowDetailInfo(itemInfo);
    }

    void HandlePurchaseButtonClickedEvent(int amount)
    {
        // 연필의 종류, 가격, 개수를 비교해 돈 계산
        
        // 플레이어가 가지고 있는 돈과 비교
        // 플레이어 소유 돈이 더 많거나 같으면 구매하기 버튼 활성화
        // 그 외는 비활성화
    }

    void OnDisable()
    {
        mainMarketUI.OnItemButtonClickedEvent -= HandleItemButtonClickedEvent;
        itemDetailInfoUI.OnPurchaseButtonClickedEvent -= HandlePurchaseButtonClickedEvent;
    }
}

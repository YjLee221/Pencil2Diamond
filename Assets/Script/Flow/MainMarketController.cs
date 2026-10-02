using System;
using System.Collections.Generic;
using UnityEngine;

public class MainMarketController : MonoBehaviour
{
    [SerializeField] MainMarketUI mainMarketUI;
    [SerializeField] ItemDetailInfoUI itemDetailInfoUI;
    
    [SerializeField] PencilManager pencilManager;
    [SerializeField] PlayerInventoryManager playerInventoryManager;

    [SerializeField] QuantitySettingUI quantitySettingUI;
    
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
        
        var itemInfo = new ShopItemInfoViewData(selectedItem.PencilType, selectedItem.PencilName, selectedItem.PencilPrice , selectedItem.PencilShortInfo, selectedItem.PencilDetailInfo);
        
        itemDetailInfoUI.ShowDetailInfo(itemInfo);
    }

    public bool CheckCanPurchase(int buyingCount, ShopItemInfoViewData itemInfoData)
    {
        if(buyingCount <= 0 || itemInfoData == null) return false;

        int totalPrice = buyingCount * itemInfoData.PencilPrice;
        return playerInventoryManager.PlayerCoinCount >= totalPrice;
    }
    
    void HandlePurchaseButtonClickedEvent(ShopItemInfoViewData itemInfo, int amount)
    {
        // 구매버튼 눌렀을 때
        playerInventoryManager.CalForBuyingPencil(itemInfo.PencilType, amount);
    }

    void OnDisable()
    {
        mainMarketUI.OnItemButtonClickedEvent -= HandleItemButtonClickedEvent;
        itemDetailInfoUI.OnPurchaseButtonClickedEvent -= HandlePurchaseButtonClickedEvent;
    }
}

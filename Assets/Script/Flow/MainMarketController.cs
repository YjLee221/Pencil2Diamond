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
            var findPencilData = playerInventoryManager.FindPencilData((PencilType)i);

            var pencilInfoData = findPencilData.pencilData;

            if (pencilInfoData == null || pencilInfoData.PencilStates == null || pencilInfoData.PencilStates.Length == 0 
                || pencilInfoData.PencilShortInfo == null || pencilInfoData.PencilDetailInfo == null) continue;

            var shopViewData = new ShopListViewData(pencilInfoData.PencilType, pencilInfoData.PencilName
                , findPencilData.unSharpenedCount, pencilInfoData.SellPriceForPencil
                , pencilInfoData.PencilStates[0].pencilSprite, pencilInfoData.PencilShortInfo, pencilInfoData.PencilDetailInfo
                );
            
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

        Debug.Log($"totalPrice:{totalPrice} / playerCoin: {playerInventoryManager.PlayerCoinCount}");
        
        return playerInventoryManager.PlayerCoinCount >= totalPrice;
    }
    
    void HandlePurchaseButtonClickedEvent(ShopItemInfoViewData itemInfo, int amount)
    {
        // 구매버튼 눌렀을 때
        playerInventoryManager.TryBuyingPencil(itemInfo.PencilType, amount);
        mainMarketUI.UpdatePlayerPencilCount(itemInfo.PencilType, amount);
    }

    void OnDisable()
    {
        mainMarketUI.OnItemButtonClickedEvent -= HandleItemButtonClickedEvent;
        itemDetailInfoUI.OnPurchaseButtonClickedEvent -= HandlePurchaseButtonClickedEvent;
    }
}

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailInfoUI : MonoBehaviour
{
    [SerializeField] GameObject itemDetailInfoPanel;
    [SerializeField] Button purchaseButton;

    [SerializeField] TextMeshProUGUI txtPencilName;
    [SerializeField] TextMeshProUGUI txtPencilShortInfo;
    [SerializeField] TextMeshProUGUI txtPencilDetailInfo;
    
    [SerializeField] QuantitySettingUI quantitySettingUI;
    [SerializeField] MainMarketController mainMarketController;
    
    ShopItemInfoViewData itemInfoData;
    
    // 이벤트
    public event Action<ShopItemInfoViewData, int> OnPurchaseButtonClickedEvent;

    void Awake()
    {
        purchaseButton.onClick.AddListener(OnPurchaseButtonClicked);
    }

    void Start()
    {
        int maxAmount = quantitySettingUI.maximumQuantity;
    }

    void OnEnable()
    {
        quantitySettingUI.OnQuantityChangedEvent += HandledQuantityChangedEvent;
    }

    public void ShowDetailInfo(ShopItemInfoViewData itemInfoData)
    {
        this.itemInfoData = itemInfoData;
        quantitySettingUI.ResetQuantity();
        itemDetailInfoPanel.SetActive(true);
        
        purchaseButton.interactable = mainMarketController.CheckCanPurchase(quantitySettingUI.CurrentQuantity, itemInfoData);
        txtPencilName.text = itemInfoData.PencilName;
        txtPencilShortInfo.text = itemInfoData.PencilShortInfo;
        txtPencilDetailInfo.text = itemInfoData.PencilDetailInfo;
    }

    void HandledQuantityChangedEvent(int buyingCount)
    {
        if (itemInfoData == null) return;
        
        purchaseButton.interactable = mainMarketController.CheckCanPurchase(buyingCount, itemInfoData);
    }

    void OnPurchaseButtonClicked()
    {
        if (!purchaseButton.interactable) return;
        purchaseButton.interactable = mainMarketController.CheckCanPurchase(quantitySettingUI.CurrentQuantity, itemInfoData);
        OnPurchaseButtonClickedEvent?.Invoke(itemInfoData, quantitySettingUI.CurrentQuantity);
        
    }

    void OnDisable()
    {
        quantitySettingUI.OnQuantityChangedEvent -= HandledQuantityChangedEvent;
    }
}

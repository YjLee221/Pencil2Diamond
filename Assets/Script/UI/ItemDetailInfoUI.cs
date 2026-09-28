using System;
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

    [SerializeField] ShopItemInfoViewData itemInfoData;

    public event Action<int> OnPurchaseButtonClickedEvent;

    void Awake()
    {
        purchaseButton.onClick.AddListener(OnPurchaseButtonClicked);
    }

    void Start()
    {
        int maxAmount = quantitySettingUI.maximumQuantity;
        purchaseButton.interactable = maxAmount > 0;
    }

    public void ShowDetailInfo(ShopItemInfoViewData itemInfoData)
    {
        quantitySettingUI.ResetQuantity();
        itemDetailInfoPanel.SetActive(true);
        
        txtPencilName.text = itemInfoData.PencilName;
        txtPencilShortInfo.text = itemInfoData.PencilShortInfo;
        txtPencilDetailInfo.text = itemInfoData.PencilDetailInfo;
    }

    void OnPurchaseButtonClicked()
    {
        if (!purchaseButton.interactable) return;
        OnPurchaseButtonClickedEvent?.Invoke(quantitySettingUI.CurrentQuantity);
    }
}

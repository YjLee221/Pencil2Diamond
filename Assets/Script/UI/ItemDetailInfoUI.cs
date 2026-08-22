using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailInfoUI : MonoBehaviour
{
    [SerializeField] GameObject itemDetailInfoPanel;
    [SerializeField] Button purchaseButton;
    
    QuantitySettingUI quantitySettingUI;

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

    public void ShowDetailInfo()
    {
        itemDetailInfoPanel.SetActive(true);
    }

    void OnPurchaseButtonClicked()
    {
        if (!purchaseButton.interactable) return;
        
        OnPurchaseButtonClickedEvent?.Invoke(quantitySettingUI.CurrentQuantity);
    }
}

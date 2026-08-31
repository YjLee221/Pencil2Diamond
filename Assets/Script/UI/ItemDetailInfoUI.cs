using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailInfoUI : MonoBehaviour
{
    [SerializeField] GameObject itemDetailInfoPanel;
    [SerializeField] Button purchaseButton;
    
    [SerializeField] QuantitySettingUI quantitySettingUI;

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
        if (quantitySettingUI.CurrentQuantity > 1) quantitySettingUI.CurrentQuantity = 1;
    }

    void OnPurchaseButtonClicked()
    {
        if (!purchaseButton.interactable) return;
        OnPurchaseButtonClickedEvent?.Invoke(quantitySettingUI.CurrentQuantity);
    }
}

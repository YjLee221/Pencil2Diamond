using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMarketUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Button closeButton;
    [SerializeField] GameObject mainMarketPanel;
    [SerializeField] QuantitySettingUI quantitySettingUI;

    [Header("Buttons")] 
    [SerializeField] Button itemButton;

    public event Action OnItemButtonClickedEvent;
    public event Action<int> OnPurchaseButtonClickedEvent;

    void Start()
    {
        itemButton.onClick.AddListener(OnClickedItemButton);
        closeButton.onClick.AddListener(OnClickedCloseButton);
    }
    
    void OnEnable()
    {
        quantitySettingUI.OnActionButtonClickedEvent += HandlePurchaseButtonClicked;
    }


    void OnClickedItemButton()
    {
        OnItemButtonClickedEvent?.Invoke();
    }

    void HandlePurchaseButtonClicked(int amount)
    {
        OnPurchaseButtonClickedEvent?.Invoke(amount);
    }

    public void ConfigurePurchaseQuantity(int maxPurchasableAmount)
    {
        quantitySettingUI.Configure(
            maxPurchasableAmount,
            1,
            "구입하기",
            maxPurchasableAmount > 0);
    }
        
    void OnClickedCloseButton()
    {
        mainMarketPanel.SetActive(false);
    }

    void OnDisable()
    {
        quantitySettingUI.OnActionButtonClickedEvent -= HandlePurchaseButtonClicked;
    }
}

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
    [SerializeField] MainMarketController mainMarketController;

    ShopItemInfoViewData itemInfoData;
    
    public event Action<ShopItemInfoViewData, int> OnPurchaseButtonClickedEvent;

    void Awake()
    {
        purchaseButton.onClick.AddListener(OnPurchaseButtonClicked);
    }

    void Start()
    {
        int maxAmount = quantitySettingUI.maximumQuantity;
        // 나중에는 선택한 연필의 가격 <= 플레이어의 코인 인 경우에만 뜨도록 수정해야할거 같아
        purchaseButton.interactable = maxAmount >= 1;
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
        OnPurchaseButtonClickedEvent?.Invoke(itemInfoData, quantitySettingUI.CurrentQuantity);
    }

    void OnDisable()
    {
        quantitySettingUI.OnQuantityChangedEvent -= HandledQuantityChangedEvent;
    }
}

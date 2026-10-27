using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMarketUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Button closeButton;
    [SerializeField] GameObject mainMarketPanel;
    [SerializeField] GameObject setAmountPanel;
    [SerializeField] GameObject buttonList;

    [Header("Buttons")] 
    [SerializeField] Button itemButton;

    [SerializeField] PlayerInventoryManager inventoryManager;

    ShopListViewData shopListViewData;

    Dictionary<PencilType, TextMeshProUGUI> playerPencilCount = new();
    
    // 이벤트
    public event Action<PencilType> OnItemButtonClickedEvent;

    void OnEnable()
    {
        setAmountPanel.SetActive(false);
        buttonList.SetActive(true);
    }

    void Start()
    {
        closeButton.onClick.AddListener(OnClickedCloseButton);
    }
    
    void OnClickedItemButton(PencilType shopItemData)
    {
        OnItemButtonClickedEvent?.Invoke(shopItemData);
        
        if (setAmountPanel.activeSelf) buttonList.SetActive(false);
    }
        
    void OnClickedCloseButton()
    {
        if(setAmountPanel.activeSelf)
        {
            setAmountPanel.SetActive(false);
            buttonList.SetActive(true);
        }
        else mainMarketPanel.SetActive(false);
    }

    public void SettingItemButtons(ShopListViewData[] itemListData)
    {
        // Scene에 미리 그려둔 아이템박스를 아이템 버튼 복제용으로만 사용
        itemButton.gameObject.SetActive(false);

        foreach (ShopListViewData shopItemData in itemListData)
        {
            Button pencilButton = Instantiate(itemButton, itemButton.transform.parent);

            pencilButton.image.sprite = shopItemData.PencilSprite;
            PencilType pencilType = shopItemData.PencilType;

            var havingTextComponent = pencilButton.transform.Find("TxtHavingCount").GetComponent<TextMeshProUGUI>();
            havingTextComponent.text = $"{shopItemData.UserPencilCount.ToString()}";
            
            playerPencilCount[pencilType] = havingTextComponent;

            var priceTextComponent = pencilButton.transform.Find("TxtPencilPrice").GetComponent<TextMeshProUGUI>();
            priceTextComponent.text = $"{shopItemData.PencilPrice.ToString() + " 골드"}";

            pencilButton.onClick.AddListener(() => OnClickedItemButton(pencilType));
            
            pencilButton.gameObject.SetActive(true);
        }
    }

    public void UpdatePlayerPencilCount(PencilType pencilType, int pencilCount)
    {
        var findPencilData = inventoryManager.FindPencilData(pencilType);
        findPencilData.unSharpenedCount = pencilCount;
        playerPencilCount[findPencilData.pencilType].text = findPencilData.unSharpenedCount.ToString();
    }
}

using System;
using System.Drawing;
using Unity.VisualScripting;
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

    [SerializeField] ShopListViewData shopListViewData;

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
        // 기존 아이템 버튼 복제용으로만 사용
        itemButton.gameObject.SetActive(false);
        
        foreach (ShopListViewData shopItemData in itemListData)
        {
            Button pencilButton = Instantiate(itemButton, itemButton.transform.parent);

            pencilButton.image.sprite = shopItemData.PencilSprite;
            
            PencilType pencilType = shopItemData.PencilType;
            
            pencilButton.onClick.AddListener(()=>OnClickedItemButton(pencilType));
            
            pencilButton.gameObject.SetActive(true);
        }
    }
}

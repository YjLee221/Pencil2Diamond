using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("WorkShop Level")] 
    [SerializeField] TextMeshProUGUI workShopLevelText;
    [SerializeField] TextMeshProUGUI workShopLevelName;

    [Header("Currency")] 
    [SerializeField] TextMeshProUGUI diamondCount;
    [SerializeField] TextMeshProUGUI coinCount;

    [Header("Buttons")] 
    [SerializeField] Button marketButton;
    [SerializeField] Button missionButton;
    [SerializeField] Button upgradeButton;
    [SerializeField] Button sellingButton;

    bool isWorkingPanelClosed;
    Coroutine hideCoroutine;
    
    [Header("Data")]
    [SerializeField] PlayerData player;
    [SerializeField] WorkShopData workshop;
    [SerializeField] PlayerInventoryManager inventoryManager;
    
    [SerializeField] MainWorkshopUI mainWorkshopUI;

    public event Action OnMarketButtonClickedEvent;
    public event Action OnMissionButtonClickedEvent;
    public event Action OnUpgradeButtonClickedEvent;
    public event Action OnSellingButtonClickedEvent;

    void Start()
    {
        ShowInfo();
 
        marketButton.onClick.AddListener(OnMarketButtonClicked);
        missionButton.onClick.AddListener(OnMissionButtonClicked);
        upgradeButton.onClick.AddListener(OnUpgradeButtonClicked);
        sellingButton.onClick.AddListener(OnSellingButtonClicked);
    }

    void OnEnable()
    {
        inventoryManager.OnInventoryChangedEvent += ShowInfo;
        ShowInfo();
    }

    void OnDisable()
    {
        inventoryManager.OnInventoryChangedEvent -= ShowInfo;

        if (hideCoroutine == null) return;
        StopCoroutine(hideCoroutine);
        hideCoroutine = null;
    }

    void ShowInfo()
    {
        workShopLevelText.text = $"Lv.{player.currentWorkshopLevel}";
        workShopLevelName.text = workshop.GetWorkShopLevelName((WorkShopLevelType)player.currentWorkshopLevel);
        diamondCount.text = player.diamondCount.ToString();
        coinCount.text = player.coinCount.ToString();
    }

    void OnMarketButtonClicked()
    {
        OnMarketButtonClickedEvent?.Invoke();
    }

    void OnMissionButtonClicked()
    {
        OnMissionButtonClickedEvent?.Invoke();
    }

    void OnUpgradeButtonClicked()
    {
        OnUpgradeButtonClickedEvent?.Invoke();
    }

    void OnSellingButtonClicked()
    {
        OnSellingButtonClickedEvent?.Invoke();
    }
}

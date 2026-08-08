using System;
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
    [SerializeField] Button pressButton;
    [SerializeField] Button sharpeningButton;

    [SerializeField] Button plusButton;
    [SerializeField] Button minusButton;
    [SerializeField] Button maxButton;

    [SerializeField] GameObject workingPanel;
    [SerializeField] TextMeshProUGUI workingPanelContents;
    bool isWorkingPanelClosed;

    [SerializeField] PlayerData player;
    [SerializeField] WorkShopData workshop;
    [SerializeField] PlayerInventoryManager inventoryManager;
    [SerializeField] SettingTemperature pressMachine;
    
    WorkingStep checkWorkStep = WorkingStep.None;

    public event Action OnMarketButtonClickedEvent;
    public event Action OnMissionButtonClickedEvent;
    public event Action OnUpgradeButtonClickedEvent;
    public event Action OnSellingButtonClickedEvent;
    public event Action OnPressingButtonClickedEvent;
    public event Action OnSharpeningButtonClickedEvent;

    void Start()
    {
        ShowInfo();

        marketButton.onClick.AddListener(OnMarketButtonClicked);
        missionButton.onClick.AddListener(OnMissionButtonClicked);
        upgradeButton.onClick.AddListener(OnUpgradeButtonClicked);
        sellingButton.onClick.AddListener(OnSellingButtonClicked);
        pressButton.onClick.AddListener(OnPressingButtonClicked);
        sharpeningButton.onClick.AddListener(OnSharpeningButtonClicked);
    }

    void OnEnable()
    {
        inventoryManager.OnInventoryChangedEvent += ShowInfo;
        ShowInfo();
    }

    void OnDisable()
    {
        inventoryManager.OnInventoryChangedEvent -= ShowInfo;
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

    void OnPressingButtonClicked()
    {
        OnPressingButtonClickedEvent?.Invoke();
    }

    void OnSharpeningButtonClicked()
    {
        OnSharpeningButtonClickedEvent?.Invoke();
    }

    public void ShowWorkAbleList(WorkingStep workingStep)
    {
        if (workingPanel.activeSelf && checkWorkStep == workingStep)
        {
            CloseWorkAbleList();
            return;
        }

        checkWorkStep = workingStep;
        workingPanel.SetActive(true);

        switch (workingStep)
        {
            case WorkingStep.Sharpening:
                workingPanelContents.text = $"보유한 연필: {player.unSharpenedPencilCount}\n " +
                                            $"[ 가공할 수량 ]\n" +
                                            
                                            $"예상 획득: 흑연 {player.unSharpenedPencilCount} 개";
                break;
            
            case WorkingStep.CollectingGraphite:
                workingPanelContents.text = $"보유한 흑연: {player.graphiteCount}";
                break;
            
            case WorkingStep.Pressing:
                workingPanelContents.text = $"현재 압축기 레벨 {pressMachine.machineLevel}\n" +
                                            $"필요한 흑연: 3 / {player.graphiteCount}";
                break;
        }
    }

    void CloseWorkAbleList()
    {
        workingPanel.SetActive(false);
        checkWorkStep = WorkingStep.None;
    }
}
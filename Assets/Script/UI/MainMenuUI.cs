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
    [SerializeField] Button pressButton;
    [SerializeField] Button sharpeningButton;
    
    [Header("AbleWorkingList")]
    [SerializeField] GameObject workingPanel;
    [SerializeField] TextMeshProUGUI workingPanelContents;
    [SerializeField] Button startWorkingBtn;
    [SerializeField] GameObject buttonsPanel;
    
    [SerializeField] TextMeshProUGUI workingAbleAmountText;
    int workingAbleAmount = 1;
    [SerializeField] int workingAbleAmountMax;
    
    [SerializeField] Button plusButton;
    [SerializeField] Button minusButton;
    // [SerializeField] Button maxButton;

    [SerializeField] TextMeshProUGUI warningText;
    [SerializeField] float displayDuration = 1.5f;

    bool isWorkingPanelClosed;
    Coroutine hideCoroutine;
    
    [Header("Data")]
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
    public event Action<WorkingStep, int> OnStartWorkButtonClickedEvent;

    void Awake()
    {
        warningText.gameObject.SetActive(false);
    }

    void Start()
    {
        ShowInfo();

        workingAbleAmountText.text = workingAbleAmount.ToString();

        marketButton.onClick.AddListener(OnMarketButtonClicked);
        missionButton.onClick.AddListener(OnMissionButtonClicked);
        upgradeButton.onClick.AddListener(OnUpgradeButtonClicked);
        sellingButton.onClick.AddListener(OnSellingButtonClicked);
        pressButton.onClick.AddListener(OnPressingButtonClicked);
        sharpeningButton.onClick.AddListener(OnSharpeningButtonClicked);
        startWorkingBtn.onClick.AddListener(OnStartWorkButtonClicked);
        plusButton.onClick.AddListener(OnAbleWorkingAmountPlusButtonClicked);
        minusButton.onClick.AddListener(OnAbleWorkingAmountMinusButtonClicked);
    }

    void OnEnable()
    {
        inventoryManager.OnInventoryChangedEvent += ShowInfo;
        ShowInfo();
    }

    void OnDisable()
    {
        inventoryManager.OnInventoryChangedEvent -= ShowInfo;

        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
            hideCoroutine = null;
        }

        warningText.gameObject.SetActive(false);
        startWorkingBtn.interactable = true;
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
    
    void OnStartWorkButtonClicked()
    {
        switch (checkWorkStep)
        {
            case WorkingStep.Sharpening when player.unSharpenedPencilCount < workingAbleAmount:
                ShowWarningMessage("연필 댕부족! 상점에서 사와욧!");
                break;
            
            case WorkingStep.Pressing when player.graphiteCount < workingAbleAmount:
                ShowWarningMessage("흑연 댕부족! 연필을 깎아욧!");
                break;
            
            default:
                OnStartWorkButtonClickedEvent?.Invoke(checkWorkStep, workingAbleAmount);
                break;
        }
    }

    void OnAbleWorkingAmountMinusButtonClicked()
    {
        workingAbleAmount = Mathf.Clamp(workingAbleAmount - 1, 1, workingAbleAmountMax);
        workingAbleAmountText.text = workingAbleAmount.ToString();
    }

    void OnAbleWorkingAmountPlusButtonClicked()
    {
        workingAbleAmount = Mathf.Clamp(workingAbleAmount + 1, 1, workingAbleAmountMax);
        workingAbleAmountText.text = workingAbleAmount.ToString();
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
                workingPanelContents.text = $"현재 가공 가능한 연필 종류: 2B \n" +
                                            $"보유한 연필: {player.unSharpenedPencilCount} 개 \n\n " +
                                            $"[ 가공할 수량 ]\n";
                break;
            
            case WorkingStep.CollectingGraphite:
                workingPanelContents.text = $"보유한 흑연: {player.graphiteCount}";
                break;
            
            case WorkingStep.Pressing:
                workingPanelContents.text = $"현재 압축기 레벨 {pressMachine.machineLevel}\n" +
                                            $"보유한 흑연: {player.graphiteCount} 개 \n\n" +
                                            $"[ 가공할 수량 ]\n";
                break;
        }
        
        buttonsPanel.SetActive(true);
    }

    void CloseWorkAbleList()
    {
        buttonsPanel.SetActive(false);
        workingPanel.SetActive(false);
        checkWorkStep = WorkingStep.None;
    }

    void ShowWarningMessage(string warningMessage)
    {
        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
        }

        warningText.text = warningMessage;
        warningText.gameObject.SetActive(true);
        startWorkingBtn.interactable = false;

        hideCoroutine = StartCoroutine(HideAfterDelay());
    }

    IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);

        startWorkingBtn.interactable = true;
        warningText.gameObject.SetActive(false);
        hideCoroutine = null;
    }
}

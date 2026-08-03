using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [FormerlySerializedAs("WorkShopLevelText")]
    [Header("WorkShop Level")]
    [SerializeField] TextMeshProUGUI workShopLevelText;
    [SerializeField] TextMeshProUGUI workShopLevelName;

    [Header("Currency")]
    [SerializeField] TextMeshProUGUI diamondCount;
    [SerializeField] TextMeshProUGUI coinCount;

    [Header("Buttons")]
    [SerializeField] Button goMainButton;
    [SerializeField] Button missionButton;
    [SerializeField] Button makingButton;
    [SerializeField] Button sellingButton;
    [SerializeField] Button pressButton;

    [SerializeField] PlayerData player;
    [SerializeField] WorkShopData workshop;
    [SerializeField] PlayerInventoryManager inventoryManager;

    public static event Action OnGoMainButtonClickedEvent;
    public static event Action OnMissionButtonClickedEvent;
    public static event Action OnMakingButtonClickedEvent;
    public static event Action OnSellingButtonClickedEvent;
    public static event Action OnPressingButtonClickedEvent;

    void Start()
    {
        ShowInfo();

        goMainButton.onClick.AddListener(OnGoMainButtonClicked);
        missionButton.onClick.AddListener(OnMissionButtonClicked);
        makingButton.onClick.AddListener(OnMakingButtonClicked);
        sellingButton.onClick.AddListener(OnSellingButtonClicked);
        pressButton.onClick.AddListener(OnPressingButtonClicked);
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

    void OnGoMainButtonClicked()
    {
        OnGoMainButtonClickedEvent?.Invoke();
    }

    void OnMissionButtonClicked()
    {
        OnMissionButtonClickedEvent?.Invoke();
    }

    void OnMakingButtonClicked()
    {
        OnMakingButtonClickedEvent?.Invoke();
    }

    void OnSellingButtonClicked()
    {
        OnSellingButtonClickedEvent?.Invoke();
    }
    
    private void OnPressingButtonClicked()
    {
        OnPressingButtonClickedEvent?.Invoke();
    }
}

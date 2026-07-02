using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("WorkShop Level")]
    [SerializeField] TextMeshProUGUI WorkShopLevelText;
    [SerializeField] TextMeshProUGUI WorkShopLevelName;

    [Header("Currency")]
    [SerializeField] TextMeshProUGUI diamondCount;
    [SerializeField] TextMeshProUGUI coinCount;

    [Header("Buttons")]
    [SerializeField] Button goMainButton;
    [SerializeField] Button missionButton;
    [SerializeField] Button makingButton;
    [SerializeField] Button sellingButton;

    [SerializeField] PlayerData player;
    [SerializeField] WorkShopData workshop;

    [SerializeField] UIManager uiManager;

    public static event Action OnSellingButtonClickedEvent;

    void Start()
    {
        ShowInfo();

        goMainButton.onClick.AddListener(OnGoMainButtonClicked);
        missionButton.onClick.AddListener(OnMissionButtonClicked);
        makingButton.onClick.AddListener(OnMakingButtonClicked);
        sellingButton.onClick.AddListener(OnSellingButtonClicked);
    }

    void ShowInfo()
    {
        WorkShopLevelText.text = $"Lv.{player.currentWorkshopLevel}";
        WorkShopLevelName.text = workshop.GetWorkShopLevelName((WorkShopLevelType)player.currentWorkshopLevel);
        diamondCount.text = player.diamondCount.ToString();
        coinCount.text = player.coinCount.ToString();
    }

    void OnGoMainButtonClicked()
    {
        // 메인 화면으로 이동하는 로직 구현
        Debug.Log("Go Main Button Clicked");
    }

    void OnMissionButtonClicked()
    {
        // 미션 화면으로 이동하는 로직 구현
        Debug.Log("Mission Button Clicked");
    }

    void OnMakingButtonClicked()
    {
        // 제작 화면으로 이동하는 로직 구현
        Debug.Log("Making Button Clicked");
    }

    void OnSellingButtonClicked()
    {
        uiManager.ShowJewelShop();
        OnSellingButtonClickedEvent?.Invoke();
    }
}

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
        WorkShopLevelText.text = $"Level: {PlayerData.Instance.WorkShopLevel}";
        WorkShopLevelName.text = PlayerData.Instance.WorkShopLevelName;
        diamondCount.text = PlayerData.Instance.Diamonds.ToString();
        coinCount.text = PlayerData.Instance.Coins.ToString();
    }
}

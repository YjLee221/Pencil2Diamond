using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuantitySettingUI : MonoBehaviour
{
    [Header("Quantity")]
    [SerializeField] Button minusButton;
    [SerializeField] TextMeshProUGUI quantityText;
    [SerializeField] Button plusButton;
    [SerializeField, Min(0)] public int maximumQuantity = 10;
    [SerializeField, Min(0)] int initialQuantity = 1;

    public int CurrentQuantity { get; set; }

    void Awake()
    {
        minusButton.onClick.AddListener(OnMinusButtonClicked);
        plusButton.onClick.AddListener(OnPlusButtonClicked);
        
        Configure(maximumQuantity, initialQuantity);
    }

    void OnDestroy()
    {
        minusButton.onClick.RemoveListener(OnMinusButtonClicked);
        plusButton.onClick.RemoveListener(OnPlusButtonClicked);
    }

    public void Configure(
        int maxQuantity,
        int startQuantity = 1)
    {
        maximumQuantity = Mathf.Max(0, maxQuantity);
        CurrentQuantity = maximumQuantity == 0
            ? 0
            : Mathf.Clamp(startQuantity, 1, maximumQuantity);
        Refresh();
    }

    void OnMinusButtonClicked()
    {
        SetQuantity(CurrentQuantity - 1);
    }

    void OnPlusButtonClicked()
    {
        SetQuantity(CurrentQuantity + 1);
    }
    
    void SetQuantity(int quantity)
    {
        CurrentQuantity = maximumQuantity == 0 ? 0 : Mathf.Clamp(quantity, 1, maximumQuantity);

        Refresh();
    }

    void Refresh()
    {
        quantityText.text = CurrentQuantity.ToString();
        minusButton.interactable = maximumQuantity > 0 && CurrentQuantity > 1;
        plusButton.interactable = maximumQuantity > 0 && CurrentQuantity < maximumQuantity;
    }
}

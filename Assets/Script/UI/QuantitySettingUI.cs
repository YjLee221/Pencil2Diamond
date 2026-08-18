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
    [SerializeField, Min(0)] int maximumQuantity = 10;
    [SerializeField, Min(0)] int initialQuantity = 1;

    [Header("Action")]
    [SerializeField] Button actionButton;
    [SerializeField] TextMeshProUGUI actionButtonText;

    public int CurrentQuantity { get; private set; }

    public event Action<int> OnActionButtonClickedEvent;

    void Awake()
    {
        minusButton.onClick.AddListener(OnMinusButtonClicked);
        plusButton.onClick.AddListener(OnPlusButtonClicked);
        actionButton.onClick.AddListener(OnActionButtonClicked);

        Configure(maximumQuantity, initialQuantity);
    }

    void OnDestroy()
    {
        minusButton.onClick.RemoveListener(OnMinusButtonClicked);
        plusButton.onClick.RemoveListener(OnPlusButtonClicked);
        actionButton.onClick.RemoveListener(OnActionButtonClicked);
    }

    public void Configure(
        int maxQuantity,
        int startQuantity = 1,
        string actionText = null,
        bool actionInteractable = true)
    {
        maximumQuantity = Mathf.Max(0, maxQuantity);
        CurrentQuantity = maximumQuantity == 0
            ? 0
            : Mathf.Clamp(startQuantity, 1, maximumQuantity);

        if (!string.IsNullOrEmpty(actionText))
        {
            actionButtonText.text = actionText;
        }

        SetActionInteractable(actionInteractable);
        Refresh();
    }

    public void SetActionInteractable(bool interactable)
    {
        actionButton.interactable = interactable && maximumQuantity > 0;
    }

    void OnMinusButtonClicked()
    {
        SetQuantity(CurrentQuantity - 1);
    }

    void OnPlusButtonClicked()
    {
        SetQuantity(CurrentQuantity + 1);
    }

    void OnActionButtonClicked()
    {
        if (!actionButton.interactable)
        {
            return;
        }

        OnActionButtonClickedEvent?.Invoke(CurrentQuantity);
    }

    void SetQuantity(int quantity)
    {
        CurrentQuantity = maximumQuantity == 0
            ? 0
            : Mathf.Clamp(quantity, 1, maximumQuantity);

        Refresh();
    }

    void Refresh()
    {
        quantityText.text = CurrentQuantity.ToString();
        minusButton.interactable = maximumQuantity > 0 && CurrentQuantity > 1;
        plusButton.interactable = maximumQuantity > 0 && CurrentQuantity < maximumQuantity;
    }
}

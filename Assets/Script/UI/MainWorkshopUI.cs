using System;
using UnityEngine;
using UnityEngine.UI;

public class MainWorkshopUI : MonoBehaviour
{
    [Header("Buttons")] 
    [SerializeField] Button mainPressingButton;
    [SerializeField] Button mainSharpeningButton;
    
    public event Action OnPressingButtonClickedEvent;
    public event Action OnSharpeningButtonClickedEvent;

    void Start()
    {
        mainPressingButton.onClick.AddListener(OnPressingButtonClicked);
        mainSharpeningButton.onClick.AddListener(OnSharpeningButtonClicked);
    }

    void OnPressingButtonClicked()
    {
        OnPressingButtonClickedEvent?.Invoke();
    }

    void OnSharpeningButtonClicked()
    {
        OnSharpeningButtonClickedEvent?.Invoke();
    }
}

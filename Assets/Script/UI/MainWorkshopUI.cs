using System;
using UnityEngine;
using UnityEngine.UI;

public class MainWorkshopUI : MonoBehaviour
{
    [Header("Buttons")] 
    [SerializeField] Button pressButton;
    [SerializeField] Button sharpeningButton;
    
    public event Action OnPressingButtonClickedEvent;
    public event Action OnSharpeningButtonClickedEvent;

    void Start()
    {
        pressButton.onClick.AddListener(OnPressingButtonClicked);
        sharpeningButton.onClick.AddListener(OnSharpeningButtonClicked);
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

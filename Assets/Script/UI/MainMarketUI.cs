using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMarketUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Button closeButton;
    [SerializeField] GameObject mainMarketPanel;
    [SerializeField] GameObject setAmountPanel;

    [Header("Buttons")] 
    [SerializeField] Button itemButton;

    public event Action OnItemButtonClickedEvent;

    void Start()
    {
        setAmountPanel.SetActive(false);
        
        itemButton.onClick.AddListener(OnClickedItemButton);
        closeButton.onClick.AddListener(OnClickedCloseButton);
    }
    
    void OnClickedItemButton()
    {
        OnItemButtonClickedEvent?.Invoke();
    }
        
    void OnClickedCloseButton()
    {
        
        if(setAmountPanel.activeSelf) setAmountPanel.SetActive(false);
        else mainMarketPanel.SetActive(false);
    }
}

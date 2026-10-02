using System;
using UnityEngine;

// ReSharper disable All

public class MainFlowController : MonoBehaviour
{
    [SerializeField] MainMenuUI mainMenuUI;
    [SerializeField] UIManager uiManager;
    [SerializeField] MainWorkshopUI mainWorkshopUI;
    [SerializeField] PlayerInventoryManager playerInventory;
    [SerializeField] WorkListUI workListUI;
    [SerializeField] GameObject mainMarketUI;
    void Start()
    {
        Debug.Log("Starting Main Flow Controller");
    }

    void OnEnable()
    {
        mainMenuUI.OnMarketButtonClickedEvent += HandleMarketButtonClickedEvent;
        
        mainWorkshopUI.OnPressingButtonClickedEvent += HandlePressingButtonClickedEvent;
        mainWorkshopUI.OnSharpeningButtonClickedEvent += HandleSharpeningButtonClickedEvent;
        workListUI.OnStartWorkingButtonClickedEvent += HandleStartWorkButtonClickedEvent;
    }

    void HandleMarketButtonClickedEvent()
    {
        mainMarketUI.SetActive(true);
    }

    void HandlePressingButtonClickedEvent()
    {
        int availableAmount = playerInventory.GraphiteCount;
        var viewData = new WorkListViewData(WorkingStep.Pressing
                                        , availableAmount
                                        , availableAmount > 0 ? availableAmount : 0
                                        , 1);
        workListUI.ShowWorkAbleList(viewData);
    }

    void HandleSharpeningButtonClickedEvent()
    {
        int availableAmount = playerInventory.UnsharpenedPencilCount;
        var viewData = new WorkListViewData(WorkingStep.Sharpening
                                        , availableAmount
                                        , availableAmount > 0 ? availableAmount : 0
                                        , 1);
        
        workListUI.ShowWorkAbleList(viewData);
    }
    
    private void HandleStartWorkButtonClickedEvent(WorkingStep workingStep, int amount)
    {
        switch (workingStep)
        {
            case WorkingStep.Sharpening:
                uiManager.StartSharpeningCanvas();
                break;
            
            case WorkingStep.CollectingGraphite:
                
                break;
            
            case WorkingStep.Pressing:
                uiManager.AdjustingTemperatureCanvas();
                break;
        }
    }

    void OnDisable()
    {
        mainMenuUI.OnMarketButtonClickedEvent -= HandleMarketButtonClickedEvent;
        
        mainWorkshopUI.OnPressingButtonClickedEvent -= HandlePressingButtonClickedEvent;
        mainWorkshopUI.OnSharpeningButtonClickedEvent -= HandleSharpeningButtonClickedEvent;
        workListUI.OnStartWorkingButtonClickedEvent -= HandleStartWorkButtonClickedEvent;
    }
}

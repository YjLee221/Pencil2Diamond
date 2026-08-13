using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

// ReSharper disable All

public class MainFlowController : MonoBehaviour
{
    [SerializeField] MainMenuUI mainMenuUI;
    [SerializeField] UIManager uiManager;
    [SerializeField] MainWorkshopUI mainWorkshopUI;
    [SerializeField] PlayerInventoryManager playerInventory;
    [SerializeField] WorkListUI workListUI;
    void Start()
    {
        Debug.Log("Starting Main Flow Controller");
    }

    void OnEnable()
    {
        mainWorkshopUI.OnPressingButtonClickedEvent += HandlePressingButtonClickedEvent;
        mainWorkshopUI.OnSharpeningButtonClickedEvent += HandleSharpeningButtonClickedEvent;
        workListUI.OnStartWorkButtonClickedEvent += HandleStartWorkButtonClickedEvent;
    }

    void HandlePressingButtonClickedEvent()
    {
        int availableAmount = playerInventory.GraphiteCount;
        var viewData = new WorkListViewData(WorkingStep.Pressing
                                        , availableAmount
                                        , Math.Min(availableAmount, playerInventory.GraphiteCount)
                                        , 1);
        workListUI.ShowWorkAbleList(viewData);
    }

    void HandleSharpeningButtonClickedEvent()
    {
        int availableAmount = playerInventory.UnsharpenedPencilCount;
        var viewData = new WorkListViewData(WorkingStep.Sharpening
                                        , availableAmount
                                        , Math.Min(availableAmount, 10)
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
        mainWorkshopUI.OnPressingButtonClickedEvent -= HandlePressingButtonClickedEvent;
        mainWorkshopUI.OnSharpeningButtonClickedEvent -= HandleSharpeningButtonClickedEvent;
        workListUI.OnStartWorkButtonClickedEvent -= HandleStartWorkButtonClickedEvent;
    }
}

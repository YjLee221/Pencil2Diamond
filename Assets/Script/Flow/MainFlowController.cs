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
        // 수정 list: 일단은 연필의 종류가 2B 밖에 없어 값을 박아놨지만 나중에는 연필을 선택할 수 있도록 만들어야함
        var findPencilType = playerInventory.FindPencilData(PencilType.TwoBPencil);
        int availableAmount = findPencilType.graphiteCount;
        var viewData = new WorkListViewData(WorkingStep.Pressing
                                        , availableAmount
                                        , availableAmount > 0 ? availableAmount : 0
                                        , 1);
        workListUI.ShowWorkAbleList(viewData);
    }

    void HandleSharpeningButtonClickedEvent()
    {
        // 수정 list: 일단은 연필의 종류가 2B 밖에 없어 값을 박아놨지만 나중에는 연필을 선택할 수 있도록 만들어야함
        var findPencilType = playerInventory.FindPencilData(PencilType.TwoBPencil);
        int availableAmount = findPencilType.unSharpenedCount;
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

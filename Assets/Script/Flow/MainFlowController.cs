using System;
using UnityEngine;
// ReSharper disable All

public class MainFlowController : MonoBehaviour
{
    [SerializeField] MainMenuUI mainMenuUI;
    [SerializeField] UIManager uiManager;
    void Start()
    {
        Debug.Log("Starting Main Flow Controller");
    }

    void OnEnable()
    {
        mainMenuUI.OnPressingButtonClickedEvent += HandlePressingButtonClickedEvent;
        mainMenuUI.OnSharpeningButtonClickedEvent += HandleSharpeningButtonClickedEvent;
        mainMenuUI.OnStartWorkButtonClickedEvent += HandleStartWorkButtonClickedEvent;
    }

    void HandlePressingButtonClickedEvent()
    {
        mainMenuUI.ShowWorkAbleList(WorkingStep.Pressing);
    }

    void HandleSharpeningButtonClickedEvent()
    {
        mainMenuUI.ShowWorkAbleList(WorkingStep.Sharpening);
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
        mainMenuUI.OnPressingButtonClickedEvent -= HandlePressingButtonClickedEvent;
        mainMenuUI.OnSharpeningButtonClickedEvent -= HandleSharpeningButtonClickedEvent;
        mainMenuUI.OnStartWorkButtonClickedEvent -= HandleStartWorkButtonClickedEvent;
    }
}

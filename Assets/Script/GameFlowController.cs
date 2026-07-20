using System;
using UnityEngine;

public class GameFlowController : MonoBehaviour
{
    public GamePhase currentGamePhase;
    public TutorialStep currentTutorialStep;

    [Header("DialogId")]
    [SerializeField] string tutorialStartDialogId = "100101";

    [Header("Manager")]
    [SerializeField] TalkManager talkManager;
    [SerializeField] PencilManager pencilManager;
    [SerializeField] UIManager uiManager;
    
    [Header("Player")]
    [SerializeField] PlayerInventoryManager playerInventoryManager;

    [Header("Diamond")] 
    [SerializeField] DiamondData tutorialDiamond;

    public void TutorialStart()
    {
        playerInventoryManager.ResetInventory();
        
        currentGamePhase = GamePhase.Tutorial;
        currentTutorialStep = TutorialStep.OpeningDialog;

        talkManager.StartDialog(tutorialStartDialogId);
        pencilManager.StartTutorialMode();
    }

    void OnEnable()
    {
        talkManager.OnDialogFinished += HandleDialogFinished;
        talkManager.OnDialogSequenceFinished += HandleTutorialFinished;
        SharpeningPencil.OnPencilSharpeningCompleted += HandleCompletedPencilSharpening;
        PencilCollectedGraphite.OnGraphiteExtractionCompleted += HandleCompletedGraphiteExtraction;
        PressMachine.OnMatchingTemperatureCompleted += HandleCompletedTemperature;
        MainMenuUI.OnSellingButtonClickedEvent += HandleSellingButtonClicked;
    }

    void HandleDialogFinished(string command)
    { 
        switch (command)
        {
            case "SharpenPencil":
                currentTutorialStep = TutorialStep.SharpeningPencil;
                uiManager.StartSharpeningCanvas();
                break;
            
            case "ExtractGraphite":
                currentTutorialStep = TutorialStep.ExtractingGraphite;
                uiManager.ExtractingGraphiteCanvas();
                break;
            
            case "AddGraphite":
                playerInventoryManager.AddGraphite();
                break;
            
            case "AdjustTemperature":
                currentTutorialStep = TutorialStep.AdjustingTemperature;
                uiManager.AdjustingTemperatureCanvas();
                break;
            
            case "AddDiamond":
                playerInventoryManager.AddDiamond();
                break;
            
            case "SellDialog":
                currentTutorialStep = TutorialStep.SellingDialog;
                uiManager.ShowMainCanvas();
                break;
            
            case "OpenJewelShop":
                currentTutorialStep = TutorialStep.SellingDiamond;
                uiManager.ShowJewelShop();
                break;
            
            case "AddCoin":
                playerInventoryManager.SellDiamond();
                playerInventoryManager.AddCoin();
                talkManager.ResumeAfterMinigame();
                break;
            
            case "End":
                currentTutorialStep = TutorialStep.EndingDialog;
                currentGamePhase = GamePhase.Workshop;
                uiManager.ShowMainCanvas();
                break;
        }
    }
    
    private void HandleTutorialFinished()
    {
        if (currentGamePhase != GamePhase.Tutorial) return;
        
        currentTutorialStep =  TutorialStep.Completed;
        currentGamePhase = GamePhase.Workshop;
        uiManager.ShowMainCanvas();
    }

    void HandleCompletedPencilSharpening(SharpeningPencil sharpeningPencil)
    {
        talkManager.ResumeAfterMinigame();
    }
    
    void HandleCompletedGraphiteExtraction(PencilCollectedGraphite collectedGraphite)
    {
        talkManager.ResumeAfterMinigame();
    }

    void HandleCompletedTemperature(bool isSuccess)
    {
        talkManager.ResumeAfterMinigame_successOrFail(isSuccess);
    }

    void HandleSellingButtonClicked()
    {
        if(currentGamePhase == GamePhase.Tutorial)
        {
            currentTutorialStep = TutorialStep.SellingDiamond;
            uiManager.ShowJewelShop();
            talkManager.ResumeAfterMinigame();
        }
        else
        {
            Debug.Log("Selling Button Clicked");
        }
        
    }

    private void OnDisable()
    {
        talkManager.OnDialogFinished -= HandleDialogFinished;
        talkManager.OnDialogSequenceFinished -= HandleTutorialFinished;
        SharpeningPencil.OnPencilSharpeningCompleted -= HandleCompletedPencilSharpening;
        PencilCollectedGraphite.OnGraphiteExtractionCompleted -= HandleCompletedGraphiteExtraction;
        PressMachine.OnMatchingTemperatureCompleted -= HandleCompletedTemperature;
        MainMenuUI.OnSellingButtonClickedEvent -= HandleSellingButtonClicked;
    }

    void ChangeGamePhase()
    {
        currentGamePhase = GamePhase.Workshop;
        currentTutorialStep = TutorialStep.Completed;
    }
}
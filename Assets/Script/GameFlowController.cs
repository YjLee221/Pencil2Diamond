using System;
using UnityEngine;

public class GameFlowController : MonoBehaviour
{
    public GamePhase currentGamePhase;
    public TutorialStep currentTutorialStep;

    [Header("DialogId")]
    [SerializeField] string tutorialStartDialogId = "100101";

    [SerializeField] TalkManager talkManager;
    [SerializeField] PencilManager pencilManager;
    [SerializeField] UIManager uiManager;

    public void TutorialStart()
    {
        currentGamePhase = GamePhase.Tutorial;
        currentTutorialStep = TutorialStep.OpeningDialog;

        talkManager.StartDialog(tutorialStartDialogId);
        pencilManager.StartTutorialMode();
    }

    void OnEnable()
    {
        talkManager.OnDialogFinished += HandleDialogFinished;
        SharpeningPencil.OnPencilSharpeningCompleted += HandleCompletedPencilSharpening;
        PencilCollectedGraphite.OnGraphiteExtractionCompleted += HandleCompletedGraphiteExtraction;
        PressMachine.OnMatchingTemperatureCompleted += HandleCompletedTemperature;
        MainMenuUI.OnSellingButtonClickedEvent += HandleSellingButtonClicked;
    }

    void HandleDialogFinished(string speakerType)
    {
        switch (speakerType)
        {
            case "SharpeningPencil":
                currentTutorialStep = TutorialStep.SharpeningPencil;
                uiManager.StartSharpeningCanvas();
                break;
            
            case "ExtractingGraphite":
                currentTutorialStep = TutorialStep.ExtractingGraphite;
                uiManager.ExtractingGraphiteCanvas();
                break;
            
            case "AdjustingTemperature":
                currentTutorialStep = TutorialStep.AdjustingTemperature;
                uiManager.AdjustingTemperatureCanvas();
                break;
            
            case "SellingDialog":
                currentTutorialStep = TutorialStep.SellingDialog;
                uiManager.ShowMainCanvas();
                break;
            
            case "Merchant":
                currentTutorialStep = TutorialStep.SellingDiamond;
                uiManager.ShowJewelShop();
                break;
            
            case "End":
                Debug.Log("튜토리얼 마지막 대사!!!!");
                currentTutorialStep = TutorialStep.EndingDialog;
                currentGamePhase = GamePhase.Workshop;
                uiManager.ShowMainCanvas();
                break;
        }
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
        SharpeningPencil.OnPencilSharpeningCompleted -= HandleCompletedPencilSharpening;
        PencilCollectedGraphite.OnGraphiteExtractionCompleted -= HandleCompletedGraphiteExtraction;
        PressMachine.OnMatchingTemperatureCompleted -= HandleCompletedTemperature;
        MainMenuUI.OnSellingButtonClickedEvent -= HandleSellingButtonClicked;
    }
}

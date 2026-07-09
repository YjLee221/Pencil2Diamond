using System;
using UnityEngine;

public class GameFlowController : MonoBehaviour
{
    public GamePhase currentGamePhase;
    public TutorialStep currentTutorialStep;

    [Header("Dialog")] 
    [SerializeField] string tutorialStartDialogId = "100101";
    [SerializeField] string graphiteStartDialog = "100204";
    [SerializeField] string temperatureStartDialog = "100209";
    [SerializeField] string temperatureRetryDialogId = "100303";
    [SerializeField] string sellingDiamondDialog = "100307";
    [SerializeField] string endingDialog = "100314";
    
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

    void HandleDialogFinished()
    {
        switch (currentTutorialStep)
        {
            case TutorialStep.None:
            case TutorialStep.OpeningDialog:
                currentTutorialStep = TutorialStep.SharpeningPencil;
                break;
            case TutorialStep.SharpeningPencil:
                break;
            case TutorialStep.GraphiteDialog:
                break;
            case TutorialStep.ExtractingGraphite:
                break;
            case TutorialStep.TemperatureDialog:
                break;
            case TutorialStep.AdjustingTemperature:
                break;
            case TutorialStep.SellingDialog:
                break;
            case TutorialStep.SellingDiamond:
                break;
            case TutorialStep.EndingDialog:
                break;
            case TutorialStep.Completed:
                break;
            default:
                throw new ArgumentOutOfRangeException();
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

    void HandleCompletedTemperature(bool isSuccessed)
    {
        talkManager.ResumeAfterMinigame_successOrFail(isSuccessed);
    }

    void HandleSellingButtonClicked()
    {
        talkManager.ResumeAfterMinigame();
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

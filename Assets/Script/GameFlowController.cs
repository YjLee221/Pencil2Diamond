using System;
using UnityEngine;

public class GameFlowController : MonoBehaviour
{
#if UNITY_EDITOR
    [Header("EditorDebug")] 
    [SerializeField] bool isDebugStarted;
    [SerializeField] GameProgressData debugProcessData;
#endif    
    GameProgressData currentProgressData;
    
    public GamePhase currentGamePhase;
    public TutorialStep currentTutorialStep;

    [Header("Dialog")] 
    [SerializeField] string tutorialStartDialogId = "100101";
    [SerializeField] string graphiteStartDialog = "100204";
    [SerializeField] string temperatureStartDialog = "100209";
    [SerializeField] string temperatureRetryDialogId = "100303";
    [SerializeField] string sellingDiamondDialog = "100307";
    [SerializeField] string endingDialog = "100314";
    
    [SerializeField] PencilManager pencilManager;
    [SerializeField] TalkManager talkManager;
    [SerializeField] UIManager uiManager;

    void Start()
    {
#if UNITY_EDITOR
        if (isDebugStarted && debugProcessData != null)
        {
            ApplyProcessDebug(debugProcessData);
            return;
        }
#endif
        TutorialStart();
    }

    void ApplyProcessDebug(GameProgressData processData)
    {
        currentProgressData = processData;
        currentGamePhase = processData.gamePhase;
        currentTutorialStep = processData.tutorialStep;

        if (processData.gamePhase == GamePhase.Tutorial)
        {
            if (!string.IsNullOrEmpty(processData.currentDialogId))
            {
                talkManager.StartDialog(processData.currentDialogId);
            }
            
            EnterTutorialStep(processData.tutorialStep);
        }
        else if (processData.gamePhase == GamePhase.Workshop)
        {
            StartMainGame();
        }
    }

    public void TutorialStart()
    {
        currentGamePhase = GamePhase.Tutorial;
        EnterTutorialStep(TutorialStep.OpeningDialog);
    }

    void OnEnable()
    {
        talkManager.OnDialogFinished += HandleDialogFinished;
        SharpeningPencil.OnPencilSharpeningCompleted += HandleCompletedPencilSharpening;
        PencilCollectedGraphite.OnGraphiteExtractionCompleted += HandleCompletedGraphiteExtraction;
        PressMachine.OnMatchingTemperatureCompleted += HandleCompletedTemperature;
        MainMenuUI.OnSellingButtonClickedEvent += HandleSellingButtonClicked;
    }

    private void HandleSellingButtonClicked()
    {
        EnterTutorialStep(TutorialStep.EndingDialog);
    }

    private void HandleCompletedTemperature(bool isSuccessed)
    {
        if (isSuccessed)
        {
            EnterTutorialStep(TutorialStep.SellingDialog);
            return;
        }

        SetProgress(GamePhase.Tutorial, TutorialStep.TemperatureDialog, temperatureRetryDialogId);
        talkManager.StartDialog(temperatureRetryDialogId);
    }

    private void HandleCompletedGraphiteExtraction(PencilCollectedGraphite graphite)
    {
        EnterTutorialStep(TutorialStep.TemperatureDialog);
    }

    private void HandleCompletedPencilSharpening(SharpeningPencil obj)
    {
        EnterTutorialStep(TutorialStep.GraphiteDialog);
    }


    void OnDisable()
    {
        talkManager.OnDialogFinished -= HandleDialogFinished;
        SharpeningPencil.OnPencilSharpeningCompleted -= HandleCompletedPencilSharpening;
        PencilCollectedGraphite.OnGraphiteExtractionCompleted -= HandleCompletedGraphiteExtraction;
        PressMachine.OnMatchingTemperatureCompleted -= HandleCompletedTemperature;
        MainMenuUI.OnSellingButtonClickedEvent -= HandleSellingButtonClicked;
    }

    void HandleDialogFinished()
    {
        switch (currentTutorialStep)
        {
            case TutorialStep.OpeningDialog:
                EnterTutorialStep(TutorialStep.SharpeningPencil);
                break;

            case TutorialStep.GraphiteDialog:
                EnterTutorialStep(TutorialStep.ExtractingGraphite);
                break;

            case TutorialStep.TemperatureDialog:
                EnterTutorialStep(TutorialStep.AdjustingTemperature);
                break;
            
            case TutorialStep.SellingDialog:
                EnterTutorialStep(TutorialStep.SellingDiamond);
                break;
            
            case TutorialStep.EndingDialog:
                EnterTutorialStep(TutorialStep.Completed);
                break;
            
            default:
                break;
        }   
    }

    public void EnterTutorialStep(TutorialStep step)
    {
        currentTutorialStep = step;

        switch (step)
        {
            case TutorialStep.OpeningDialog:
                SetProgress(GamePhase.Tutorial, currentTutorialStep, tutorialStartDialogId);
                talkManager.StartDialog(tutorialStartDialogId);
                break;
            
            case TutorialStep.SharpeningPencil:
                pencilManager.StartTutorialMode();
                uiManager.StartSharpeningCanvas();
                break;
            
            case TutorialStep.GraphiteDialog:
                SetProgress(GamePhase.Tutorial, currentTutorialStep, graphiteStartDialog);
                break;
            
            case TutorialStep.ExtractingGraphite:
                uiManager.ExtractingGraphiteCanvas();
                break;
            
            case TutorialStep.TemperatureDialog:
                SetProgress(GamePhase.Tutorial, currentTutorialStep, temperatureStartDialog);
                break;
            
            case TutorialStep.AdjustingTemperature:
                uiManager.PressingCanvas();
                break;
            
            case TutorialStep.SellingDialog:
                SetProgress(GamePhase.Tutorial, currentTutorialStep, sellingDiamondDialog);
                break;
            
            case TutorialStep.SellingDiamond:
                uiManager.ShowMainCanvas();
                break;

            case TutorialStep.EndingDialog:
                SetProgress(GamePhase.Tutorial, currentTutorialStep, endingDialog);
                break;

            case TutorialStep.Completed:
                TutorialEnd();
                break;

            default:
                break;
        }
    }

    void TutorialEnd()
    {
        currentTutorialStep = TutorialStep.Completed;
        currentGamePhase = GamePhase.Workshop;
    }
    
    void StartMainGame()
    {
        SetProgress(GamePhase.Workshop, TutorialStep.Completed);
        uiManager.ShowMainCanvas();
    }

    void SetProgress(GamePhase gamePhase, TutorialStep step, string dialogId = "")
    {
        currentProgressData ??= new GameProgressData();
        
        currentGamePhase = gamePhase;
        currentTutorialStep = step;
        
        currentProgressData.gamePhase =  gamePhase;
        currentProgressData.tutorialStep = step;
        currentProgressData.currentDialogId =  dialogId;

    }
}

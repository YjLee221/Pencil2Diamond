using System;
using UnityEngine;

public class GameFlowController : MonoBehaviour
{
    /*
     * GamePhase
     게임 전체에서 현재 플레이어가 위치한 큰 흐름
    화면과 입력 규칙이 크게 바뀌는 단위로 구분
     */
    public enum GamePhase
    {
        None,       // 아직 게임 흐름이 시작되지 않음
        Tutorial,   // 튜토리얼 진행 중
        Workshop    // 메인 공방 플레이 중
    }

    /*
     * TutorialStep
     튜토리얼 진행 단계
     */
    public enum TutorialStep
    {
        None,

        OpeningDialog,      // 튜토리얼 시작 대화
        SharpeningPencil,     // 연필 깎기

        GraphiteDialog,       // 흑연 추출 대화
        ExtractingGraphite,   // 흑연 추출

        TemperatureDialog,    // 온도 조절 대화
        AdjustingTemperature, // 온도 조절

        SellingDialog,        // 보석 판매 대화
        SellingDiamond,       // 보석 판매

        EndingDialog,         // 튜토리얼 종료 대화
        Completed
    }

    public GamePhase currentGamePhase { get; private set; } = GamePhase.None;
    public TutorialStep currentTutorialStep { get; private set; } = TutorialStep.None;

    [SerializeField] string tutorialStartDialogId = "100101";
    [SerializeField] PencilManager pencilManager;
    [SerializeField] TalkManager talkManager;
    [SerializeField] UIManager uiManager;

    public void TutorialStart()
    {
        currentGamePhase = GamePhase.Tutorial;
        EnterTutorialStep(TutorialStep.OpeningDialog);
    }

    void OnEnable()
    {
        talkManager.OnDialogFinished += HandleDialogFinished;
        SharpeningPencil.OnPencilSharpeningCompleted += HandleCompletedPencilSharpening;
        PencilCollectedGraphite.OnGraphiteExtractionCompleted += HandleCompletedPencilSharpening;
        PressMachine.OnMatchingTemperatureCompleted += HandleCompletedPencilSharpening;
        MainMenuUI.OnSellingButtonClickedEvent += HandleSellingButtonClicked;
    }

<<<<<<< Updated upstream
    private void HandleSellingButtonClicked()
    {
        throw new NotImplementedException();
    }

    private void HandleCompletedPencilSharpening(bool obj)
    {
        throw new NotImplementedException();
    }

    private void HandleCompletedPencilSharpening(PencilCollectedGraphite graphite)
    {
        throw new NotImplementedException();
    }

    private void HandleCompletedPencilSharpening(SharpeningPencil pencil)
    {
        throw new NotImplementedException();
    }

    void OnDisable()
    {
        talkManager.OnDialogFinished -= HandleDialogFinished;
        SharpeningPencil.OnPencilSharpeningCompleted -= HandleCompletedPencilSharpening;
        PencilCollectedGraphite.OnGraphiteExtractionCompleted -= HandleCompletedPencilSharpening;
        PressMachine.OnMatchingTemperatureCompleted -= HandleCompletedPencilSharpening;
        MainMenuUI.OnSellingButtonClickedEvent -= HandleSellingButtonClicked;
    }

    void HandleDialogFinished()
=======
    void HandleDialogFinished(string gameType)
>>>>>>> Stashed changes
    {
        switch (gameType)
        {
<<<<<<< Updated upstream
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
                talkManager.StartDialog(tutorialStartDialogId);
                break;
            
            case TutorialStep.SharpeningPencil:
                pencilManager.StartTutorialMode();
                uiManager.StartSharpeningCanvas();
                break;
            
            case TutorialStep.GraphiteDialog:
                break;
            
            case TutorialStep.ExtractingGraphite:
                uiManager.ExtractingGraphiteCanvas();
                break;
            
            case TutorialStep.TemperatureDialog:
                break;
            
            case TutorialStep.AdjustingTemperature:
                uiManager.PressingCanvas();
                break;
            
            case TutorialStep.SellingDialog:
                break;
            
            case TutorialStep.SellingDiamond:
                uiManager.ShowMainCanvas();
                break;

            case TutorialStep.EndingDialog:
                break;

            case TutorialStep.Completed:
                TutorialEnd();
                break;

            default:
                break;
        }
    }

    public void TutorialEnd()
    {
        currentTutorialStep = TutorialStep.Completed;
        currentGamePhase = GamePhase.Workshop;
=======
            case "SharpeningPencil":
                StartSharpeningPencil();
                break;
            
            case "ExtractingGraphite" :
                ExtractingGraphite();
                break;
            
            case "SettingTemperature":
                SettingTemperature();
                break;
            
            case "SellingDiamond":
                SellingDiamond();
                break;
        }
    }

    void StartSharpeningPencil()
    {
        currentTutorialStep = TutorialStep.SharpeningPencil;
        uiManager.StartSharpeningCanvas();
    }
    
    void ExtractingGraphite()
    {
        currentTutorialStep = TutorialStep.ExtractingGraphite;
        uiManager.ExtractingGraphiteCanvas();
    }
    
    void SettingTemperature()
    {
        currentTutorialStep = TutorialStep.SettingTemperature;
        uiManager.SettingTemperatureCanvas();
    }
    void SellingDiamond()
    {
        currentTutorialStep = TutorialStep.SellingDiamond;
        uiManager.ShowJewelShop();
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
        talkManager.ResumeAfterMinigame();
    }

    private void OnDisable()
    {
        talkManager.OnDialogFinished -= HandleDialogFinished;
        SharpeningPencil.OnPencilSharpeningCompleted -= HandleCompletedPencilSharpening;
        PencilCollectedGraphite.OnGraphiteExtractionCompleted -= HandleCompletedGraphiteExtraction;
        PressMachine.OnMatchingTemperatureCompleted -= HandleCompletedTemperature;
        MainMenuUI.OnSellingButtonClickedEvent -= HandleSellingButtonClicked;
>>>>>>> Stashed changes
    }
}

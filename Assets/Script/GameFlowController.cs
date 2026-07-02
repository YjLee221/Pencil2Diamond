using UnityEngine;

public class GameFlowController : MonoBehaviour
{
    public bool isTutorialCompleted;

    [SerializeField] PencilManager pencilManager;
    [SerializeField] TalkManager talkManager;
    [SerializeField] DialogData dialogData;

    public void GameStart()
    {
        if (!isTutorialCompleted)
        {
            pencilManager.StartTutorialMode();
            talkManager.StartDialog("100101");

            //isTutorialMode = false;
        }
        else
        {
            // 메인 게임 모드 시작 시 필요한 초기화 작업 수행
        }
    }

    public void TutorialEnd()
    {
        if (dialogData.nextId == "0") isTutorialCompleted = true;
        // 튜토리얼 종료 후 메인 게임 모드로 전환 시 필요한 초기화 작업 수행
    }
}

using UnityEngine;

public class GameMode : MonoBehaviour
{
    public bool isTutorialMode = true;

    [SerializeField] PencilManager pencilManager;
    [SerializeField] TalkManager talkManager;

    public void GameStart()
    {
        if(isTutorialMode)
        {
            pencilManager.StartTutorialMode();
            talkManager.StartDialog("100101");

            isTutorialMode = false;
        }
        else
        {
            // 메인 게임 모드 시작 시 필요한 초기화 작업 수행
        }
    }
}

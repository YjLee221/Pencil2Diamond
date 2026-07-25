using UnityEngine;
using UnityEngine.UI;

public class StartMenuUI: MonoBehaviour
{
    [SerializeField] Button gameStartbutton;
    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject scriptPanel;

    [SerializeField] GameFlowController gameFlowController;
    
#if UNITY_EDITOR
    [Header("개발용 옵션")]
    [SerializeField] private bool startFromWorkshopForDev;
#endif

    void Start()
    {
        if(gameStartbutton != null)
        {
            gameStartbutton.onClick.AddListener(OnClickStartBtn);
        }
    }

    void OnClickStartBtn()
    {
        startPanel.SetActive(false);
        
#if UNITY_EDITOR
        if (startFromWorkshopForDev)
        {
            gameFlowController.MainGameStartForDev();
            return;
        }
#endif
        scriptPanel.SetActive(true);
        
        if(gameFlowController.currentGamePhase == GamePhase.Tutorial) gameFlowController.TutorialStart();
        else gameFlowController.MainGameStartForDev();
    }
}

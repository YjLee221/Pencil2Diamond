using UnityEngine;
using UnityEngine.UI;
//Resharper disable all

public class StartMenuUI: MonoBehaviour
{
    [SerializeField] Button gameStartbutton;
    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject scriptPanel;

    [SerializeField] TutorialFlowController tutorialFlowController;
    
#if UNITY_EDITOR || DEVELOPMENT_BUILD
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

#if UNITY_EDITOR || DEVELOPMENT_BUILD
        if (startFromWorkshopForDev)
        {
            tutorialFlowController.MainGameStartForDev();
            return;
        }
#endif
        
        scriptPanel.SetActive(true);
        
        if(tutorialFlowController.currentGamePhase == GamePhase.Tutorial) tutorialFlowController.TutorialStart();
        else tutorialFlowController.MainGameStartForDev();
    }
}

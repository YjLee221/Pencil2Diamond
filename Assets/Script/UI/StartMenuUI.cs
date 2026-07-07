using UnityEngine;
using UnityEngine.UI;

public class StartMenuUI: MonoBehaviour
{
    [SerializeField] Button gameStartbutton;
    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject scriptPanel;

    [SerializeField] GameFlowController gameFlowController;

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
        scriptPanel.SetActive(true);

        gameFlowController.TutorialStart();
    }
}

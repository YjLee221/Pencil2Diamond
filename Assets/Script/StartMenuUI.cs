using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuUI: MonoBehaviour
{
    [SerializeField] Button gameStartbutton;
    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject scriptPanel;

    [SerializeField] TalkManager talkManager;

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

        talkManager.StartDialog("100101");
    }
}

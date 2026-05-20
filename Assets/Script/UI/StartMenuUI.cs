using UnityEngine;
using UnityEngine.UI;

public class StartMenuUI: MonoBehaviour
{
    [SerializeField] Button gameStartbutton;
    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject scriptPanel;

    [SerializeField] GameMode gameMode;

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

        gameMode.GameStart();
    }
}

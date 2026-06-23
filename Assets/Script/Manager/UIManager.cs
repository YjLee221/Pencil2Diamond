using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Canvas Objects")]
    [SerializeField] GameObject backgroundCanvas;
    [SerializeField] GameObject mainCanvas;
    [SerializeField] GameObject inworkCanvas;
    [SerializeField] GameObject popupCanvas;

    [Header("Working Panel")]
    [SerializeField] GameObject sharpeningPanel;
    [SerializeField] GameObject extractingPanel;
    [SerializeField] GameObject pressingPanel;
    [SerializeField] PressMachine pressMachine;
    [SerializeField] PencilCollectedGraphite graphiteCollector;

    [Header("Background Image")]
    [SerializeField] Image factoryImg;
    [SerializeField] Image deskImg;

    [SerializeField] BaseTool selectedTool;  // 현재 유저가 장착한 도구 (다형성 활용)

    void Start()
    {
        ShowCanvasFirst();
    }

    void ShowCanvasFirst()
    {
        backgroundCanvas.SetActive(true);
        mainCanvas.SetActive(false);
        popupCanvas.SetActive(true);
        inworkCanvas.SetActive(false);
        ShowOnlyWorkPanel(null);
    }

    public void StartWorkingCanvas()
    {
        StartSharpeningCanvas();
    }

    public void StartWorkingCanvas(string dialogId)
    {
        switch (dialogId)
        {
            case "100203":
                StartSharpeningCanvas();
                break;
            case "100208":
                ExtractingGraphiteCanvas();
                break;
            case "100302":
                PressingCanvas();
                break;
            default:
                StartSharpeningCanvas();
                Debug.LogWarning($"No work panel is mapped for dialog id: {dialogId}");
                break;
        }
    }

    public void StartSharpeningCanvas()
    {
        ShowWorkCanvas();
        ShowOnlyWorkPanel(sharpeningPanel);
    }

    public void EndWorkCanvas()
    {
        inworkCanvas.SetActive(false);
        popupCanvas.SetActive(true);
        ShowOnlyWorkPanel(null);

        factoryImg.gameObject.SetActive(true);
        deskImg.gameObject.SetActive(false);
    }

    public void ExtractingGraphiteCanvas()
    {
        ShowWorkCanvas();
        ShowOnlyWorkPanel(extractingPanel);
    }

    public void PressingCanvas()
    {
        ShowWorkCanvas();
        ShowOnlyWorkPanel(pressingPanel);

        if (pressMachine != null)
        {
            GraphiteData graphiteData = graphiteCollector != null ? graphiteCollector.currentGraphiteData : null;

            pressMachine.StartPressing(graphiteData);
        }
    }

    void ShowWorkCanvas()
    {
        inworkCanvas.SetActive(true);
        popupCanvas.SetActive(false);

        factoryImg.gameObject.SetActive(false);
        deskImg.gameObject.SetActive(true);
    }

    void ShowOnlyWorkPanel(GameObject activePanel)
    {
        if (sharpeningPanel != null) sharpeningPanel.SetActive(sharpeningPanel == activePanel);
        if (extractingPanel != null) extractingPanel.SetActive(extractingPanel == activePanel);
        if (pressingPanel != null) pressingPanel.SetActive(pressingPanel == activePanel);
    }

    // TODO: 추후 자동 도구 버튼 클릭 시 호출될 메서드
    public void OnAutoToolClicked()
    {

    }

}

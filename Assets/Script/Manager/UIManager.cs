using System;
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
    [SerializeField] Image WorkShopImg;
    [SerializeField] Image deskImg;
    [SerializeField] Image mainImg;
    [SerializeField] Image jewelShopImg;
    [SerializeField] Image openingImg;

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

        WorkShopImg.gameObject.SetActive(false);
        deskImg.gameObject.SetActive(false);
        mainImg.gameObject.SetActive(false);
    }

    public void StartWorkingCanvas()
    {
        StartSharpeningCanvas();
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

        WorkShopImg.gameObject.SetActive(true);
        deskImg.gameObject.SetActive(false);
    }

    public void ExtractingGraphiteCanvas()
    {
        ShowWorkCanvas();
        ShowOnlyWorkPanel(extractingPanel);
    }

    public void SettingTemperatureCanvas()
    {
        ShowWorkCanvas();
        ShowOnlyWorkPanel(pressingPanel);

        if (pressMachine != null)
        {
            GraphiteData graphiteData = graphiteCollector != null ? graphiteCollector.currentGraphiteData : null;

            pressMachine.StartPressing(graphiteData);
        }
    }

    public void ShowMainCanvas()
    {
        mainImg.gameObject.SetActive(true);
        mainCanvas.SetActive(true);

        popupCanvas.SetActive(false);
    }

    void ShowWorkCanvas()
    {
        inworkCanvas.SetActive(true);
        popupCanvas.SetActive(false);
        mainCanvas.SetActive(false);

        WorkShopImg.gameObject.SetActive(false);
        deskImg.gameObject.SetActive(true);
        mainImg.gameObject.SetActive(false);
    }

    void ShowOnlyWorkPanel(GameObject activePanel)
    {
        if (sharpeningPanel != null) sharpeningPanel.SetActive(sharpeningPanel == activePanel);
        if (extractingPanel != null) extractingPanel.SetActive(extractingPanel == activePanel);
        if (pressingPanel != null) pressingPanel.SetActive(pressingPanel == activePanel);
    }

    public void ShowJewelShop()
    {
        jewelShopImg.gameObject.SetActive(true);
    }

    // TODO: 추후 자동 도구 버튼 클릭 시 호출될 메서드
    public void OnAutoToolClicked()
    {

    }

}

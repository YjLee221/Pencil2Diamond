using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Canvas Objects")]
    [SerializeField] GameObject backgroundCanvas;
    [SerializeField] GameObject mainCanvas;
    [SerializeField] GameObject inworkCanvas;
    [SerializeField] GameObject popupCanvas;

    [Header("Background Image")]
    [SerializeField] Image factoryImg;
    [SerializeField] Image deskImg;

    [SerializeField] BaseTool selectedTool;  // 현재 유저가 장착한 도구 (다형성 활용)

    void Start()
    {
        ShowCanvas();
    }

    void ShowCanvas()
    {
        backgroundCanvas.SetActive(true);
        mainCanvas.SetActive(true);
        popupCanvas.SetActive(true);
        inworkCanvas.SetActive(false);
    }

    public void StartWorkingCanvas()
    {
        inworkCanvas.SetActive(true);
        popupCanvas.SetActive(false);

        factoryImg.gameObject.SetActive(false);
        deskImg.gameObject.SetActive(true);
    }

    public void EndWorkCanvas()
    {
        inworkCanvas.SetActive(false);
        popupCanvas.SetActive(true);

        factoryImg.gameObject.SetActive(true);
        deskImg.gameObject.SetActive(false);
    }

    // TODO: 추후 자동 도구 버튼 클릭 시 호출될 메서드
    public void OnAutoToolClicked()
    {

    }

}
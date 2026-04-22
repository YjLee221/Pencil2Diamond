using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Canvas Objects")]
    [SerializeField] GameObject backgroundCanvas;
    [SerializeField] GameObject mainCanvas;
    [SerializeField] GameObject inworkCanvas;
    [SerializeField] GameObject popupCanvas;

    [SerializeField] Pencil currentPencil;   // 씬에 있는 연필
    [SerializeField] BaseTool selectedTool;  // 현재 유저가 장착한 도구 (다형성 활용)
    [SerializeField] Button btnGoPress; // UI 버튼

    void Start()
    {
        ShowCanvas();

        if(currentPencil != null)
        {
            // 연필이 완전히 깎였을 때 버튼 활성화
            currentPencil.OnPencilCompleted += SetActiveButton;
        }

        if(btnGoPress != null)
        {
            btnGoPress.gameObject.SetActive(false); // 시작할 때 버튼 비활성화
        }
    }

    void ShowCanvas()
    {
        backgroundCanvas.SetActive(true);
        mainCanvas.SetActive(true);
        popupCanvas.SetActive(false);
        inworkCanvas.SetActive(false);
        popupCanvas.SetActive(false);
    }

    // 오브젝트가 파괴될 때 이벤트 구독 해제 (메모리 누수 방지)
    void OnDestroy()
    {
        if (currentPencil != null)
        {
            currentPencil.OnPencilCompleted -= SetActiveButton;
        }
    }

    // UI 버튼 이벤트나, 화면 터치 이벤트에서 이 함수를 호출합니다.
    public void OnUserAction()
    {
        if (selectedTool != null && currentPencil != null)
        {
            // 장착된 도구가 칼인지 연필깎이인지 매니저는 알 필요가 없습니다.
            // 알아서 각자의 TryUseTool 로직이 작동합니다.
            selectedTool.TryUseTool(currentPencil);
        }
    }

    public void SetActiveButton()
    {
        if (btnGoPress != null) 
        { 
            btnGoPress.gameObject.SetActive(true); 
        }
    }
}
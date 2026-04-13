using UnityEngine;

public class ToolManager : MonoBehaviour
{
    [SerializeField] Pencil currentPencil; // 씬에 있는 연필
    [SerializeField] BaseTool selectedTool;      // 현재 유저가 장착한 도구 (다형성 활용)

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
}
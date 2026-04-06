using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// DragHandler 인터페이스를 구현하여 드래그 이벤트를 처리하는 클래스
public class KnifeDragHandler : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    [SerializeField] RectTransform knifeTransform;
    [SerializeField] RectTransform hitAreaTransform;

    Image imgKnife;
    Vector2 knifeStartVec;

    float dragDistance = 0f; // 드래그 이동 거리 누적 변수
    readonly float cutThreshold = 30f; // 깎이는 판정 기준: 픽셀

    void Start()
    {
        if(knifeTransform == null) knifeTransform = GetComponent<RectTransform>();
        imgKnife = GetComponent<Image>();
        knifeStartVec = knifeTransform.anchoredPosition; // knife의 초기 위치 저장
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("칼이 클릭되었습니다.");
    }

    public void OnBeginDrag()
    {
        // 드래그 시작 시 칼의 색상을 밝게 변경하여 시각적 피드백 제공
        if (imgKnife != null) imgKnife.color = new Color(0.8f, 0.8f, 0.8f);

        imgKnife.raycastTarget = false; // 드래그 시작 시 raycastTarget을 false로 설정하여 다른 UI 요소와의 충돌 방지
        knifeTransform.SetAsLastSibling(); // 드래그 중인 칼이 다른 UI 요소보다 위에 있도록 설정
    }

    public void OnDrag(PointerEventData eventData)
    {
        
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)knifeTransform.parent, eventData.position, eventData.pressEventCamera, out Vector2 localPos))
        {
            knifeTransform.localPosition = localPos;
        }

        // 칼이 연필 깎아야하는 영역 안에 들어왔는지 실시간 검사
        if (RectTransformUtility.RectangleContainsScreenPoint(hitAreaTransform, eventData.position, eventData.pressEventCamera))
        {
            Debug.Log("칼이 깎아야하는 영역 안에 들어옴");
            // 이동 거리 누적
            dragDistance += eventData.delta.magnitude;

            // 칼이 타겟 영역에 들어왔을 때 시각적 피드백 제공
            if (imgKnife != null) imgKnife.color = new Color(0.5f, 1f, 0.5f); // 초록색으로 변경

            if(dragDistance >= cutThreshold)
            {
                // 깎이는 판정 기준을 충족했을 때 필요한 로직 실행 (예: 연필 깎기)
                Debug.Log("연필이 깎였습니다!");
                // 드래그 종료 후 초기 위치로 되돌리기
                //knifeTransform.anchoredPosition = knifeStartVec;
                dragDistance = 0f; // 이동 거리 초기화
            }
        }
        //else
        //{
        //    // 칼이 타겟 영역에서 벗어났을 때 원래 색상으로 복원
        //    if (imgKnife != null) imgKnife.color = new Color(0.8f, 0.8f, 0.8f); // 밝은 회색으로 변경
        //}
    }

    public void OnEndDrag()
    {
        // 드래그 종료 시 칼의 색상을 원래대로 복원
        if (imgKnife != null) imgKnife.color = Color.white;

        imgKnife.raycastTarget = true; // 드래그 종료 시 raycastTarget을 true로 설정하여 다른 UI 요소와의 충돌 허용
        knifeTransform.anchoredPosition = knifeStartVec; // 드래그 종료 후 초기 위치로 되돌리기

        dragDistance = 0f; // 이동 거리 초기화
    }
}

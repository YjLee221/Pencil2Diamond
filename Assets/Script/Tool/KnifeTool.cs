using UnityEngine;
using UnityEngine.EventSystems;

public class KnifeTool : BaseTool, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("드래그 세팅")]
    Vector2 _knifeOriginPosition;
    [SerializeField] RectTransform knifeTransform;
    [SerializeField] RectTransform knifeHitArea;

    [Header("거리 기반 타격 세팅")]
    [Tooltip("칼을 이 픽셀만큼 움직일 때마다 1번씩 깎입니다.")]
    [SerializeField] float distanceToShave = 30f;

    // 드래그 상태 관리 변수
    float _accumulatedDistance = 0f;
    Vector2 _lastPointerPosition;
    int _activePointId = -999;

    [SerializeField] GameObject pencilObject;

    IPencil _targetPencil;

    void Awake()
    {
        _knifeOriginPosition = knifeTransform.anchoredPosition;

        if(pencilObject != null)  _targetPencil = pencilObject.GetComponent<IPencil>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_activePointId != -999) return;

        _activePointId = eventData.pointerId;
        _lastPointerPosition = eventData.position;
        _accumulatedDistance = 0f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerId != _activePointId) return;

        if (_targetPencil == null || _targetPencil.CurrentHp <= 0) return;
        
        CalculateKnifePosition(eventData);

        while(_accumulatedDistance >= distanceToShave)
        {
            TryUseTool(_targetPencil);
            _accumulatedDistance -= distanceToShave;

            if(_targetPencil.CurrentHp <= 0)
            {
                OnEndDrag(eventData);
                break;
            }
        }
    }

    public void CalculateKnifePosition(PointerEventData eventData)
    {
        // 1. 칼의 위치를 드래그 위치로 업데이트
        Vector3 worldPos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(knifeTransform, eventData.position,
            eventData.pressEventCamera, out worldPos))
        {
            knifeTransform.position = worldPos;
        }

        // 2. 거리 누적 계산
        float moveDelta = Vector2.Distance(eventData.position, _lastPointerPosition);
        _accumulatedDistance += moveDelta;
        _lastPointerPosition = eventData.position;

        // 안전장치: Inspector에서 distanceToShave를 0 이하로 설정했을 때 무한 루프 도는 것 방지
        if (distanceToShave <= 0f) distanceToShave = 30f;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        knifeTransform.anchoredPosition = _knifeOriginPosition;
        _activePointId = -999;
    }

    public override void TryUseTool(IPencil targetPencil)
    {
        // 연필의 히트박스와 닿았는지 체크
        bool isHitWithPencil = IsHitAreaOverlapped(knifeHitArea, targetPencil.HitArea);
        if (isHitWithPencil)
        {
            targetPencil.TakeShaveDamage(shavePower);
        }

        if (targetPencil.CurrentHp <= 0)
        {
            OnEndDrag(null);
        }
    }

    // 칼의 히트 영역과 연필의 히트 영역이 겹치는지 체크하는 함수
    bool IsHitAreaOverlapped(RectTransform knifeRect, RectTransform targetRect)
    {
        Camera canvasCamera = targetRect.GetComponentInParent<Canvas>().worldCamera;
        Vector2 knifeBladeScreenPoint = RectTransformUtility.WorldToScreenPoint(canvasCamera, knifeRect.position);

        return RectTransformUtility.RectangleContainsScreenPoint(targetRect, knifeBladeScreenPoint, canvasCamera);
    }

    public void TakeShaveDamage(int damage)
    {
    }
}
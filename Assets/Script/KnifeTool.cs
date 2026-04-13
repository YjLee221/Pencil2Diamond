using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class KnifeTool : BaseTool, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("드래그 세팅")]
    RectTransform knifeOriginTransform; // 칼의 원래 위치를 저장할 변수
    [SerializeField] RectTransform knifeTransform; // 칼의 UI 요소
    [SerializeField] RectTransform knifeHitArea; // 칼날이 닿는 영역

    [Header("깎임 정도 세팅")]
    [SerializeField] float shaveCooltime = 0.1f; // 광클 방지용 쿨타임
    float lastShaveTime; // 마지막으로 깎은 시간 기록

    Pencil targetPencil;

    void Start()
    {
        knifeOriginTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData){ Debug.Log("칼 드래그 시작"); }

    public void OnDrag(PointerEventData eventData)
    {
        knifeTransform.position = eventData.position; // 칼이 마우스를 따라다니도록 위치 업데이트

        if (targetPencil == null) return;

        if(Time.time - lastShaveTime >= shaveCooltime)
        {
            TryUseTool(targetPencil);
            lastShaveTime = Time.time;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("칼 드래그 종료");
        knifeTransform = knifeOriginTransform; // 칼을 원래 위치로 되돌림
    }

    public override void TryUseTool(Pencil targetPencil)
    {
        bool isHitWithPencil = IsHitAreaOverlapped(knifeHitArea, targetPencil.pencilHitArea);

        if (isHitWithPencil)
        {
            targetPencil.TakeShaveDamage(shavePower);

            if (targetPencil.currentPencilHp <= 0) { OnEndDrag(null); }
        }
    }

    // hitArea 두 개가 겹치는지 체크하는 함수
    bool IsHitAreaOverlapped(RectTransform rect1, RectTransform rect2)
    {
        // 1. 두 UI의 실제 화면상 면적(Rect)을 계산해서 가져옵니다.
        Rect r1 = UIUtills.GetScreenRect(rect1);
        Rect r2 = UIUtills.GetScreenRect(rect2);

        // 2. 두 사각형이 겹치는지 확인합니다. (AABB 충돌 판정)
        return r1.Overlaps(r2);
    }   
}

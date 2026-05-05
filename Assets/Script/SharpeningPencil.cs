using System;
using UnityEngine;
using UnityEngine.UI;

public class SharpeningPencil : MonoBehaviour
{
    [SerializeField] public RectTransform pencilHitArea; // 연필이 깎일 영역
    public float currentPencilHp { get; private set; } // 외부에서의 수정이 불가능하도록 private set으로 설정

    Image pencilImageForHealth; // 연필의 HP에 따라 변경될 이미지 (Sprite Swap)
    PencilData currentPencilData; // 현재 작업 중인 연필 (ScriptableObject)

    // 이벤트 발생 시 Manager가 어떤 슬롯인지 알 수 있도록 자신을 인자로 전달
    public event Action<SharpeningPencil> OnPencilCompleted;

    void Awake()
    {
        pencilImageForHealth = GetComponent<Image>();
    }

    // 작업대의 빈 슬롯에 연필 데이터를 로드하는 매서드
    public void LoadPencil(PencilData newPencil)
    {
        currentPencilData = newPencil;
        currentPencilHp = newPencil.MaxPencilHp; // 연필의 최대 HP로 초기화

        pencilImageForHealth.enabled = true; // 연필 이미지 활성화

        UpdatePencilSprite();
    }

    // 연필 깎는 매서드
    public void TakeShaveDamage(float damage)
    {
        // 연필이 없는 상태이거나 이미 연필이 다 깎인 경우라면 연필을 더 깎지 않음
        if(currentPencilData == null || currentPencilHp <= 0) return;

        currentPencilHp -= damage;

        if (currentPencilHp <= 0)
        {
            currentPencilHp = 0;

            // 연필이 완전히 깎였을 때 이벤트 발생: 자기 자신에 대한 정보도 같이 넘겨줌
            OnPencilCompleted?.Invoke(this); 
           
            currentPencilData = null;

            pencilImageForHealth.enabled = false; // 연필 이미지 비활성화
        }

        // 연필의 HP 비율에 따라 스프라이트 교체
        else 
        { 
            UpdatePencilSprite(); 
        }
    }

    public void UpdatePencilSprite()
    {
        if (currentPencilData == null) return;

        float currentHPRatio = currentPencilHp / currentPencilData.MaxPencilHp;

        //if (currentHPRatio <= 0.5)
        //{
        //    pencilImageForHealth.sprite = currentPencilData.PencilStates[1].pencilSprite; // HP 50% 이하일 때 이미지 변경

        //    if (currentHPRatio <= 0) pencilImageForHealth.sprite = currentPencilData.PencilStates[2].pencilSprite; // HP 0% 이하일 때 이미지 변경
        //}

        foreach (var pencilState in currentPencilData.PencilStates)
        {
            if (currentHPRatio <= pencilState.remainingHpPercent)
            {
                pencilImageForHealth.sprite = pencilState.pencilSprite;
                break;
            }
        }
    }
}
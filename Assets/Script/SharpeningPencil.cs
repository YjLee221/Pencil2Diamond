using System;
using UnityEngine;
using UnityEngine.UI;

// 연필의 진행 단계
public enum PencilPhase
{
    None, // 아무 상태도 아님
    ReadyToSharpen, // 연필이 깎일 준비가 된 상태
    Sharpening, // 깎이는 중
    ReadyToExtract, // 깎인 상태로, 추출할 준비가 된 상태
    Extracting, // 추출 중
    Completed // 완성된 상태
}

public class SharpeningPencil : MonoBehaviour
{
    [SerializeField] public RectTransform pencilHitArea; // 연필이 깎일 영역
    public float currentPencilHp { get; private set; } // 외부에서의 수정이 불가능하도록 private set으로 설정
    public float currentGraphiteHp { get; private set; } // 연필심의 HP (추출 단계에서 사용)
    public PencilPhase currentPencilPhase { get; private set; } // 연필의 현재 진행 단계

    Image pencilImageForHealth; // 연필의 HP에 따라 변경될 이미지 (Sprite Swap)
    PencilData currentPencilData; // 현재 작업 중인 연필 (ScriptableObject)

    [SerializeField] UIManager uiManager; // UIManager 참조 (추후 작업 진행 단계에 따라 UI 변경을 위해 필요)

    // 이벤트 발생 시 Manager가 어떤 슬롯인지 알 수 있도록 자신을 인자로 전달
    public event Action<SharpeningPencil> OnPencilSharpeningCompleted; // 연필의 나무부분이 완전히 깎였을 때
    public event Action<SharpeningPencil> OnPencilExtractingCompleted; // 연필심이 완전히 추출되었을 때

    void Awake()
    {
        pencilImageForHealth = GetComponent<Image>();
    }

    // 작업대의 빈 슬롯에 연필 데이터를 로드하는 매서드
    public void LoadPencil(PencilData newPencil)
    {
        currentPencilData = newPencil;
        currentPencilHp = newPencil.MaxPencilHp; // 연필의 최대 HP로 초기화
        currentGraphiteHp = newPencil.MaxGraphiteHp; // 연필심의 최대 HP로 초기화
        currentPencilPhase = PencilPhase.ReadyToSharpen; // 연필이 깎일 준비가 된 상태로 설정

        pencilImageForHealth.enabled = true; // 연필 이미지 활성화

        UpdatePencilSprite();
    }

    public void UnlockSharpening()
    {
        if(currentPencilPhase == PencilPhase.ReadyToSharpen)
        {
            currentPencilPhase = PencilPhase.Sharpening; // 깎이는 중으로 상태 변경
        }
    }

    // 연필 깎는 매서드
    public void TakeShaveDamage(float damage)
    {
        // 연필이 없는 상태이거나 이미 연필의 나무 부분이 완전히 깎인 상태라면 더 이상 데미지를 입힐 수 없음
        if (currentPencilData == null || currentPencilHp <= 0) return;

        currentPencilHp -= damage;

        if (currentPencilHp <= 0)
        {
            currentPencilHp = 0;
            currentPencilPhase += 1;

            // 연필이 완전히 깎였을 때 이벤트 발생: 자기 자신에 대한 정보도 같이 넘겨줌
            OnPencilSharpeningCompleted?.Invoke(this); 
           
            currentPencilData = null;
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

        if (currentHPRatio <= 0.5)
        {
            pencilImageForHealth.sprite = currentPencilData.PencilStates[1].pencilSprite; // HP 50% 이하일 때 이미지 변경

            if (currentHPRatio <= 0) pencilImageForHealth.sprite = currentPencilData.PencilStates[2].pencilSprite; // HP 0% 이하일 때 이미지 변경
        }
    }

    public void ExtractGraphite()
    {
        if(currentPencilPhase != PencilPhase.ReadyToExtract) return;

        currentPencilPhase = PencilPhase.Extracting; // 추출 중으로 상태 변경

        uiManager.ExtractingGraphiteCanvas(); // UI 매니저에게 추출 단계로 UI 변경 요청

        OnPencilExtractingCompleted?.Invoke(this);

        currentPencilData = null; // 연필 데이터 초기화
    }
}
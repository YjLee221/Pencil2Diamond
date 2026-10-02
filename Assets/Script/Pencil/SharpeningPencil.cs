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

public class SharpeningPencil : MonoBehaviour, IPencil
{
    [SerializeField] public RectTransform pencilHitArea; // 연필이 깎일 영역
    public int CurrentPencilHp { get; private set; } // 외부에서의 수정이 불가능하도록 private set으로 설정
    public int CurrentGraphiteHp { get; private set; } // 연필심의 HP (추출 단계에서 사용)
    public PencilPhase CurrentPencilPhase { get; private set; } // 연필의 현재 진행 단계

    public int CurrentHp => CurrentPencilHp;

    public RectTransform HitArea => pencilHitArea;

    Image _pencilImageForHealth; // 연필의 HP에 따라 변경될 이미지 (Sprite Swap)
    public PencilData CurrentPencilData { get; private set; } // 현재 작업 중인 연필 (ScriptableObject)

    // 이벤트 발생 시 Manager가 어떤 슬롯인지 알 수 있도록 자신을 인자로 전달
    public static event Action<SharpeningPencil> OnPencilSharpeningCompleted; // 연필의 나무부분이 완전히 깎였을 때
    
    void Awake()
    {
        _pencilImageForHealth = GetComponent<Image>();
    }

    // 작업대의 빈 슬롯에 연필 데이터를 로드하는 매서드
    public void LoadPencil(PencilData newPencil)
    {
        CurrentPencilData = newPencil;
        CurrentPencilHp = newPencil.MaxPencilHp; // 연필의 최대 HP로 초기화
        CurrentGraphiteHp = newPencil.MaxGraphiteHp; // 연필심의 최대 HP로 초기화
        CurrentPencilPhase = PencilPhase.ReadyToSharpen; // 연필이 깎일 준비가 된 상태로 설정

        if (_pencilImageForHealth == null) _pencilImageForHealth = GetComponent<Image>();

        _pencilImageForHealth.enabled = true; // 연필 이미지 활성화

        UpdatePencilSprite();
    }

    public void UnlockSharpening()
    {
        if(CurrentPencilPhase == PencilPhase.ReadyToSharpen) CurrentPencilPhase = PencilPhase.Sharpening;
    }

    // 연필 깎는 매서드
    public void TakeShaveDamage(int damage)
    {
        // 연필이 없는 상태이거나 이미 연필의 나무 부분이 완전히 깎인 상태라면 더 이상 데미지를 입힐 수 없음
        if (CurrentPencilData == null || CurrentPencilHp <= 0) return;

        CurrentPencilHp -= damage;

        if (CurrentPencilHp <= 0)
        {
            CurrentPencilHp = 0;
            CurrentPencilPhase = PencilPhase.ReadyToExtract;

            // 연필이 완전히 깎였을 때 이벤트 발생: 자기 자신에 대한 정보도 같이 넘겨줌
            OnPencilSharpeningCompleted?.Invoke(this); 
           
            //currentPencilData = null;
        }

        // 연필의 HP 비율에 따라 스프라이트 교체
        else 
        { 
            UpdatePencilSprite(); 
        }
    }

    public void UpdatePencilSprite()
    {
        if (CurrentPencilData == null) return;

        float currentHpRatio = (float)CurrentPencilHp / CurrentPencilData.MaxPencilHp;
        if (currentHpRatio <= 0.5f)
        {
            _pencilImageForHealth.sprite = CurrentPencilData.PencilStates[1].pencilSprite; // HP 50% 이하일 때 이미지 변경

            if (currentHpRatio <= 0) _pencilImageForHealth.sprite = CurrentPencilData.PencilStates[2].pencilSprite; // HP 0% 이하일 때 이미지 변경
        }
    }
}
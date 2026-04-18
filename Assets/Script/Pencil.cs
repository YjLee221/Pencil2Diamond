using System;
using UnityEngine;
using UnityEngine.UI;

public class Pencil : MonoBehaviour
{
    [SerializeField] public RectTransform pencilHitArea; // 연필이 깎일 영역
    public float currentPencilHp { get; private set; } // 외부에서의 수정이 불가능하도록 private set으로 설정

    Image pencilImageForHealth; // 연필의 HP에 따라 변경될 이미지 (Sprite Swap)
    [SerializeField] PencilData pencilData; // 연필의 데이터 (ScriptableObject)

    public event Action OnPencilCompleted; // 연필이 완전히 깎였을 때 발생하는 이벤트

    void Start()
    {
        if(pencilData != null) { currentPencilHp = pencilData.MaxPencilHp; }
        
        pencilImageForHealth = GetComponent<Image>();
    }

    // 연필 깎는 매서드
    public void TakeShaveDamage(float damage)
    {
        if(currentPencilHp <= 0) return; // 이미 연필이 다 깎인 경우 추가 데미지 무시

        currentPencilHp -= damage;
        //Debug.Log($"연필이 깎임! 남은 내구도: {currentPencilHp}");

        // 여기에 이전 답변의 스프라이트 변경(Sprite Swap) 로직이 들어갑니다.
        if (currentPencilHp <= 0)
        {
            //Debug.Log("연필 다 깎음! 흑연 생성!");
            currentPencilHp = 0;

            // 연필이 완전히 깎였을 때 이벤트 발생
            OnPencilCompleted?.Invoke(); 
        }

        // 연필의 HP 비율에 따라 스프라이트 교체
        UpdatePencilSprite();
    }

    public void UpdatePencilSprite()
    {
        if (pencilData == null) return;

        float currentHPRatio = currentPencilHp / pencilData.MaxPencilHp;

        if (currentHPRatio <= 0.5)
        {
            pencilImageForHealth.sprite = pencilData.PencilStates[1].pencilSprite; // HP 50% 이하일 때 이미지 변경

            if(currentHPRatio <= 0) pencilImageForHealth.sprite = pencilData.PencilStates[2].pencilSprite; // HP 0% 이하일 때 이미지 변경
        }
    }
}
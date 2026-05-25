using System;
using UnityEngine;
using UnityEngine.UI;

public class PencilCollectedGraphite : MonoBehaviour, IPencil
{
    public PencilData currentPencilData;
    public int currentGraphiteHp;
    public PencilPhase currentPencilPhase { get; private set; }

    public int CurrentHP => currentGraphiteHp;

    [SerializeField] public RectTransform graphiteHitArea;

    public RectTransform HitArea => graphiteHitArea;

    Image graphiteImageForHealth;

    public GraphiteData currentGraphiteData { get; private set; }
    public event Action<PencilCollectedGraphite> OnGraphiteExtractionCompleted;

    public void StartExtractingPhase(PencilData pencilData, GraphiteData graphiteData, int graphiteHP)
    {
        currentPencilData = pencilData;
        currentGraphiteData = graphiteData;
        currentGraphiteHp = graphiteHP;
        currentPencilPhase = PencilPhase.Extracting;
    }

    public void TakeShaveDamage(int damage)
    {
        if(currentGraphiteData == null || currentGraphiteHp <= 0) return;

        currentGraphiteHp -= damage;

        if (currentGraphiteHp <= 0)
        {
            currentGraphiteHp = 0;

            OnGraphiteExtractionCompleted?.Invoke(this);
        }
        else
        {
            UpdateGraphiteSprite();
        }
    }

    public void UpdateGraphiteSprite()
    {
        if (currentGraphiteData == null) return;

        float currentHPRatio = (float)currentGraphiteHp / currentPencilData.MaxGraphiteHp;
        Sprite currentSprite = currentGraphiteData.GraphiteStates[0].graphiteSprite;

        // 현재 HP 비율에 따라 그래파이트의 스프라이트를 교체하는 로직
        // 그래파이트의 HP 단계별로 설정된 스프라이트 배열을 순회하면서, 현재 HP 비율이 해당 단계의 남은 HP 퍼센트 이하인 경우 해당 스프라이트로 교체
        // 예시: 그래파이트의 HP가 70% 이하일 때 첫 번째 스프라이트, 40% 이하일 때 두 번째 스프라이트, 10% 이하일 때 세 번째 스프라이트로 교체
        

        if (graphiteImageForHealth != null)
        {
            graphiteImageForHealth.sprite = currentSprite;
        }
    }
}
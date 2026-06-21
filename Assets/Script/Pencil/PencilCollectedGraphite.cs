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

    public GraphiteData currentGraphiteData { get; private set; }
    public static event Action<PencilCollectedGraphite> OnGraphiteExtractionCompleted;

    [SerializeField] Image imgSharpenedPencil;

    public void StartExtractingPhase(PencilData pencilData, GraphiteData graphiteData, int graphiteHP)
    {
        currentPencilData = pencilData;
        currentGraphiteData = graphiteData;
        currentGraphiteHp = graphiteHP;
        currentPencilPhase = PencilPhase.Extracting;

        UpdateGraphiteSprite();
    }

    public void TakeShaveDamage(int damage)
    {
        if (currentGraphiteData == null || currentGraphiteHp <= 0) return;

        currentGraphiteHp -= damage;

        if (currentGraphiteHp <= 0)
        {
            currentGraphiteHp = 0;
            UpdateGraphiteSprite();
            OnGraphiteExtractionCompleted?.Invoke(this);
        }
        else
        {
            UpdateGraphiteSprite();
        }
    }

    public void UpdateGraphiteSprite()
    {
        if (currentGraphiteData == null || currentGraphiteData.ImgGraphiteStates == null || currentGraphiteData.ImgGraphiteStates.Length == 0) return;

        float currentHPRatio = (float)currentGraphiteHp / currentPencilData.MaxGraphiteHp;

        Sprite targetSprite = currentGraphiteData.ImgGraphiteStates[0].graphiteSprite;

        for (int i = 0; i < currentGraphiteData.ImgGraphiteStates.Length; i++)
        {
            float statePercent = currentGraphiteData.ImgGraphiteStates[i].remainingHpPercent;
            if (currentHPRatio <= statePercent)
            {                
                targetSprite = currentGraphiteData.ImgGraphiteStates[i].graphiteSprite;
            }
        }

        imgSharpenedPencil.sprite = targetSprite;
    }
}
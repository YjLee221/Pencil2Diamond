using System;
using UnityEngine;
using UnityEngine.UI;

public class PencilCollectedGraphite : MonoBehaviour, IPencil
{
    public PencilData currentPencilData;
    public int currentGraphiteHp;
    public PencilPhase CurrentPencilPhase { get; private set; }

    public int CurrentHp => currentGraphiteHp;

    [SerializeField] public RectTransform graphiteHitArea;
    public RectTransform HitArea => graphiteHitArea;

    public GraphiteData CurrentGraphiteData { get; private set; }
    public static event Action<PencilCollectedGraphite> OnGraphiteExtractionCompleted;

    [SerializeField] Image imgSharpenedPencil;

    public void StartExtractingPhase(PencilData pencilData, GraphiteData graphiteData, int graphiteHp)
    {
        currentPencilData = pencilData;
        CurrentGraphiteData = graphiteData;
        currentGraphiteHp = graphiteHp;
        CurrentPencilPhase = PencilPhase.Extracting;

        UpdateGraphiteSprite();
    }

    public void TakeShaveDamage(int damage)
    {
        if (CurrentGraphiteData == null || currentGraphiteHp <= 0) return;

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
        if (CurrentGraphiteData == null || CurrentGraphiteData.ImgGraphiteStates == null || CurrentGraphiteData.ImgGraphiteStates.Length == 0) return;

        float currentHpRatio = (float)currentGraphiteHp / currentPencilData.MaxGraphiteHp;

        Sprite targetSprite = CurrentGraphiteData.ImgGraphiteStates[0].graphiteSprite;

        for (int i = 0; i < CurrentGraphiteData.ImgGraphiteStates.Length; i++)
        {
            float statePercent = CurrentGraphiteData.ImgGraphiteStates[i].remainingHpPercent;
            if (currentHpRatio <= statePercent)
            {                
                targetSprite = CurrentGraphiteData.ImgGraphiteStates[i].graphiteSprite;
            }
        }

        imgSharpenedPencil.sprite = targetSprite;
    }
}
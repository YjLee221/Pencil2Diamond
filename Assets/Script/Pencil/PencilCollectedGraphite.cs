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

    public void StartExtractingPhase(PencilData pencilData, int graphiteHP)
    {
        currentPencilData = pencilData;
        currentGraphiteHp = graphiteHP;
        currentPencilPhase = PencilPhase.Extracting;
    }

    public void TakeShaveDamage(int damage)
    {
        if(currentGraphiteData == null || currentGraphiteHp <= 0) return;

        if(currentGraphiteHp <= 0)
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

        float currentHPRatio = currentGraphiteHp / currentPencilData.MaxGraphiteHp;

        if(currentHPRatio < 1)
        {
            if (currentHPRatio <= 0.75f)
            {
                graphiteImageForHealth.sprite = currentGraphiteData.GraphiteStates[1].graphiteSprite;
            }
            else if (currentHPRatio <= 0.5f)
            {
                graphiteImageForHealth.sprite = currentGraphiteData.GraphiteStates[2].graphiteSprite;
            }
            else if (currentHPRatio <= 0.25f)
            {
                graphiteImageForHealth.sprite = currentGraphiteData.GraphiteStates[3].graphiteSprite;
            }
            else
            {
                graphiteImageForHealth.sprite = currentGraphiteData.GraphiteStates[4].graphiteSprite;
            }
        }
    }
}
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PressMachine : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Dial")]
    [SerializeField] RectTransform dialTouchArea;
    //[SerializeField] RectTransform dialVisual;
    [SerializeField] float minDialDegree = -135f;
    [SerializeField] float maxDialDegree = 135f;

    [Header("Temperature")]
    [SerializeField] TextMeshProUGUI userSettingTemperature;
    [SerializeField] TextMeshProUGUI goalTemperature;
    [SerializeField] int minTemperature = 0;
    [SerializeField] int maxTemperature = 2400;
    [SerializeField] int temperatureStep = 50;
    [SerializeField] int successTolerance = 0;

    int currentTemperature;
    int targetTemperature;
    float currentDialDegree;

    int activePointerId = int.MinValue;
    float startPointerAngle;
    float startDialDegree;

    void Awake()
    {
        if (dialTouchArea == null) dialTouchArea = transform as RectTransform;

        ResetTemperature();
        UpdateTemperatureUI();
        UpdateDialVisual();
    }

    public void StartPressing(GraphiteData graphiteData)
    {
        if (graphiteData == null)
        {
            Debug.LogWarning("PressMachine.StartPressing was called without GraphiteData.");
            targetTemperature = minTemperature;
        }
        else
        {
            targetTemperature = graphiteData.TargetPressTemperature;
        }

        ResetTemperature();
        UpdateTemperatureUI();
        UpdateDialVisual();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (activePointerId != int.MinValue) return;
        if (dialTouchArea == null) return;

        activePointerId = eventData.pointerId;
        startPointerAngle = GetPointerAngle(eventData);
        startDialDegree = currentDialDegree;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerId != activePointerId) return;
        if (dialTouchArea == null) return;

        float pointerAngle = GetPointerAngle(eventData);
        float deltaAngle = Mathf.DeltaAngle(startPointerAngle, pointerAngle);

        SetDialDegree(startDialDegree + deltaAngle);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData != null && eventData.pointerId != activePointerId) return;

        activePointerId = int.MinValue;
    }

    float GetPointerAngle(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(dialTouchArea, eventData.position,
            eventData.pressEventCamera, out Vector2 localPoint
        );

        return Mathf.Atan2(localPoint.y, localPoint.x) * Mathf.Rad2Deg;
    }

    void SetDialDegree(float degree)
    {
        currentDialDegree = Mathf.Clamp(degree, minDialDegree, maxDialDegree);

        float ratio = Mathf.InverseLerp(minDialDegree, maxDialDegree, currentDialDegree);
        int temperature = Mathf.RoundToInt(Mathf.Lerp(minTemperature, maxTemperature, ratio));

        if (temperatureStep > 1) temperature = Mathf.RoundToInt(temperature / (float)temperatureStep) * temperatureStep;

        currentTemperature = Mathf.Clamp(temperature, minTemperature, maxTemperature);

        UpdateTemperatureUI();
        UpdateDialVisual();
    }

    void ResetTemperature()
    {
        currentTemperature = minTemperature;
        currentDialDegree = minDialDegree;
    }

    void UpdateTemperatureUI()
    {
        if (userSettingTemperature != null)
        {
            userSettingTemperature.text = currentTemperature.ToString();
        }

        if (goalTemperature != null)
        {
            goalTemperature.text = targetTemperature.ToString();
        }
    }

    void UpdateDialVisual()
    {
        dialTouchArea.localRotation = Quaternion.Euler(0f, 0f, currentDialDegree);
    }

    public bool IsCorrectTemperature()
    {
        return Mathf.Abs(currentTemperature - targetTemperature) <= successTolerance;
    }
}

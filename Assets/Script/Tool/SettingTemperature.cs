using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingTemperature : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Dial")]
    [SerializeField] RectTransform dialTouchArea;
    [SerializeField] float minDialDegree = -135f;
    [SerializeField] float maxDialDegree = 135f;

    [Header("Temperature")]
    [SerializeField] TextMeshProUGUI userSettingTemperature;
    [SerializeField] TextMeshProUGUI goalTemperature;
    [SerializeField] int minTemperature = 0;
    [SerializeField] int maxTemperature = 2400;
    [SerializeField] int temperatureStep = 50;
    [SerializeField] int successTolerance = 0;

    [Header("Confirm")]
    [SerializeField] Button confirmBtn;

    int _currentTemperature;
    int _targetTemperature;
    float _currentDialDegree;

    int _activePointerId = int.MinValue;
    float _previousPointerAngle;

    public static event Action<bool> OnMatchingTemperatureCompleted;

    void Awake()
    {
        if (dialTouchArea == null) dialTouchArea = transform as RectTransform;

        ResetTemperature();
        UpdateTemperatureUI();
        UpdateDialVisual();
    }

    void Start()
    {
        if (confirmBtn != null)
        {
            confirmBtn.onClick.AddListener(OnConfirmButtonClicked);
        }
    }

    private void OnConfirmButtonClicked()
    {
        bool checkTemperature = Mathf.Abs(_currentTemperature - _targetTemperature) <= successTolerance;
        if (checkTemperature)
        {
            Debug.Log("성공! 온도 맞추기 성공~");
            OnMatchingTemperatureCompleted?.Invoke(true);
        }
        else
        {
            Debug.Log("실패! 온도 맞추기 실패~");
            OnMatchingTemperatureCompleted?.Invoke(false);
        }
    }

    public void StartPressing(GraphiteData graphiteData)
    {
        if (graphiteData == null)
        {
            _targetTemperature = minTemperature;
        }
        else
        {
            _targetTemperature = graphiteData.TargetPressTemperature;
        }

        ResetTemperature();
        UpdateTemperatureUI();
        UpdateDialVisual();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_activePointerId != int.MinValue) return;
        if (dialTouchArea == null) return;

        _activePointerId = eventData.pointerId;
        _previousPointerAngle = GetPointerAngle(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerId != _activePointerId) return;
        if (dialTouchArea == null) return;

        float pointerAngle = GetPointerAngle(eventData);
        float deltaAngle = Mathf.DeltaAngle(_previousPointerAngle, pointerAngle);

        SetDialDegree(_currentDialDegree + deltaAngle);
        _previousPointerAngle = pointerAngle;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData != null && eventData.pointerId != _activePointerId) return;

        _activePointerId = int.MinValue;
    }

    float GetPointerAngle(PointerEventData eventData)
    {
        Vector2 centerPoint = RectTransformUtility.WorldToScreenPoint(eventData.pressEventCamera, dialTouchArea.position);
        Vector2 pointerDirection = eventData.position - centerPoint;

        if (pointerDirection.sqrMagnitude <= Mathf.Epsilon) return _previousPointerAngle;

        return Mathf.Atan2(pointerDirection.y, pointerDirection.x) * -Mathf.Rad2Deg;
    }

    void SetDialDegree(float degree)
    {
        _currentDialDegree = Mathf.Clamp(degree, minDialDegree, maxDialDegree);

        float ratio = Mathf.InverseLerp(minDialDegree, maxDialDegree, _currentDialDegree);
        int temperature = Mathf.RoundToInt(Mathf.Lerp(minTemperature, maxTemperature, ratio));

        if (temperatureStep > 1) temperature = Mathf.RoundToInt(temperature / (float)temperatureStep) * temperatureStep;

        _currentTemperature = Mathf.Clamp(temperature, minTemperature, maxTemperature);

        UpdateTemperatureUI();
        UpdateDialVisual();
    }

    void ResetTemperature()
    {
        // currentTemperature = minTemperature;
        // currentDialDegree = minDialDegree;
    }

    void UpdateTemperatureUI()
    {
        if (userSettingTemperature != null)
        {
            userSettingTemperature.text = _currentTemperature.ToString();
        }

        if (goalTemperature != null)
        {
            goalTemperature.text = _targetTemperature.ToString();
        }
    }

    void UpdateDialVisual()
    {
        dialTouchArea.localRotation = Quaternion.Euler(0f, 0f, _currentDialDegree);
    }
}

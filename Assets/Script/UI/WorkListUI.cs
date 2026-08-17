using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorkListUI : MonoBehaviour
{
    // [SerializeField] MainWorkshopUI mainWorkshopUI;
    
    [Header("AbleWorkingList")]
    [SerializeField] GameObject workingPanel;
    [SerializeField] TextMeshProUGUI workingPanelContents;
    [SerializeField] Button startWorkingBtn;
    
    [SerializeField] TextMeshProUGUI workingAbleAmountText;
    int _workingAbleAmount = 1;
    [SerializeField] int workingAbleAmountMax;
    
    [SerializeField] Button plusButton;
    [SerializeField] Button minusButton;
    // [SerializeField] Button maxButton;

    [SerializeField] TextMeshProUGUI warningText;
    [SerializeField] float displayDuration = 1.5f;

    bool _isWorkingPanelClosed;
    Coroutine _hideCoroutine;
    
    WorkingStep _checkWorkStep = WorkingStep.None;
    
    public event Action<WorkingStep, int> OnStartWorkButtonClickedEvent;

    void Awake()
    {
        warningText.gameObject.SetActive(false);
    }

    void Start()
    {
        workingAbleAmountText.text = _workingAbleAmount.ToString();
        
        startWorkingBtn.onClick.AddListener(OnStartWorkButtonClicked);
        plusButton.onClick.AddListener(OnAbleWorkingAmountPlusButtonClicked);
        minusButton.onClick.AddListener(OnAbleWorkingAmountMinusButtonClicked);
    }

    void OnDisable()
    {
        if (_hideCoroutine != null)
        {
            StopCoroutine(_hideCoroutine);
            _hideCoroutine = null;
        }

        warningText.gameObject.SetActive(false);
        startWorkingBtn.interactable = true;
    }
    
    void OnStartWorkButtonClicked()
    {
        OnStartWorkButtonClickedEvent?.Invoke(_checkWorkStep, _workingAbleAmount);
    }

    void OnAbleWorkingAmountMinusButtonClicked()
    {
        _workingAbleAmount = Mathf.Clamp(_workingAbleAmount - 1, 1, workingAbleAmountMax);
        workingAbleAmountText.text = _workingAbleAmount.ToString();
    }

    void OnAbleWorkingAmountPlusButtonClicked()
    {
        _workingAbleAmount = Mathf.Clamp(_workingAbleAmount + 1, 1, workingAbleAmountMax);
        workingAbleAmountText.text = _workingAbleAmount.ToString();
    }
    
    public void ShowWorkAbleList(WorkListViewData workData)
    {
        if (workingPanel.activeSelf && _checkWorkStep == workData.WorkingStep)
        {
            CloseWorkAbleList();
            return;
        }

        _checkWorkStep = workData.WorkingStep;
        workingPanel.SetActive(true);

        startWorkingBtn.interactable = workData.AvailableAmount > 0;
        
        switch (_checkWorkStep)
        {
            case WorkingStep.Sharpening:
                workingPanelContents.text = $"작업할 연필 종류: 2B \n" +
                                            $"보유한 연필: {workData.AvailableAmount} 개 \n\n " +
                                            $"[ 가공할 수량 ]\n";
                break;
            
            case WorkingStep.CollectingGraphite:
                workingPanelContents.text = $"보유한 흑연: {workData.AvailableAmount}";
                break;
            
            case WorkingStep.Pressing:
                workingPanelContents.text = $"현재 압축기 레벨 {workData.PressMachineLevel}\n" +
                                            $"보유한 흑연: {workData.AvailableAmount} 개 \n\n" +
                                            $"[ 가공할 수량 ]\n";
                break;
        }
    }

    void CloseWorkAbleList()
    {
        workingPanel.SetActive(false);
        _checkWorkStep = WorkingStep.None;
    }

    void ShowWarningMessage(string warningMessage)
    {
        if (_hideCoroutine != null)
        {
            StopCoroutine(_hideCoroutine);
        }

        warningText.text = warningMessage;
        warningText.gameObject.SetActive(true);
        startWorkingBtn.interactable = false;

        _hideCoroutine = StartCoroutine(HideAfterDelay());
    }

    IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);

        startWorkingBtn.interactable = true;
        warningText.gameObject.SetActive(false);
        _hideCoroutine = null;
    }
}

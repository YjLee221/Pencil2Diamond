using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class WorkListUI : MonoBehaviour
{
    [Header("AbleWorkingList")]
    [SerializeField] GameObject workingPanel;
    [SerializeField] TextMeshProUGUI workingPanelContents;
    [SerializeField] QuantitySettingUI quantitySettingUI;

    [SerializeField] TextMeshProUGUI warningText;
    [SerializeField] float displayDuration = 1.5f;

    Coroutine hideCoroutine;
    WorkingStep checkWorkStep = WorkingStep.None;

    public event Action<WorkingStep, int> OnStartWorkButtonClickedEvent;

    void Awake()
    {
        warningText.gameObject.SetActive(false);
    }

    void OnEnable()
    {
        quantitySettingUI.OnActionButtonClickedEvent += HandleQuantityConfirmed;
    }

    void OnDisable()
    {
        quantitySettingUI.OnActionButtonClickedEvent -= HandleQuantityConfirmed;

        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
            hideCoroutine = null;
        }

        warningText.gameObject.SetActive(false);
    }

    void HandleQuantityConfirmed(int amount)
    {
        OnStartWorkButtonClickedEvent?.Invoke(checkWorkStep, amount);
    }

    public void ShowWorkAbleList(WorkListViewData workData)
    {
        if (workingPanel.activeSelf && checkWorkStep == workData.WorkingStep)
        {
            CloseWorkAbleList();
            return;
        }

        checkWorkStep = workData.WorkingStep;
        workingPanel.SetActive(true);

        quantitySettingUI.Configure(
            workData.MaxSelectableAmount,
            1,
            "작업하기",
            workData.AvailableAmount > 0);

        switch (checkWorkStep)
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
        checkWorkStep = WorkingStep.None;
    }

    void ShowWarningMessage(string warningMessage)
    {
        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
        }

        warningText.text = warningMessage;
        warningText.gameObject.SetActive(true);
        quantitySettingUI.SetActionInteractable(false);

        hideCoroutine = StartCoroutine(HideAfterDelay());
    }

    IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);

        quantitySettingUI.SetActionInteractable(true);
        warningText.gameObject.SetActive(false);
        hideCoroutine = null;
    }
}

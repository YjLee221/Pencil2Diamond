using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorkListUI : MonoBehaviour
{
    [Header("AbleWorkingList")]
    [SerializeField] GameObject workingPanel;
    [SerializeField] TextMeshProUGUI workingPanelContents;
    [SerializeField] Button startWorkingBtn;
    [SerializeField] QuantitySettingUI quantitySettingUI;

    [SerializeField] TextMeshProUGUI warningText;
    [SerializeField] float displayDuration = 1.5f;

    Coroutine hideCoroutine;
    WorkingStep checkWorkStep = WorkingStep.None;

    public event Action<WorkingStep, int> OnStartWorkingButtonClickedEvent;

    void Awake()
    {
        startWorkingBtn.onClick.AddListener(OnStartWorkingButtonClicked);
        warningText.gameObject.SetActive(false);
    }

    void OnStartWorkingButtonClicked()
    {
        if(startWorkingBtn.interactable)
            OnStartWorkingButtonClickedEvent?.Invoke(checkWorkStep, quantitySettingUI.CurrentQuantity);
    }

    void OnDisable()
    {
        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
            hideCoroutine = null;
        }

        warningText.gameObject.SetActive(false);
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
            1);
        
        startWorkingBtn.interactable = workData.MaxSelectableAmount > 0;

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
        
        startWorkingBtn.interactable = false;

        hideCoroutine = StartCoroutine(HideAfterDelay());
    }

    IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);

        startWorkingBtn.interactable = true;
        warningText.gameObject.SetActive(false);
        hideCoroutine = null;
    }
}

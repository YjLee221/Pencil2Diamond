using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI Item")]
    [SerializeField] Button btnKnife;
    [SerializeField] Button btnPencilCutter;

    [Header("UI Text")]
    [SerializeField] TextMeshProUGUI uiText;
    [SerializeField] Button btnConfirm;
    [SerializeField] Button btnCancel;

    public void Start()
    {
        btnConfirm.gameObject.SetActive(false);
        btnCancel.gameObject.SetActive(false);
    }

    public void OnClickItem(Button clickedButton)
    {
        // 클릭된 버튼이 아닌 다른 아이템 버튼을 비활성화
        Button otherButton = clickedButton == btnKnife ? btnPencilCutter : btnKnife;
        otherButton.gameObject.SetActive(false);
        
        OnClickTool();
    }

    void OnClickTool()
    {
        uiText.gameObject.SetActive(false);
        btnConfirm.gameObject.SetActive(true);
        btnCancel.gameObject.SetActive(true);
    }

    public void OnClickConfirm()
    {

    }

    public void OnClickCancel()
    {
        btnConfirm.gameObject.SetActive(false);
        btnCancel.gameObject.SetActive(false);

        uiText.gameObject.SetActive(true);
        btnKnife.gameObject.SetActive(true);
        btnPencilCutter.gameObject.SetActive(true);
    }
}

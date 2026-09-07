using System;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainMarketUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Button closeButton;
    [SerializeField] GameObject mainMarketPanel;
    [SerializeField] GameObject setAmountPanel;

    [Header("Buttons")] 
    [SerializeField] Button itemButton;

    [SerializeField] PencilManager pencilManager;

    public event Action OnItemButtonClickedEvent;

    void Start()
    {
        setAmountPanel.SetActive(false);
        
        SettingItemButtons();
        
        itemButton.onClick.AddListener(OnClickedItemButton);
        closeButton.onClick.AddListener(OnClickedCloseButton);
    }
    
    void OnClickedItemButton()
    {
        OnItemButtonClickedEvent?.Invoke();
    }
        
    void OnClickedCloseButton()
    {
        
        if(setAmountPanel.activeSelf) setAmountPanel.SetActive(false);
        else mainMarketPanel.SetActive(false);
    }

    void SettingItemButtons()
    {
        // 기존 아이템 버튼 복제용으로만 사용
        itemButton.gameObject.SetActive(false);
        
        // 튜토리얼 연필과 MaxPencilType을 제외한 나머지 연필로 생성
        for(int i = (int)PencilType.TwoBPencil; i < (int)PencilType.MaxPencilType; i++)
        {
            // 1. 현재 순서에 해당하는 연필 데이터를 가져오기
            PencilType pencilType = (PencilType)i;
            PencilData pencilData = pencilManager.GetPencilData(pencilType);
            // 2. 버튼 복제하기
            Button itemButton = Instantiate(this.itemButton, this.itemButton.transform.parent);
            // 3. 복제한 버튼에 그 연필의 이름과 가격 표시하기
            itemButton.image.sprite = pencilData.PencilStates[0].pencilSprite;
            
            // 4. 클릭하면 그 연필이 선택되도록 연결하기
            
            // 복제한 아이템으로 보여주기
            itemButton.gameObject.SetActive(true);
        }
    }
}

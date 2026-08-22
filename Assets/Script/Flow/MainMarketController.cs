using UnityEngine;

public class MainMarketController : MonoBehaviour
{
    [SerializeField] MainMarketUI mainMarketUI;
    [SerializeField] ItemDetailInfoUI itemDetailInfoUI;
    
    void OnEnable()
    {
        mainMarketUI.OnItemButtonClickedEvent += HandleItemButtonClickedEvent;
        itemDetailInfoUI.OnPurchaseButtonClickedEvent += HandlePurchaseButtonClickedEvent;
    }

    void HandleItemButtonClickedEvent()
    {
        itemDetailInfoUI.ShowDetailInfo();
    }

    void HandlePurchaseButtonClickedEvent(int amount)
    {
        // TODO: 선택된 상품과 수량을 이용해 실제 구매를 처리한다.
    }

    void OnDisable()
    {
        mainMarketUI.OnItemButtonClickedEvent -= HandleItemButtonClickedEvent;
        itemDetailInfoUI.OnPurchaseButtonClickedEvent -= HandlePurchaseButtonClickedEvent;
    }
}

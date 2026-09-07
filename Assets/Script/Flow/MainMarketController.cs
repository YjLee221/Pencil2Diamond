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
        // 연필의 종류, 가격, 개수를 비교해 돈 계산
        
        // 플레이어가 가지고 있는 돈과 비교
        // 플레이어 소유 돈이 더 많거나 같으면 구매하기 버튼 활성화
        // 그 외는 비활성화
    }

    void OnDisable()
    {
        mainMarketUI.OnItemButtonClickedEvent -= HandleItemButtonClickedEvent;
        itemDetailInfoUI.OnPurchaseButtonClickedEvent -= HandlePurchaseButtonClickedEvent;
    }
}

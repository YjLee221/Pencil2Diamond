using System;
using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour
{
    [SerializeField] public PlayerData playerData;
    [SerializeField] DiamondData diamondData;
    [SerializeField] PencilManager pencilManager;
    
    [Header("Data")]
    PencilData pencilData;
    PencilInventoryData pencilInventoryData;
    
    public int UnsharpenedPencilCount { get; private set; }
    public int GraphiteCount { get; private set; }
    public int PlayerCoinCount { get; private set; }
    
    public event Action OnInventoryChangedEvent;

#if UNITY_EDITOR || DEVELOPMENT_BUILD
    public void SetCurrencyForDev(int coinCount, int diamondCount)
    {
        PlayerCoinCount = Mathf.Max(0, coinCount);
        playerData.diamondCount = Mathf.Max(0, diamondCount);
        
        OnInventoryChangedEvent?.Invoke();
    }
#endif
    
    // 연필 데이터를 찾는 함수
    public PencilInventoryData FindPencilData(PencilType pencilType)
    {
        return playerData.pencilInventoryDataList.Find(inventoryData=>inventoryData.pencilType == pencilType);
    }

    public void ResetInventory()
    {
        UnsharpenedPencilCount = 0;
        GraphiteCount = 0;
        PlayerCoinCount = 0;
    }
    
    public bool TryBuyingPencil(PencilType pencilType, int purchaseAmount)
    {
        var findPencilData = FindPencilData(pencilType);

        int checkRemainCoin = PlayerCoinCount - (findPencilData.pencilData.SellPriceForPencil * purchaseAmount);

        if (checkRemainCoin >= 0)
        {
            TryUnsharpenedGettingPencil(pencilType, purchaseAmount);
            PlayerCoinCount = checkRemainCoin;
            
            OnInventoryChangedEvent?.Invoke();
            
            return true;
        }
        else
        {
            Debug.LogError("잔액이 부족합니다");
            return false;
        }
    }

    int TryUnsharpenedGettingPencil(PencilType pencilType, int purchaseAmount)
    {
        var newPencilData = FindPencilData(pencilType);
        UnsharpenedPencilCount = newPencilData.unSharpenedCount += purchaseAmount;
        
        return UnsharpenedPencilCount;
    }

    public int AddGraphite()
    {
        GraphiteCount++;
        OnInventoryChangedEvent?.Invoke();
        
        return GraphiteCount;
    }

    int AddCoin()
    {
        PlayerCoinCount += diamondData.SellPriceForDiamond;
        OnInventoryChangedEvent?.Invoke();
        
        return PlayerCoinCount;
    }

    public int AddDiamond()
    {
        playerData.diamondCount++;
        OnInventoryChangedEvent?.Invoke();
        
        return playerData.diamondCount;
    }

    public int SellDiamond()
    {
        if (playerData.diamondCount > 0)
        {
            playerData.diamondCount--;
            
            AddCoin();
        }
        else
        {
            playerData.diamondCount = 0;
        }

        return playerData.diamondCount;
    }
}

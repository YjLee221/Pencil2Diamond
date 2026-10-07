using System;
using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] DiamondData diamondData;
    [SerializeField] PencilManager pencilManager;
    
    [Header("Data")]
    PencilData pencilData;
    PencilInventoryData pencilInventoryData;
    
    public int UnsharpenedPencilCount { get; private set; }
    public int GraphiteCount { get; private set; }
    public int PlayerCoinCount { get; private set; }
    
    public event Action OnInventoryChangedEvent;
    
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
    

    public int TryBuyingPencil(PencilType pencilType, int purchaseAmount)
    {
        var findPencilData = FindPencilData(pencilType);
        
        int checkRemainCoin = playerData.coinCount - (findPencilData.pencilData.SellPriceForPencil * purchaseAmount);

        if (checkRemainCoin >= 0)
        {
            TyrGettingPencil(pencilType, purchaseAmount);
            playerData.coinCount = checkRemainCoin;
            
            OnInventoryChangedEvent?.Invoke();
        }
        else Debug.Log("잔액이 부족합니다");
        
        return playerData.coinCount;
    }

    int TyrGettingPencil(PencilType pencilType, int purchaseAmount)
    {
        var newPencilData = FindPencilData(pencilType);
        return newPencilData.unSharpenedCount += purchaseAmount;
    }

    public int AddGraphite()
    {
        GraphiteCount++;
        OnInventoryChangedEvent?.Invoke();
        
        return GraphiteCount;
    }

    int AddCoin()
    {
        playerData.coinCount += diamondData.SellPriceForDiamond;
        OnInventoryChangedEvent?.Invoke();
        
        return playerData.coinCount;
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

using System;
using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] DiamondData diamondData;
    [SerializeField] PencilManager pencilManager;

    public int UnsharpenedPencilCount => playerData.unSharpenedPencilCount;
    public int GraphiteCount => playerData.graphiteCount;
    public int PlayerCoinCount => playerData.coinCount;
    
    public event Action OnInventoryChangedEvent;

    public void ResetInventory()
    {
        playerData.graphiteCount = 0;
        playerData.diamondCount = 0;
        playerData.coinCount = 0;
    }

    public int CalForBuyingPencil(PencilType pencilType, int purchaseAmount)
    {
        var pencilData = pencilManager.GetPencilData(pencilType);
        playerData.coinCount -= (pencilData.SellPriceForPencil * purchaseAmount);

        if (playerData.coinCount < 0) Debug.Log("잔액이 부족합니다");

        CalForGettingPencil(purchaseAmount);
        
        return playerData.coinCount;
    }

    int CalForGettingPencil(int purchaseAmount)
    {
        return playerData.unSharpenedPencilCount += purchaseAmount;
    }

    public int AddGraphite()
    {
        playerData.graphiteCount++;
        OnInventoryChangedEvent?.Invoke();
        
        return playerData.graphiteCount;
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

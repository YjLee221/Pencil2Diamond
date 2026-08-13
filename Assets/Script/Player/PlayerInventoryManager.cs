using System;
using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] DiamondData diamondData;

    public int UnsharpenedPencilCount => playerData.unSharpenedPencilCount;
    public int GraphiteCount => playerData.graphiteCount;
    
    public event Action OnInventoryChangedEvent;

    public void ResetInventory()
    {
        playerData.graphiteCount = 0;
        playerData.diamondCount = 0;
        playerData.coinCount = 0;
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

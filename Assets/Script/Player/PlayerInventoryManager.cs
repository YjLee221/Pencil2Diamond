using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour
{
    private int playerCoinCount;
    private int playerDiamondCount;
    
    [SerializeField] PlayerData playerData;
    [SerializeField] DiamondData diamondData;

    void Start()
    {
        playerCoinCount = playerData.coinCount;
        playerDiamondCount = playerData.diamondCount;
    }

    public int AddCoin()
    {
        playerDiamondCount += diamondData.SellPriceForDiamond;
        return playerDiamondCount;
    }

    public int AddDiamond()
    {
        return playerDiamondCount++;
    }

    public int SellDiamond(DiamondData targetDiamond)
    {
        if(playerDiamondCount > 0 && playerDiamondCount >= diamondData.SellPriceForDiamond)
        {
            playerData.diamondCount -= targetDiamond.SellPriceForDiamond;
            AddCoin();
        }
        
        else if(playerDiamondCount == 0) Debug.Log("No Diamond");
        
        return playerData.diamondCount;
    }
}

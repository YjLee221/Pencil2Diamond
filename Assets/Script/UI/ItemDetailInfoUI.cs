using UnityEngine;

public class ItemDetailInfoUI : MonoBehaviour
{
    [SerializeField] GameObject itemDetailInfoPanel;

    public void ShowDetailInfo()
    {
        itemDetailInfoPanel.SetActive(true);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Button btnKnife;
    [SerializeField] Button btnPencilCutter;

    public void OnClickKnife()
    {
        Debug.Log("Knife Clicked");
        btnPencilCutter.gameObject.SetActive(false);
    }

    public void OnClickPencilCutter()
    {
        Debug.Log("Pencil Cutter Clicked");
        btnKnife.gameObject.SetActive(true);
    }
}

using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour
{
    [Header("Pencil")]
    int unSharpeningPencilCount = 0;
    int sharpeningPencilCount = 0;
    int sharpenedPencilCount = 0;

    [Header("Graphite")]
    int graphiteCount = 0;

    [Header("Diamond")]
    int diamondCount = 0;

    PencilData pencil;
    GraphiteData graphite;
    DiamondData diamond;
}

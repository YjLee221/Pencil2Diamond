using UnityEngine;
using System.Linq;

public class PencilManager : MonoBehaviour
{
    [Header("Slots")]
    [SerializeField] SharpeningPencil[] pencilSlots;

    [SerializeField] UIManager uiManager;
    [SerializeField] PencilCollectedGraphite graphiteCollector;

    [Header("Pencil Data")]
    [SerializeField] PencilData[] pencilData;

    void OnEnable()
    {
        foreach (var slot in pencilSlots)
        {
            slot.OnPencilSharpeningCompleted += HandleSharpeningCompleted;
        }
    }

    void OnDisable()
    {
        foreach (var slot in pencilSlots)
        {
            slot.OnPencilSharpeningCompleted -= HandleSharpeningCompleted;
        }
    }

    public void StartTutorialMode()
    {
        if (pencilSlots.Length > 0)
        {
            pencilSlots[0].gameObject.SetActive(true);
            
            PencilData introPencil = GetPencilData(PencilType.INTRO_PENCIL);
            
            if (introPencil != null) 
            {
                pencilSlots[0].LoadPencil(introPencil);
            }
        }

    }

    public void MainGameMode(SharpeningPencil targetSlot, PencilType userSelectedPencil)
    {
        // 유저가 선택한 연필 타입에 맞는 데이터를 가져옴
        PencilData selectedPencilData = GetPencilData(userSelectedPencil);

        // 빈 슬롯이 남아 있는지 확인
        // 1. 빈 슬롯이 있다면, 선택한 연필 데이터를 해당 슬롯에 로드
        // 2. 빈 슬록이 없다면, 현재 모두 작업중이라 장착할 수 없다는 메세지 출력
    }

    public PencilData GetPencilData(PencilType pencilType)
    {
        return pencilData.FirstOrDefault(pencilData => pencilData.PencilType == pencilType);
    }

    void HandleSharpeningCompleted(SharpeningPencil sharpenedPencil)
    {
        Debug.Log($"Sharpening completed for pencil: {sharpenedPencil.name}");

        uiManager.ExtractingGraphiteCanvas();

        graphiteCollector.gameObject.SetActive(true);
        graphiteCollector.StartExtractingPhase(sharpenedPencil.currentPencilData, sharpenedPencil.currentGraphiteHp);
        
        sharpenedPencil.gameObject.SetActive(false);

    }
}
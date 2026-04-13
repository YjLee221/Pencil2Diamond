using UnityEngine;

public class Pencil : MonoBehaviour
{
    [SerializeField] float maxPencilHp = 100f;
    [SerializeField] public RectTransform pencilHitArea;
    public float currentPencilHp { get; private set; }

    void Start()
    {
        currentPencilHp = maxPencilHp;
    }

    // 연필 깎는 매서드
    public void TakeShaveDamage(float damage)
    {
        currentPencilHp -= damage;
        Debug.Log($"연필이 깎임! 남은 내구도: {currentPencilHp}");

        // 여기에 이전 답변의 스프라이트 변경(Sprite Swap) 로직이 들어갑니다.
        if (currentPencilHp <= 0)
        {
            Debug.Log("연필 다 깎음! 다이아몬드 생성!");
            currentPencilHp = 0;
        }
    }
}
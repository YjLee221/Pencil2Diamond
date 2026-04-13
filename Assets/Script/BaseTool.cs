using UnityEngine;

public abstract class BaseTool : MonoBehaviour
{
    [Header("공통 도구 스탯")]
    [SerializeField] string toolName;
    [SerializeField] public float shavePower; // 깎는 힘

    public abstract void TryUseTool(Pencil targetPencil);
}
using UnityEngine;

public interface IPencil
{
    int CurrentHP { get; }
    RectTransform HitArea { get; }
    void TakeShaveDamage(int damage);
}

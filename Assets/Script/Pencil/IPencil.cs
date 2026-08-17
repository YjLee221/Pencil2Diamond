using UnityEngine;

public interface IPencil
{
    int CurrentHp { get; }
    RectTransform HitArea { get; }
    void TakeShaveDamage(int damage);
}

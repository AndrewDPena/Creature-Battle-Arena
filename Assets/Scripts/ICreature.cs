using System.Collections;
using UnityEngine;

public interface ICreature
{
    void TakeDamage(int damage);
    void Attack1(Vector2 direction);
    void Attack2(Vector2 direction);
    void LearnAttack(IAttackType attackType);
    void Swap(int slot);
}
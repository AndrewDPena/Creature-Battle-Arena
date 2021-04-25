using System.Collections;
using AttackList;
using UnityEngine;


public abstract class AttackAreaOfEffect : MonoBehaviour
{
    public abstract IEnumerator Attack(Vector2 direction, Transform[] exitPoints,AttackBase attack);
}

using System.Collections;
using UnityEngine;


public abstract class AttackAreaOfEffect : MonoBehaviour
{
    public abstract IEnumerator Attack(Vector2 direction, Transform[] exitPoints, GameObject spriteObject);
}

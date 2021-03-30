using System.Collections;
using UnityEngine;

public interface IAttackType
{
    IEnumerator Attack(Vector2 direction, Transform[] exitPoints, GameObject attackPrefab);
}
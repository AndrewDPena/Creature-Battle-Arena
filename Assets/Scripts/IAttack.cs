using System.Collections;
using UnityEngine;

public interface IAttack
{
    IEnumerator Attack(Vector2 direction, Transform[] exitPoints, GameObject attackPrefab);
}
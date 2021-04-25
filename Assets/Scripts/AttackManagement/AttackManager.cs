using UnityEngine;
using AttackList;

public class AttackManager : MonoBehaviour
{  
    public void Attack(AttackBase attack, Vector2 direction, Transform[] exitPoints)
    {
        var area = attack.GetAreaOfEffect();
        StartCoroutine(area.Attack(direction, exitPoints, attack));
    }
}

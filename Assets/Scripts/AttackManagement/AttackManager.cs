using UnityEngine;
using AttackAreas;

namespace AttackManagement
{
    public class AttackManager : MonoBehaviour
    {
        public void Attack(AttackBase attack, Vector2 direction, Transform[] exitPoints, Creature creature)
        {
            var area = attack.GetAreaOfEffect();
            StartCoroutine(area.Attack(direction, exitPoints, attack, creature));
        }
    }
}

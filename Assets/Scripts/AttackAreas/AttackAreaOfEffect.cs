using System.Collections;
using AttackAreas;
using UnityEngine;

namespace AttackManagement
{
    public abstract class AttackAreaOfEffect : MonoBehaviour
    {
        public abstract IEnumerator Attack(Vector2 direction, Transform[] exitPoints, AttackBase attack,
            Creature creature);
    }
}
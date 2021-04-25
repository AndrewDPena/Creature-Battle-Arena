using System.Collections;
using AttackManagement;
using UnityEngine;


namespace AttackAreas
{
    public class ConeAttack : AttackAreaOfEffect
    {
        public override IEnumerator Attack(Vector2 direction, Transform[] exitPoints, AttackBase attackBase, Creature creature)
        {
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            var cast = angle - (angle % 45);
            var point = (int) (cast / 45);
            if (point < 0)
            {
                point += exitPoints.Length;
            }

            var spriteObject = attackBase.SpriteObject;
            var attack = Instantiate(spriteObject, exitPoints[point].position, Quaternion.identity);
            attack.transform.rotation = Quaternion.AngleAxis(cast, Vector3.forward);
            var damage = attack.GetComponent<DamageManager>();
            damage.SetAttack(attackBase);
            damage.SetAttacker(creature);

            yield return new WaitForSeconds(.25f);
    
            Destroy(attack);
        }
    }
}
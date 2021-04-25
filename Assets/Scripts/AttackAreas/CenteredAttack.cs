using System.Collections;
using AttackList;
using UnityEngine;


namespace AttackAreas
{
    public class CenteredAttack : AttackAreaOfEffect
    {
        public override IEnumerator Attack(Vector2 direction, Transform[] exitPoints, AttackBase attackBase)
        {
            var spriteObject = attackBase.SpriteObject;
            var attack = Instantiate(spriteObject, exitPoints[6].position, Quaternion.identity);
            var damage = attack.GetComponent<DamageManager>();
            damage.SetAttack(attackBase);

            yield return new WaitForSeconds(.25f); 
    
            Object.Destroy(attack);
        }
    }
}
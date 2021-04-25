using UnityEngine;
using AttackList;

public class AttackManager : MonoBehaviour
{
    private int _battlePower;
    private int _damagePerTick;
    
    public void Attack(AttackBase attack, Vector2 direction, Transform[] exitPoints)
    {
        var area = attack.GetAreaOfEffect();
        _battlePower = attack.Damage;
        _damagePerTick = attack.DamagePerTick;
        StartCoroutine(area.Attack(direction, exitPoints, attack.SpriteObject));
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        var c = other.GetComponentInParent<Creature>();
        if (c != null & _battlePower > 0)
        {
            Debug.Log("You shouldn't see me if you're burning the tree.");
            c.TakeDamage(_damagePerTick);
            _battlePower -= _damagePerTick;
        }
    }
}

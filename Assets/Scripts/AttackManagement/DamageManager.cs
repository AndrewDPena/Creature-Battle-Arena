using AttackList;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    private int _battlePower;
    private int _damagePerTick;

    public void SetAttack(AttackBase attack)
    {
        _battlePower = attack.Damage;
        _damagePerTick = attack.DamagePerTick;
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

using AttackAreas;
using UnityEngine;
using static TypeChart;

namespace AttackManagement
{
    public class DamageManager : MonoBehaviour
    {
        private int _battlePower;
        private int _damagePerTick;
        private bool _canDamageSelf;
        private CreatureType _attackType;
        private Creature _attacker;

        public void SetAttack(AttackBase attack)
        {
            _battlePower = attack.Damage;
            _damagePerTick = attack.DamagePerTick;
            _canDamageSelf = attack.CanDamageSelf;
            _attackType = attack.AttackType;
        }

        public void SetAttacker(Creature creature)
        {
            _attacker = creature;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            var c = other.GetComponentInParent<Creature>();
            if (c == _attacker && !_canDamageSelf)
            {
                return;
            }

            if (c != null & _battlePower > 0)
            {
                Debug.Log("You shouldn't see me if you're burning the tree.");
                var mult = c.GetDamageMultiplier(_attackType);
                c.TakeDamage((int)(_damagePerTick * mult));
                _battlePower -= _damagePerTick;
            }
        }

        public int BattlePower => _battlePower;

        public int DamagePerTick => _damagePerTick;

        public bool CanDamageSelf => _canDamageSelf;

        public CreatureType AttackType => _attackType;

        public Creature Attacker => _attacker;
    }
}

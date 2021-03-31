using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AttackList
{

    public class Explosion : MonoBehaviour
    {
        private int _battlePower;
        private int _damagePerUpdate;
            
        void Start()
        {
            _battlePower = 20;
            _damagePerUpdate = 5;
        }
    
        private void OnTriggerStay2D(Collider2D other)
        {
            Debug.Log("Events are working.");
            var c = other.GetComponentInParent<Creature>();
            if (c != null & _battlePower > 0)
            {
                c.TakeDamage(_damagePerUpdate);
                _battlePower -= _damagePerUpdate;
            }
        }
    }
}

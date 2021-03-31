using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AttackList
{

    public class FireBreath : MonoBehaviour
    {
        private int _battlePower;
        private int _damagePerUpdate;
            
        void Start()
        {
            _battlePower = 10;
            _damagePerUpdate = 1;
        }
    
        void Update()
        {
            // This is the godawful damage per frame firebreath
            /*var results = new Collider2D[10];
            if (Physics2D.OverlapCollider(_collider, new ContactFilter2D(), results) > 0)
            {
                foreach (var collider in results)
                {
                    if (collider.gameObject.GetComponent<Creature>() != null)
                    {
                        var creature = collider.gameObject.GetComponent<Creature>();
                        creature.TakeDamage(1);
                    }
                }
            }*/
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

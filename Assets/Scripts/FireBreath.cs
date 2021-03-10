using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBreath : MonoBehaviour
{
    [SerializeField] 
    private Collider2D _collider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var results = new Collider2D[10];
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
        }
    }
}

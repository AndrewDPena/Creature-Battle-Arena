using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainTrigger : MonoBehaviour
{
    [SerializeField] 
    public float TerrainModifier;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        var c = other.attachedRigidbody.GetComponentInParent<CreatureMove>();
        c.SetTerrainModifier(TerrainModifier);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var c = other.attachedRigidbody.GetComponentInParent<CreatureMove>();
        c.SetTerrainModifier(1f);    
    }
}

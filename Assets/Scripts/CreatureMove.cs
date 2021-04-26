using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Rigidbody2D))]
public class CreatureMove : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private float _creatureSpeed;
    public float TerrainSpeedModifier;
    private bool _isFlying;
    private Vector2 _velocityVector;
    public IUnityService UnityService;

    private void Awake()
    {
        TerrainSpeedModifier = 1;
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _rigidbody2D.gravityScale = 0;
        if (UnityService == null)
        {
            UnityService = new UnityService();
        }
    }

    public void SetVelocity(Vector2 velocity)
    {
        _velocityVector = velocity;
    }

    public void SetCreatureSpeed(float speed)
    {
        _creatureSpeed = speed;
    }

    public bool IsFlying
    {
        get { return _isFlying; }
        set { _isFlying = value; }
    }

    public void SetTerrainModifier(float modifier)
    {
        TerrainSpeedModifier = _isFlying ? 1f : modifier;            
    }

    private void Update()
    {
        _rigidbody2D.AddRelativeForce(_velocityVector * UnityService.GetDeltaTime() * _creatureSpeed * TerrainSpeedModifier);

        // This block of code caused the adorable and hilarious rotation. Leaving it here so I can always re-enable
        // its awfulness. It is MUCH worse now that the game uses physics.
        /**
        if (_velocityVector != Vector2.zero)
        {
            gameObject.transform.right = _velocityVector;
        } **/
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CreatureMove : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    public float CreatureSpeed;
    private Vector2 _velocityVector;
    public IUnityService UnityService;

    private void Awake()
    {
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

    private void Update()
    {
        _rigidbody2D.AddRelativeForce(_velocityVector * UnityService.GetDeltaTime() * CreatureSpeed);

        // This block of code caused the adorable and hilarious rotation. Leaving it here so I can always re-enable
        // its awfulness.
        /**
        if (_velocityVector != Vector2.zero)
        {
            gameObject.transform.right = _velocityVector;
        } **/
    }
}

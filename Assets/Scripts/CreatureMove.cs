using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CreatureMove : MonoBehaviour
{
    private CharacterController _controller;
    public float CreatureSpeed;
    private Vector2 _velocityVector;
    public IUnityService UnityService;

    private void Awake()
    {
        _controller = gameObject.GetComponent<CharacterController>();
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
        _controller.Move(_velocityVector * UnityService.GetDeltaTime() * CreatureSpeed);

        // This block of code caused the adorable and hilarious rotation. Leaving it here so I can always re-enable
        // its awfulness.
        /**
        if (_velocityVector != Vector2.zero)
        {
            gameObject.transform.right = _velocityVector;
        } **/
    }
}

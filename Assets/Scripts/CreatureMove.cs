using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CreatureMove : MonoBehaviour
{
    private CharacterController _controller;
    private float _creatureSpeed = 2.0f;
    public IUnityService UnityService;

    // Start is called before the first frame update
    void Start()
    {
        _controller = gameObject.GetComponent<CharacterController>();
        if (UnityService == null)
        {
            UnityService = new UnityService();
        }
    }

    // Update is called once per frame
    void Update()
    {
        var move = new Vector2(UnityService.GetAxis("Horizontal"), UnityService.GetAxis("Vertical"));
        _controller.Move(move * UnityService.GetDeltaTime() * _creatureSpeed);

        if (move != Vector2.zero)
        {
            gameObject.transform.right = move;
        }
    }
}

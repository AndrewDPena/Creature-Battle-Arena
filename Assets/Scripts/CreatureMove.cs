using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CreatureMove : MonoBehaviour
{
    private CharacterController _controller;
    private float playerSpeed = 2.0f;
    private UnityService _unityService;

    // Start is called before the first frame update
    void Start()
    {
        _controller = gameObject.GetComponent<CharacterController>();
        if (_unityService == null)
        {
            _unityService = new UnityService();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = new Vector2(_unityService.GetAxis("Horizontal"), _unityService.GetAxis("Vertical"));
        _controller.Move(move * _unityService.GetDeltaTime() * playerSpeed);

        if (move != Vector2.zero)
        {
            gameObject.transform.right = move;
        }
    }
}

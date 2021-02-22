using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StupidFuckingSolution : MonoBehaviour
{
    private CharacterController _controller;
    private float playerSpeed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        _controller = gameObject.GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
    }
}

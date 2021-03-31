using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputKeyboard : MonoBehaviour
{
    
    public IUnityService UnityService;
    private CreatureMove _move;

    private void Start()
    {
        _move = gameObject.GetComponent<CreatureMove>();
        if (UnityService == null)
        {
            UnityService = new UnityService();
        }
    }

    private void Update()
    {
        var move = new Vector2(UnityService.GetAxis("Horizontal"), UnityService.GetAxis("Vertical"));
        _move.SetVelocity(move);

        if (UnityService.GetKeyDown("left shift"))
        {
            gameObject.GetComponent<Creature>().Attack2(move);
        }

        if (UnityService.GetKeyDown("left ctrl"))
        {
            gameObject.GetComponent<Creature>().Attack1(move);
        }
    }
}

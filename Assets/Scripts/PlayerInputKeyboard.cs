using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputKeyboard : MonoBehaviour
{
    
    public IUnityService UnityService;
    private CreatureMove _move;
    private Creature _creature;

    private void Start()
    {
        _move = gameObject.GetComponent<CreatureMove>();
        _creature = gameObject.GetComponent<Creature>();
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
            _creature.Attack2(move);
        }

        if (UnityService.GetKeyDown("left ctrl"))
        {
            _creature.GetComponent<Creature>().Attack1(move);
        }

        if (UnityService.GetKeyDown("1"))
        {
            _creature.Return(1);
        }
    }
}

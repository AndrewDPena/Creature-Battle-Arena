using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInputKeyboard : MonoBehaviour
{
    
    public IUnityService UnityService;
    private CreatureMove _move;
    private Creature _creature;
    private readonly List<string> _slots = new List<string>(){"1", "2", "3", "4"};

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
            _creature.Attack1(move);
        }

        var inputs = UnityService.InputString();

        if (_slots.Any(s=>inputs.Contains(s)))
        {
            _creature.Swap(int.Parse(_slots.First(s=>inputs.Contains(s))));
        }
    }
}

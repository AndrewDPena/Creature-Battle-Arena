using System;
using AttackList;
using UnityEngine;
using System.Collections.Generic;
using AttackManagement;

public class Creature : MonoBehaviour
{
    public CreatureData CurrentCreature;
    private SpriteRenderer _renderer;
    [SerializeField] private Transform[] _exitPoints;
    [SerializeField] private AttackManager _manager;
    [SerializeField] private List<AttackBase> _attacks = new List<AttackBase>();

    public Player Owner;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _manager = GetComponent<AttackManager>();
    }

    public void AssignPlayer(Player player)
    {
        Owner = player;
    }

    public void Summon(CreatureData creature)
    {
        CurrentCreature = creature;
        _attacks = creature.Attacks;

        try
        {
            _renderer.sprite = CurrentCreature.Sprite;
        }
        catch (NullReferenceException ex)
        {
            Debug.Log("Why on Earth does this fail?");
        }
    }

    public void Swap(int slot)
    {
        if (Owner.CanSummonCreature(slot))
        {
            Summon(Owner.SummonCreature(slot));
        }
    }

    public void TakeDamage(int damage)
    {
        CurrentCreature.TakeDamage(damage);
        Owner.UpdateHUD(CurrentCreature);
    }

    // Change to one method with a dictionary or something, potentially
    public void Attack1(Vector2 direction)
    {
        _manager.Attack(_attacks[0], direction, _exitPoints, this);
    }

    public void Attack2(Vector2 direction)
    {
        _manager.Attack(_attacks[1], direction, _exitPoints, this);
    }

    public void LearnAttack(AttackBase attack)
    {
        
    }
}
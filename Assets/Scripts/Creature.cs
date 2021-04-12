using System;
using UnityEngine;

public class Creature : MonoBehaviour, ICreature
{
    /*public string Name;
    public int Strength;
    public int MaxHealth;
    public int CurrentHealth;*/
    public CreatureData CurrentCreature;
    [SerializeField] private Transform[] _exitPoints;
    [SerializeField] private GameObject _attack1Prefab;
    [SerializeField] private GameObject _attack2Prefab;
    [SerializeField] private IAttackType _attack1;
    [SerializeField] private IAttackType _attack2;

    public Player Owner;

    public void AssignPlayer(Player player)
    {
        Owner = player;
    }

    public void Summon(CreatureData creature)
    {
        CurrentCreature = creature;
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
        StartCoroutine(_attack1.Attack(direction, _exitPoints, _attack1Prefab));
    }

    public void Attack2(Vector2 direction)
    {
        StartCoroutine(_attack2.Attack(direction, _exitPoints, _attack2Prefab));
    }

    public void LearnAttack(IAttackType attackType)
    {
        if (_attack1 == null)
        {
            _attack1 = attackType;
            //_attack1Prefab = attackPrefab;
        }
        else
        {
            _attack2 = attackType;
        }
    }
}
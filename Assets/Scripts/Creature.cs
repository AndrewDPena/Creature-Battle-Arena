using System;
using UnityEngine;

public class Creature : MonoBehaviour, ICreature
{
    public string Name;
    public int Strength;
    public int MaxHealth;
    public int CurrentHealth;
    [SerializeField] private Transform[] _exitPoints;
    [SerializeField] private GameObject _attack1Prefab;
    [SerializeField] private GameObject _attack2Prefab;
    [SerializeField] private IAttackType _attack1;
    [SerializeField] private IAttackType _attack2;

    private Player _owner;

    public void Setup(string creatureName, int strength, int maxHealth, Player player)
    {
        Name = creatureName;
        Strength = strength;
        MaxHealth = maxHealth;
        CurrentHealth = MaxHealth;
        _owner = player;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        _owner.UpdateHUD(this, CurrentHealth);
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

    public void Return(int slot)
    {
        if (_owner.GetPocketSize() > slot)
        {
            _owner.SummonCreature(slot);
        }
    }
}
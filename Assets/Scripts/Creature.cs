using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;

public class Creature : MonoBehaviour
{
    public string Name;
    public int Strength;
    public int MaxHealth;
    public int CurrentHealth;
    [SerializeField] private Transform[] _exitPoints;
    [SerializeField] private GameObject _attack1Prefab;
    [SerializeField] private IAttack _attack1;
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

    public void Attack1(Vector2 direction)
    {
        StartCoroutine(_attack1.Attack(direction, _exitPoints, _attack1Prefab));
    }

    public void LearnAttack(IAttack attack)
    {
        _attack1 = attack;
    }
}
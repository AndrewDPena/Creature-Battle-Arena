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
}
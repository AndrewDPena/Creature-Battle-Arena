using System.Collections;
using UnityEngine;

public class CreatureData
{
    public string Name;
    public int Strength;
    public int MaxHealth;
    public int CurrentHealth;
    
    public CreatureData(string creatureName, int strength, int maxHealth)
    {
        Name = creatureName;
        Strength = strength;
        MaxHealth = maxHealth;
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
    }
}

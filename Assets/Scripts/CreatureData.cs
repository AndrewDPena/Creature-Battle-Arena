using System.Collections;
using UnityEngine;

public class CreatureData
{
    public string Name;
    public int Strength;
    public int MaxHealth;
    public int CurrentHealth;
    public Sprite Sprite;

    public CreatureData(CreatureBase cBase)
    {
        Name = cBase.Name;
        Strength = cBase.Strength;
        MaxHealth = cBase.MaxHealth;
        CurrentHealth = MaxHealth;
        Sprite = cBase.CreatureSprite;
    }
    
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

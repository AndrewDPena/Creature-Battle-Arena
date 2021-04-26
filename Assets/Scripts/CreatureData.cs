using AttackManagement;
using UnityEngine;
using System.Collections.Generic;

public class CreatureData
{
    public string Name;
    public int Strength;
    public int MaxHealth;
    public int CurrentHealth;
    public float CreatureSpeed;
    public TypeChart.CreatureType CreatureType1;
    public TypeChart.CreatureType CreatureType2;
    public Sprite Sprite;
    public List<AttackBase> Attacks = new List<AttackBase>();

    public CreatureData(CreatureBase cBase)
    {
        Name = cBase.Name;
        Strength = cBase.Strength;
        MaxHealth = cBase.MaxHealth;
        CurrentHealth = MaxHealth;
        CreatureSpeed = cBase.CreatureSpeed;
        CreatureType1 = cBase.CreatureType1;
        CreatureType2 = cBase.CreatureType2;
        Sprite = cBase.CreatureSprite;
        Attacks = cBase.Attacks;
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

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using UnityEditor;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public string Name;
    public int Strength;
    public int MaxHealth = 69;
    public int CurrentHealth;
    public HealthBar HealthBar;
    
    private void Start()
    {
        CurrentHealth = MaxHealth;
        HealthBar.SetMaxHealth(MaxHealth);
    }

    private void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        HealthBar.SetHealth(CurrentHealth);
    }
}

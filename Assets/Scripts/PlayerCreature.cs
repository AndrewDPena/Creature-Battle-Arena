using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PlayerCreature : MonoBehaviour
{
    public int MaxHealth = 69;
    public int CurrentHealth;
    [SerializeReference]
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

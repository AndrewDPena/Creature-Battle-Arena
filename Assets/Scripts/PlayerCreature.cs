using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCreature : MonoBehaviour
{
    public int MaxHealth = 69;
    public int CurrentHealth;

    public HealthBar HealthBar;

    
    // Start is called before the first frame update
    private void Start()
    {
        CurrentHealth = MaxHealth;
        HealthBar.SetMaxHealth(MaxHealth);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        HealthBar.SetHealth(CurrentHealth);
    }
}

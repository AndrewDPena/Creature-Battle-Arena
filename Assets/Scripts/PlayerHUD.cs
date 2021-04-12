using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public Text Name;
    public Text HealthDisplay;
    public HealthBar HealthBar;

    public void InitializeHUD(CreatureData creature)
    {
        Name.text = creature.Name;
        SetMaxHealth(creature.MaxHealth);
        SetHealth(creature.CurrentHealth);
    }

    private void SetMaxHealth(int health)
    {
        HealthBar.SetMaxHealth(health);
    }

    public void SetHealth(int health)
    {
        HealthBar.SetHealth(health);
        SetHealthDisplayText(health);
    }

    public int GetHealth()
    {
        return HealthBar.GetHealth();
    }

    private void SetHealthDisplayText(int health)
    {
        HealthDisplay.text = health + " / " + HealthBar.GetMaxHealth();
    }
}

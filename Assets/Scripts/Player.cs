using System.Collections;
using System.Collections.Generic;
using GitHub.Unity;
using UnityEngine;

public class Player
{
    public string Name;
    private PlayerHUD _hud;
    private List<CreatureData> CreaturePocket = new List<CreatureData>();
    private CreatureData _activeCreature;

    public void AddCreature(CreatureData creature)
    {
        CreaturePocket.Add(creature);
    }

    public int GetPocketSize()
    {
        return CreaturePocket.Count;
    }

    public bool CanSummonCreature(int slot)
    {
        if (slot >= GetPocketSize())
        {
            return false;
        }

        return CreaturePocket[slot].CurrentHealth > 0;
    }

    public void SetHUD(PlayerHUD HUD)
    {
        _hud = HUD;
    }

    public CreatureData SummonCreature(int slot)
    {
        _activeCreature = CreaturePocket[slot];
        CreaturePocket[slot] = CreaturePocket[0];
        CreaturePocket[0] = _activeCreature;
        _hud.InitializeHUD(_activeCreature);
        return _activeCreature;
    }

    public CreatureData GetActiveCreature()
    {
        return _activeCreature;
    }

    public void UpdateHUD(CreatureData creature)
    {
        if (creature == _activeCreature)
        {
            _hud.SetHealth(creature.CurrentHealth);
        }
    }
}

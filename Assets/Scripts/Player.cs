using System.Collections;
using System.Collections.Generic;
using GitHub.Unity;
using UnityEngine;

public class Player
{
    public string Name;
    private PlayerHUD _hud;
    private List<Creature> CreaturePocket = new List<Creature>();
    private Creature _activeCreature;

    public void AddCreature(Creature creature)
    {
        CreaturePocket.Add(creature);
    }

    public int GetPocketSize()
    {
        return CreaturePocket.Count;
    }

    public void SetHUD(PlayerHUD HUD)
    {
        this._hud = HUD;
    }

    public Creature SummonCreature(int slot)
    {
        _activeCreature = CreaturePocket[slot];
        _hud.InitializeHUD(_activeCreature);
        return CreaturePocket[slot];
    }

    public Creature GetActiveCreature()
    {
        return _activeCreature;
    }

    public void UpdateHUD(Creature creature, int currentHealth)
    {
        if (creature == _activeCreature)
        {
            _hud.SetHealth(currentHealth);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UserInterfaceScripts;

public class Player
{
    public string Name;
    private PlayerHUD _hud;
    private PocketHUD _pocketHud;
    private AttackWindowHud _ctrlHud;
    private AttackWindowHud _shiftHud;
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

    public bool HasRemainingCreatures()
    {
        foreach (var creature in CreaturePocket)
        {
            if (creature.CurrentHealth > 0)
            {
                return true;
            }
        }

        return false;
    }

    public bool CanSummonCreature(int slot)
    {
        return !(slot >= GetPocketSize()) && CreaturePocket[slot].CurrentHealth > 0;
    }

    public void SetHUD(PlayerHUD HUD)
    {
        _hud = HUD;
    }

    public void SetPocketHUD(PocketHUD HUD)
    {
        _pocketHud = HUD;
        SetHUD(_pocketHud.GetPrimaryHud());
    }

    public void SetAttackHuds(AttackWindowHud ctrl, AttackWindowHud shift)
    {
        _ctrlHud = ctrl;
        _shiftHud = shift;
    }

    private void UpdatePocketHUD()
    {
        for (var i = 0; i < _pocketHud.GetNumOfHuds(); i++)
        {
            var creature = i > GetPocketSize() - 1 ? null : CreaturePocket[i];            
            _pocketHud.SetHud(creature, i);
        }
    }

    private void UpdateAttackHuds(CreatureData creature)
    {
        if (_ctrlHud == null || creature.Attacks.Count < 1)
        {
            return;
            
        }
        _ctrlHud.SetAttackDesc(creature.Attacks[0]);
        _shiftHud.SetAttackDesc(creature.Attacks[1]);
    }

    public CreatureData SummonCreature(int slot)
    {
        _activeCreature = CreaturePocket[slot];
        CreaturePocket[slot] = CreaturePocket[0];
        CreaturePocket[0] = _activeCreature;
        UpdatePocketHUD();
        UpdateAttackHuds(_activeCreature);
        return _activeCreature;
    }

    public CreatureData GetActiveCreature()
    {
        return _activeCreature;
    }

    public int GetNextHealthyCreature()
    {
        for (var i = 0; i < GetPocketSize(); i++)
        {
            if (CanSummonCreature(i))
            {
                return i;
            }
        }
        // A -1 should NEVER be returned; something seriously fucked up if we hit this.
        Debug.Log("Something SERIOUSLY fucked up");
        return -1;
    }

    public void UpdateHUD(CreatureData creature)
    {
        if (creature == _activeCreature)
        {
            _hud.SetHealth(creature.CurrentHealth);
        }
    }
}

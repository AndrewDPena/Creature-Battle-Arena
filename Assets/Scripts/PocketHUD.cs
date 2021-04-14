using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PocketHUD : MonoBehaviour
{
    [SerializeField] private List<PlayerHUD> _hudList = new List<PlayerHUD>();

    public void SetHud(CreatureData creature, int slot)
    {
        _hudList[slot].InitializeHUD(creature);
    }

    public void AddHud(PlayerHUD hud)
    {
        _hudList.Add(hud);
    }

    public PlayerHUD GetPrimaryHud()
    {
        return _hudList[0];
    }

    public int GetNumOfHuds()
    {
        return _hudList.Count;
    }
}

using System.Collections;
using UnityEngine;

public class PocketHUD : MonoBehaviour
{
    [SerializeField] private PlayerHUD[] _hudList;

    public void SetHud(CreatureData creature, int slot)
    {
        _hudList[slot].InitializeHUD(creature);
    }

    public PlayerHUD GetPrimaryHud()
    {
        return _hudList[0];
    }
}

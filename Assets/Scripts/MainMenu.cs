using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //[SerializeField] private GameObject _pocketMenu;
    //[SerializeField] private GameObject _mainMenu;
    [SerializeField] private Creaturedex _creatureDex;
    [SerializeField] private Dropdown[] _playerSlots;
    [SerializeField] private Dropdown[] _npcSlots;
    [SerializeField] private List<CreatureBase> _playerCreatures;
    [SerializeField] private List<CreatureBase> _npcCreatures;
    
    public void LoadBattleScene()
    {
        SceneManager.LoadScene("ArenaScene");
    }

    public void SavePocket()
    {
        _playerCreatures = new List<CreatureBase>();
        _npcCreatures = new List<CreatureBase>();
        _playerSlots[0].value += 1; // Accounts for lack of "None" option
        _npcSlots[0].value += 1;
        foreach (var slot in _playerSlots)
        {
            if (slot.value == 0)
            {
                continue;
            }
            _playerCreatures.Add(_creatureDex.GetCreatureByDexNumber(slot.value));
        }
        foreach (var slot in _npcSlots)
        {
            if (slot.value == 0)
            {
                continue;
            }
            _npcCreatures.Add(_creatureDex.GetCreatureByDexNumber(slot.value));
        }
    }
}

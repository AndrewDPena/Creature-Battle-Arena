using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Creaturedex _creatureDex;
    [SerializeField] private Dropdown[] _playerSlots;
    [SerializeField] private Dropdown[] _npcSlots;
    [SerializeField] private List<CreatureBase> _playerCreatures;
    [SerializeField] private List<CreatureBase> _npcCreatures;
    
    public void LoadBattleScene()
    {
        SceneManager.LoadScene("ArenaScene");
        ArenaHandler.SetData(_playerCreatures, _npcCreatures);
    }

    private void Awake()
    {
        var options = LoadCreaturedex();
        _playerSlots[0].ClearOptions();
        _playerSlots[0].AddOptions(options);
        _npcSlots[0].ClearOptions();
        _npcSlots[0].AddOptions(options);
        
        options.Insert(0, new Dropdown.OptionData("None"));

        for (var i = 1; i < _playerSlots.Length; i++)
        {
            _playerSlots[i].ClearOptions();
            _playerSlots[i].AddOptions(options);
            _npcSlots[i].ClearOptions();
            _npcSlots[i].AddOptions(options);
        }
    }

    private List<Dropdown.OptionData> LoadCreaturedex()
    {
        var options = new List<Dropdown.OptionData>();
        foreach (var creature in _creatureDex.CreatureDex)
        {
            options.Add(new Dropdown.OptionData(creature.Name, creature.CreatureSprite));;
        }

        return options;
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
        
        _playerSlots[0].value -= 1; // Not really necessary, just leaves options in place when returning to menu.
        _npcSlots[0].value -= 1;
    }

    public Creaturedex CreatureDex => _creatureDex;

    public List<CreatureBase> PlayerCreatures => _playerCreatures;

    public List<CreatureBase> NpcCreatures => _npcCreatures;

    public Dropdown[] PlayerSlots => _playerSlots;

    public Dropdown[] NpcSlots => _npcSlots;
}

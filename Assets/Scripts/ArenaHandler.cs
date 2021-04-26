using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaHandler : MonoBehaviour
{
    public Player HumanPlayer;
    public Player NPCPlayer;

    public PocketHUD NPCHud;
    public PocketHUD PlayerPocketHud;

    [SerializeField] private GameObject _creaturePrefab;
    [SerializeField] private CreatureBase[] _playerCreatures;
    [SerializeField] private CreatureBase[] _npcCreatures;


    public Transform PlayerSpawn;
    public Transform NPCSpawn;

    void Start()
    {
        HumanPlayer = new Player{};
        HumanPlayer.Name = "Human";
        NPCPlayer = new Player{};
        NPCPlayer.Name = "NPC";

        var playerCreatureGO = Instantiate(_creaturePrefab, PlayerSpawn.position, Quaternion.identity);
        playerCreatureGO.AddComponent<PlayerInputKeyboard>();
        var enemyCreatureGO = Instantiate(_creaturePrefab, NPCSpawn.position, Quaternion.identity);
        //var ai = enemyCreatureGO.AddComponent<BasicAI>();
        //ai.Player = playerCreatureGO;

        HumanPlayer.SetPocketHUD(PlayerPocketHud);
        NPCPlayer.SetPocketHUD(NPCHud);

        var playerCreature = playerCreatureGO.GetComponent<Creature>();

        var enemyCreature = enemyCreatureGO.GetComponent<Creature>();
        
        playerCreature.AssignPlayer(HumanPlayer);
        foreach (var cBase in _playerCreatures)
        {
            HumanPlayer.AddCreature(new CreatureData(cBase));
        }
        
        enemyCreature.AssignPlayer(NPCPlayer);
        foreach (var cBase in _npcCreatures)
        {
            NPCPlayer.AddCreature(new CreatureData(cBase));
        }

        playerCreature.Summon(HumanPlayer.SummonCreature(0));
        enemyCreature.Summon(NPCPlayer.SummonCreature(0));
    }
}

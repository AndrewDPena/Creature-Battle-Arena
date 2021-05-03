using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UserInterfaceScripts;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class ArenaHandler : MonoBehaviour
{
    public Player HumanPlayer;
    public Player NPCPlayer;

    public PocketHUD NPCHud;
    public PocketHUD PlayerPocketHud;

    [SerializeField] private GameObject _creaturePrefab;
    [SerializeField] private SummonNextCreatureWindow _summonNext;
    [SerializeField] private static List<CreatureBase> _playerCreatures = new List<CreatureBase>();
    [SerializeField] private static List<CreatureBase> _npcCreatures = new List<CreatureBase>();


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
        playerCreature.Handler = this;

        var enemyCreature = enemyCreatureGO.GetComponent<Creature>();
        enemyCreature.Handler = this;
        
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

    private void SummonNewCreature(Transform Spawn, Creature creature)
    {
        
    }

    private void NpcSummon()
    {
        
    }

    private void WinScreen()
    {
        
    }

    public void ReportCreatureFainted(Player player)
    {
        Debug.Log("Faint reported successfully.");
        if (player == NPCPlayer) {NpcSummon();}

        if (player.HasRemainingCreatures())
        {
            _summonNext.gameObject.SetActive(true);
            _summonNext.SetKeys(player);
        }
    }

    public static void SetData(List<CreatureBase> playerPocket, List<CreatureBase> npcPocket)
    {
        _playerCreatures = playerPocket;
        _npcCreatures = npcPocket;
        Debug.Log("This worked.");
    }
}

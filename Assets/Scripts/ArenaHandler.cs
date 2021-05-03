using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using GitHub.Unity;
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

    private Creature _playerCreature;
    private Creature _enemyCreature;

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

        _playerCreature = playerCreatureGO.GetComponent<Creature>();
        _playerCreature.Handler = this;

        _enemyCreature = enemyCreatureGO.GetComponent<Creature>();
        _enemyCreature.Handler = this;
        
        _playerCreature.AssignPlayer(HumanPlayer);
        foreach (var cBase in _playerCreatures)
        {
            HumanPlayer.AddCreature(new CreatureData(cBase));
        }
        
        _enemyCreature.AssignPlayer(NPCPlayer);
        foreach (var cBase in _npcCreatures)
        {
            NPCPlayer.AddCreature(new CreatureData(cBase));
        }

        _playerCreature.Summon(HumanPlayer.SummonCreature(0));
        _enemyCreature.Summon(NPCPlayer.SummonCreature(0));
    }

    private IEnumerator WaitForSummon()
    {
        while (_playerCreature.GetCreatureHealth() <= 0)
        {
                yield return new WaitForSeconds(0.1f);
        }
        _playerCreature.transform.position = PlayerSpawn.position;
        _summonNext.gameObject.SetActive(false);
    }

    private void NpcSummon()
    {
        
    }

    private void WinScreen()
    {
        
    }

    public void ReportCreatureFainted(Player player)
    {
        if (player == NPCPlayer) {NpcSummon();}

        if (player.HasRemainingCreatures())
        {
            _summonNext.gameObject.SetActive(true);
            _summonNext.SetKeys(player);
            _playerCreature.transform.position = new Vector3(100,100,100);
            StartCoroutine(WaitForSummon());
        }
    }

    public static void SetData(List<CreatureBase> playerPocket, List<CreatureBase> npcPocket)
    {
        _playerCreatures = playerPocket;
        _npcCreatures = npcPocket;
    }
}

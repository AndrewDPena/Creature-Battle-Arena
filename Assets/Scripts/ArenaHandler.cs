using System.Collections;
using System.Collections.Generic;
using UserInterfaceScripts;
using UnityEngine;

public class ArenaHandler : MonoBehaviour
{
    public ISummoner HumanPlayer;
    public ISummoner NPCPlayer;

    public PocketHUD NPCHud;
    public PocketHUD PlayerPocketHud;
    [SerializeField] private SummonNextCreatureWindow _swapKeyHud;
    [SerializeField] private AttackWindowHud _ctrlHud;
    [SerializeField] private AttackWindowHud _shiftHud;

    [SerializeField] private GameObject _creaturePrefab;
    [SerializeField] private SummonNextCreatureWindow _summonNext;
    [SerializeField] private BattleEndWindow _battleEnd;
    [SerializeField] private List<CreatureBase> _playerCreatures = new List<CreatureBase>();
    [SerializeField] private List<CreatureBase> _npcCreatures = new List<CreatureBase>();
    private static List<CreatureBase> _playerIncomingBases;
    private static List<CreatureBase> _npcIncomingBases;


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

        if (_playerIncomingBases != null)
        {
            _playerCreatures = _playerIncomingBases;
        }

        if (_npcIncomingBases != null)
        {
            _npcCreatures = _npcIncomingBases;
        }

        var playerCreatureGO = Instantiate(_creaturePrefab, PlayerSpawn.position, Quaternion.identity);
        playerCreatureGO.AddComponent<PlayerInputKeyboard>();
        var enemyCreatureGO = Instantiate(_creaturePrefab, NPCSpawn.position, Quaternion.identity);
        var ai = enemyCreatureGO.AddComponent<BasicAI>();
        ai.Player = playerCreatureGO;

        HumanPlayer.SetPocketHUD(PlayerPocketHud);
        NPCPlayer.SetPocketHUD(NPCHud);
        HumanPlayer.SetAttackHuds(_ctrlHud, _shiftHud);

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
        
        _swapKeyHud.SetKeys(HumanPlayer);
    }

    private IEnumerator WaitForSummon()
    {
        while (_playerCreature.GetCreatureHealth() <= 0)
        {
                yield return new WaitForSeconds(0.1f);
        }
        _playerCreature.transform.position = PlayerSpawn.position;
        _summonNext.gameObject.SetActive(false);
        _swapKeyHud.SetKeys(HumanPlayer);
    }

    private void NpcSummon()
    {
        if (!NPCPlayer.HasRemainingCreatures())
        {
            EndScreen(true);
        }
        else
        {
            _enemyCreature.transform.position = NPCSpawn.position;

            _enemyCreature.Swap(NPCPlayer.GetNextHealthyCreature());
        }
    }

    private void EndScreen(bool playerWonBattle)
    {
        _battleEnd.gameObject.SetActive(true);
        _battleEnd.SetOutcome(playerWonBattle);
    }

    public void ReportCreatureFainted(ISummoner player)
    {
        if (player == NPCPlayer) 
        {
            NpcSummon();
        }
        else if (!player.HasRemainingCreatures())
        {
            EndScreen(false);
        }
        else
        {
            _summonNext.gameObject.SetActive(true);
            _summonNext.SetKeys(HumanPlayer);
            _playerCreature.transform.position = new Vector3(100, 100, 100);
            StartCoroutine(WaitForSummon());
        }
    }

    public static void SetData(List<CreatureBase> playerPocket, List<CreatureBase> npcPocket)
    {
        _playerIncomingBases = playerPocket;
        _npcIncomingBases = npcPocket;
    }

    public List<CreatureBase> PlayerCreatures => _playerCreatures;

    public List<CreatureBase> NpcCreatures => _npcCreatures;
}

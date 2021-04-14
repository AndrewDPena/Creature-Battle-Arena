using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AttackTypes;

public class ArenaHandler : MonoBehaviour
{
    public Player HumanPlayer;
    public Player NPCPlayer;

    public PlayerHUD NPCHud;
    public PocketHUD PlayerPocketHud;

    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private GameObject _cylinderPrefab;

    [SerializeField] private CreatureBase[] _playerCreatures;


    public Transform PlayerSpawn;
    public Transform NPCSpawn;

    void Start()
    {
        HumanPlayer = new Player{};
        HumanPlayer.Name = "Human";
        NPCPlayer = new Player{};
        NPCPlayer.Name = "NPC";

        var playerCreatureGO = Instantiate(_cubePrefab, PlayerSpawn.position, Quaternion.identity);
        playerCreatureGO.AddComponent<PlayerInputKeyboard>();
        var enemyCreatureGO = Instantiate(_cylinderPrefab, NPCSpawn.position, Quaternion.identity);

        HumanPlayer.SetPocketHUD(PlayerPocketHud);
        NPCPlayer.SetHUD(NPCHud);

        var playerCreature = playerCreatureGO.GetComponent<Creature>();
        //var playerCreatureData = new CreatureData("Cubey", 10, 69);
        playerCreature.LearnAttack(new ConeAttackType());
        playerCreature.LearnAttack(new CenteredAttackType());
        
        /*var backupCreatureData = new CreatureData("Blocky", 5, 120);
        var backupCreatureData2 = new CreatureData("Testo", 5, 120);
        var backupCreatureData3 = new CreatureData("Testa", 5, 120);
        var backupCreatureData4 = new CreatureData("Paul", 5, 120);*/

        var enemyCreature = enemyCreatureGO.GetComponent<Creature>();
        var enemyCreatureData = new CreatureData("Cylindork", 5, 80);
        
        playerCreature.AssignPlayer(HumanPlayer);
        foreach (var cBase in _playerCreatures)
        {
            HumanPlayer.AddCreature(new CreatureData(cBase));
        }
        enemyCreature.AssignPlayer(NPCPlayer);
        NPCPlayer.AddCreature(enemyCreatureData);

        playerCreature.Summon(HumanPlayer.SummonCreature(0));
        enemyCreature.Summon(NPCPlayer.SummonCreature(0));
    }
}

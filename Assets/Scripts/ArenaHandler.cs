using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AttackTypes;

public class ArenaHandler : MonoBehaviour
{
    public Player HumanPlayer;
    public Player NPCPlayer;

    public PlayerHUD PlayerHud;
    public PlayerHUD NPCHud;

    public GameObject CreaturePrefab;

    public Transform PlayerSpawn;
    public Transform NPCSpawn;

    void Start()
    {
        HumanPlayer = new Player{};
        HumanPlayer.Name = "Human";
        NPCPlayer = new Player{};
        NPCPlayer.Name = "NPC";

        var playerCreatureGO = Instantiate(CreaturePrefab, PlayerSpawn.position, Quaternion.identity);
        playerCreatureGO.AddComponent<PlayerInputKeyboard>();
        var enemyCreatureGO = Instantiate(CreaturePrefab, NPCSpawn.position, Quaternion.identity);

        HumanPlayer.SetHUD(PlayerHud);
        NPCPlayer.SetHUD(NPCHud);

        var playerCreature = playerCreatureGO.GetComponent<Creature>();
        var playerCreatureData = new CreatureData("Cubey", 10, 69);
        //SetupCreature(playerCreature, "Cubey", 10, 69, HumanPlayer);
        playerCreature.LearnAttack(new ConeAttackType());
        playerCreature.LearnAttack(new CenteredAttackType());
        
        //var backupGO = new GameObject();
        //var backupCreature = backupGO.AddComponent<Creature>();
        //SetupCreature(backupCreature, "OtherCube", 10, 120, HumanPlayer);
        //playerCreature.LearnAttack(new ConeAttackType());
        var backupCreatureData = new CreatureData("Blocky", 5, 120);

        var enemyCreature = enemyCreatureGO.GetComponent<Creature>();
        var enemyCreatureData = new CreatureData("Cylindork", 5, 80);
        //SetupCreature(enemyCreature, "Cylindork", 5, 80, NPCPlayer);
        
        playerCreature.AssignPlayer(HumanPlayer);
        HumanPlayer.AddCreature(playerCreatureData);
        HumanPlayer.AddCreature(backupCreatureData);
        //HumanPlayer.AddCreature(backupCreature);
        enemyCreature.AssignPlayer(NPCPlayer);
        NPCPlayer.AddCreature(enemyCreatureData);

        playerCreature.Summon(HumanPlayer.SummonCreature(0));
        enemyCreature.Summon(NPCPlayer.SummonCreature(0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

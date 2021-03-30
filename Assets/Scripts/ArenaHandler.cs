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
        HumanPlayer = new Player();
        HumanPlayer.Name = "Human";
        NPCPlayer = new Player();
        NPCPlayer.Name = "NPC";

        var playerCreatureGO = Instantiate(CreaturePrefab, PlayerSpawn.position, Quaternion.identity);
        playerCreatureGO.AddComponent<PlayerInputKeyboard>();
        var enemyCreatureGO = Instantiate(CreaturePrefab, NPCSpawn.position, Quaternion.identity);

        HumanPlayer.SetHUD(PlayerHud);
        NPCPlayer.SetHUD(NPCHud);

        var playerCreature = playerCreatureGO.GetComponent<Creature>();
        SetupCreature(playerCreature, "Cubey", 10, 69, HumanPlayer);
        playerCreature.LearnAttack(new ConeAttackType());
        playerCreature.LearnAttack(new CenteredAttackType());

        var enemyCreature = enemyCreatureGO.GetComponent<Creature>();
        SetupCreature(enemyCreature, "Cylindork", 5, 80, NPCPlayer);
        
        HumanPlayer.AddCreature(playerCreature);
        NPCPlayer.AddCreature(enemyCreature);

        HumanPlayer.SummonCreature(0);
        NPCPlayer.SummonCreature(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private static void SetupCreature(Creature creature, string name, int strength, int maxHealth, Player player)
    {
        creature.Setup(name, strength, maxHealth, player);
    }
}

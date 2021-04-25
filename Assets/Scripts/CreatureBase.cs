using AttackList;
using UnityEngine;

[CreateAssetMenu(fileName = "Creature", menuName = "Creature/Create a new Creature")]
public class CreatureBase : ScriptableObject
{
    [SerializeField] string name;
    [SerializeField] int strength;
    [SerializeField] int maxHealth;
    [SerializeField] Sprite creatureSprite;
    [SerializeField] private AttackBase[] _attacks;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public int Strength
    {
        get { return strength; }
        set { strength = value; }
    }

    public int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    public Sprite CreatureSprite
    {
        get { return creatureSprite; }
        set { creatureSprite = value; }
    }

    public AttackBase[] Attacks
    {
        get { return _attacks; }
        set { _attacks = value; }
    }
}

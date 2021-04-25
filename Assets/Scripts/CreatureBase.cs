using AttackList;
using UnityEngine;

[CreateAssetMenu(fileName = "Creature", menuName = "Creature/Create a new Creature")]
public class CreatureBase : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private int _strength;
    [SerializeField] private int _maxHealth;
    [SerializeField] private Sprite _creatureSprite;
    [SerializeField] private AttackBase[] _attacks;

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public int Strength
    {
        get { return _strength; }
        set { _strength = value; }
    }

    public int MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    public Sprite CreatureSprite
    {
        get { return _creatureSprite; }
        set { _creatureSprite = value; }
    }

    public AttackBase[] Attacks
    {
        get { return _attacks; }
        set { _attacks = value; }
    }
}

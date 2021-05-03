using System;
using static TypeChart;
using AttackManagement;
using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "Creature", menuName = "Creature/Create a new Creature")]
public class CreatureBase : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private int _strength;
    [SerializeField] private int _maxHealth;
    [SerializeField] private float _creatureSpeed;
    [SerializeField] private CreatureType _creatureType1;
    [SerializeField] private CreatureType _creatureType2;
    [SerializeField] private Sprite _creatureSprite;
    [SerializeField] private List<AttackBase> _attacks = new List<AttackBase>();

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

    public float CreatureSpeed
    {
        get { return _creatureSpeed; }
        set { _creatureSpeed = value; }
    }

    public CreatureType CreatureType1
    {
        get { return _creatureType1; }
        set { _creatureType1 = value; }
    }

    public CreatureType CreatureType2
    {
        get { return _creatureType2; }
        set { _creatureType2 = value; }
    }

    public Sprite CreatureSprite
    {
        get { return _creatureSprite; }
        set { _creatureSprite = value; }
    }
    
    public List<AttackBase> Attacks
    {
        get { return _attacks; }
        set { _attacks = value; }
    }
}

using System;
using UnityEngine;

public class Creature : MonoBehaviour, ICreature
{
    public CreatureData CurrentCreature;
    //private Sprite _sprite;
    private SpriteRenderer _renderer;
    [SerializeField] private Transform[] _exitPoints;
    [SerializeField] private GameObject _attack1Prefab;
    [SerializeField] private GameObject _attack2Prefab;
    [SerializeField] private IAttackType _attack1;
    [SerializeField] private IAttackType _attack2;

    public Player Owner;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void AssignPlayer(Player player)
    {
        Owner = player;
    }

    public void Summon(CreatureData creature)
    {
        CurrentCreature = creature;
        _renderer.sprite = creature.Sprite;
    }

    public void Swap(int slot)
    {
        if (Owner.CanSummonCreature(slot))
        {
            Summon(Owner.SummonCreature(slot));
        }
    }

    public void TakeDamage(int damage)
    {
        CurrentCreature.TakeDamage(damage);
        Owner.UpdateHUD(CurrentCreature);
    }

    // Change to one method with a dictionary or something, potentially
    public void Attack1(Vector2 direction)
    {
        StartCoroutine(_attack1.Attack(direction, _exitPoints, _attack1Prefab));
    }

    public void Attack2(Vector2 direction)
    {
        StartCoroutine(_attack2.Attack(direction, _exitPoints, _attack2Prefab));
    }

    public void LearnAttack(IAttackType attackType)
    {
        if (_attack1 == null)
        {
            _attack1 = attackType;
        }
        else
        {
            _attack2 = attackType;
        }
    }
}
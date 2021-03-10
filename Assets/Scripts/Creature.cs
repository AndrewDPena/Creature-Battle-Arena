using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;

public class Creature : MonoBehaviour
{
    public string Name;
    public int Strength;
    public int MaxHealth;
    public int CurrentHealth;
    [SerializeField] 
    private GameObject _attackPrefab;
    [SerializeField] 
    private Transform[] _exitPoints;
    private Player _owner;

    public void Setup(string creatureName, int strength, int maxHealth, Player player)
    {
        Name = creatureName;
        Strength = strength;
        MaxHealth = maxHealth;
        CurrentHealth = MaxHealth;
        _owner = player;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        _owner.UpdateHUD(this, CurrentHealth);
    }

    public void Attack(Vector2 direction)
    {
        StartCoroutine(BreatheFire(direction));
    }

    private IEnumerator BreatheFire(Vector2 direction)
    {
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        var cast = angle - (angle % 45);
        var point = (int) (cast / 45);
        if (point < 0)
        {
            point += _exitPoints.Length;
        }
        var breath = Instantiate(_attackPrefab, _exitPoints[point].position, Quaternion.identity);


        breath.transform.rotation = Quaternion.AngleAxis(cast, Vector3.forward);

        yield return new WaitForSeconds(.25f);
        
        Destroy(breath);
    }
}
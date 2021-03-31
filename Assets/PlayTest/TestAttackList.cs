using System.Collections;
using System.Numerics;
using AttackList;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace PlayTest
{
    public class TestAttackList : MonoBehaviour
    {
        private GameObject _gameObject;
        private Creature _creature;
        private Player _player;

        [SetUp]
        public void Setup()
        {
            _gameObject = Instantiate(new GameObject());
            _player = new Player();
            _creature = Instantiate(Resources.Load<Creature>("Prefabs/CreaturePrefab"), 
                Vector3.zero, Quaternion.identity);
            _creature.Setup("Test", 0, 100, _player);
        }
        
        [TearDown]
        public void Teardown()
        {
            foreach (GameObject o in Object.FindObjectsOfType<GameObject>()) {
                Destroy(o);
            }
        }

        [UnityTest]
        public IEnumerator FireBreathDamagesCreature()
        {
            var health = _creature.CurrentHealth;
            var attack = Instantiate(Resources.Load<FireBreath>("Prefabs/FireBreath"), 
                new Vector3(10, 10, 0), Quaternion.identity);
            
            yield return new WaitForSeconds(0.1f);
            Destroy(attack);
            
            Assert.AreEqual(health, _creature.CurrentHealth, 
                "Creature does not take damage when not in contact with FireBreath.");

            var attack2 = Instantiate(Resources.Load<FireBreath>("Prefabs/FireBreath"), 
                Vector3.zero, Quaternion.identity);
            
            yield return new WaitForSeconds(0.1f);
            
            Destroy(attack2);
            
            Assert.Less(_creature.CurrentHealth, health,
                "Creature does take damage when in contact with FireBreath.");
        }
        
        [UnityTest]
        public IEnumerator ExplosionDamagesCreature()
        {
            var health = _creature.CurrentHealth;
            var attack = Instantiate(Resources.Load<Explosion>("Prefabs/Explosion"), 
                new Vector3(10, 10, 0), Quaternion.identity);
            
            yield return new WaitForSeconds(0.1f);
            Destroy(attack);
            
            Assert.AreEqual(health, _creature.CurrentHealth, 
                "Creature does not take damage when not in contact with Explosion.");

            var attack2 = Instantiate(Resources.Load<Explosion>("Prefabs/Explosion"), 
                Vector3.zero, Quaternion.identity);
            
            yield return new WaitForSeconds(0.1f);
            
            Destroy(attack2);
            
            Assert.Less(_creature.CurrentHealth, health,
                "Creature does take damage when in contact with Explosion.");
        }
    }
}
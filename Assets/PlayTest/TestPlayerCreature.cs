using System.Collections;
using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using UnityEngine.TestTools;

namespace PlayTest
{
    
    public class TestPlayerCreature
    {
        private GameObject _gameObject;
        private Player _player;
        private Creature _creature;
        private CreatureData _data;
        private PlayerHUD _playerHud;
        private SpriteRenderer _renderer;
        
        [SetUp]
        public void Setup()
        {
            _gameObject = GameObject.Instantiate(new GameObject());
            _player = new Player();
            _playerHud = GameObject.Instantiate(Resources.Load<PlayerHUD>("Prefabs/PlayerHudPrefab"), Vector3.zero, Quaternion.identity);
            _player.SetHUD(_playerHud);
            _creature = _gameObject.AddComponent<Creature>();
            _renderer = _gameObject.AddComponent<SpriteRenderer>();
            _data = new CreatureData("Test", 10, 100);
        }

        [TearDown]
        public void Teardown()
        {
            GameObject.Destroy(_gameObject);
        }
        
        [UnityTest]
        public IEnumerator PlayerCreatureInstantiates()
        {
            yield return new WaitForSeconds(0.1f);
            
            Assert.NotNull(_creature, "Creature instantiates correctly.");
        }
        
        [UnityTest]
        public IEnumerator PlayerCreatureCanAssignAPlayer()
        {
            
            yield return new WaitForSeconds(0.1f);
            Assert.Null(_creature.Owner, "Player starts null.");
            
            _creature.AssignPlayer(_player);
            yield return new WaitForSeconds(0.1f);

            
            Assert.AreEqual(_player, _creature.Owner, "Creature assigns its owner correctly.");
        }
        
        [UnityTest]
        public IEnumerator PlayerCreatureCanCummonACreature()
        {
            
            yield return new WaitForSeconds(0.1f);
            Assert.Null(_creature.CurrentCreature, "CurrentCreature starts null.");
            
            _creature.Summon(_data);
            yield return new WaitForSeconds(0.1f);

            
            Assert.AreEqual(_data, _creature.CurrentCreature, "Creature assigns data object correctly.");
        }
        
        [UnityTest]
        public IEnumerator PlayerCreatureTakesDamage()
        {
            _creature.Summon(_data);
            _creature.AssignPlayer(_player);
            var health = _creature.CurrentCreature.CurrentHealth;
            var damage = 10;
            _creature.TakeDamage(damage);
            
            yield return new WaitForSeconds(0.1f);
            
            Assert.AreEqual(health - damage, _creature.CurrentCreature.CurrentHealth, 
                "Creature passes along the correct amount of damage.");
        }

        [UnityTest]
        public IEnumerator PlayerCreatureAttacks()
        {
            var direction = new Vector2(1,1);
            var testAttack = Substitute.For<IAttackType>();
            _creature.LearnAttack(testAttack);
            _creature.Attack1(direction);
            
            yield return new WaitForSeconds(0.1f);

            testAttack.ReceivedWithAnyArgs().Attack(default(Vector2), default(Transform[]), default(GameObject));
            Debug.Log("Creature calls the attack coroutine successfully.");
        }

        [UnityTest]
        public IEnumerator CreatureCanSwap()
        {
            yield return new WaitForSeconds(0.1f);
            var newTest = new CreatureData("NewTest", 100, 100);
            _creature.AssignPlayer(_player);
            _player.AddCreature(_data);
            _player.AddCreature(newTest);
            _creature.Summon(_data);

            
            _creature.Swap(2);
            yield return new WaitForSeconds(0.1f);
            
            Assert.AreEqual(_creature.CurrentCreature, _data, "Creature doesn't swap to bad swap values.");
            
            _creature.Swap(1);
            yield return new WaitForSeconds(0.1f);
            
            Assert.AreEqual(_creature.CurrentCreature, newTest, "Creature Swaps on good value.");
        }
    }
}
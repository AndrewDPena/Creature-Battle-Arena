using System.Collections;
using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace PlayTest
{
    public class TestPlayerCreature
    {
        private GameObject _gameObject;
        private Player _player;
        private Creature _creature;
        
        [SetUp]
        public void Setup()
        {
            _gameObject = GameObject.Instantiate(new GameObject());
            _player = new Player();
            _creature = _gameObject.AddComponent<Creature>();
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
        public IEnumerator PlayerCreatureSpawnsWithDefaultValues()
        {
            yield return new WaitForSeconds(0.1f);
            Assert.Null(_creature.Name, "Name is null.");
            Assert.AreEqual(0, _creature.Strength, "Strength is 0.");
            Assert.AreEqual(0, _creature.CurrentHealth, "CurrentHealth is 0.");
            Assert.AreEqual(0, _creature.MaxHealth, "MaxHealth is 0.");
        }
        
        [UnityTest]
        public IEnumerator PlayerCreatureSetsValuesCorrectly()
        {
            _creature.Setup("Name", 1, 2, _player);
            yield return new WaitForSeconds(0.1f);
            
            Assert.AreEqual(2, _creature.MaxHealth,  
                "Creature sets the second int to max health.");
            Assert.AreEqual("Name", _creature.Name,
                "Creature sets the first param to be its Name.");
        }
        
        [UnityTest]
        public IEnumerator PlayerCreatureTakesDamage()
        {
            _creature.Setup("Name", 1, 20, _player);
            yield return new WaitForSeconds(0.1f);
            var newHealth = _creature.MaxHealth - 10;
            _creature.TakeDamage(10);
            yield return new WaitForSeconds(0.1f);
            
            Assert.AreEqual(newHealth, _creature.CurrentHealth, "Creature updates current health when taking damage.");
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
    }
}
using System.Collections;
using AttackManagement;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace PlayTest
{ 
    public class TestDamageManager : MonoBehaviour
    {
        private GameObject _gameObject;
        private DamageManager _manager;

        [SetUp]
        public void Setup()
        {
            _gameObject = Instantiate(new GameObject());
            _manager = _gameObject.AddComponent<DamageManager>();
        }

        [TearDown]
        public void Teardown()
        {
            foreach (var o in FindObjectsOfType<GameObject>())
            {
                Destroy(o);
            }
        }

        [UnityTest]
        public IEnumerator DamageManagerHasNoAttackSetByDefault()
        {
            yield return new WaitForSeconds(0.1f);
            
            Assert.AreEqual(0, _manager.BattlePower, "Battle Power is 0 by default."); //Shouldn't it be null?
        }

        [UnityTest]
        public IEnumerator DamageManagerSetsAnAttackCorrectly()
        {
            var test = Instantiate(Resources.Load<AttackBase>("Prefabs/Attacks/TestAttack"));
            _manager.SetAttack(test);
            
            yield return new WaitForSeconds(0.1f);
            
            Assert.AreEqual(test.Damage, _manager.BattlePower, "Damage gets set correctly.");
            Assert.AreEqual(test.DamagePerTick, _manager.DamagePerTick, "Damage per tick gets set correctly.");
            Assert.AreEqual(test.CanDamageSelf, _manager.CanDamageSelf, "Damageself gets set correctly.");
            Assert.AreEqual(test.AttackType, _manager.AttackType, "Damage gets set correctly.");
        }

        [UnityTest]
        public IEnumerator DamageManagerHasNoCreatureByDefault()
        {
            yield return new WaitForSeconds(0.1f);
            
            Assert.Null(_manager.Attacker, "Attacker is null by default.");
        }

        [UnityTest]
        public IEnumerator DamageManagerSetsAnAttackerCorrectly()
        {
            var test = Instantiate(Resources.Load<Creature>("Prefabs/CreaturePrefab"));
            _manager.SetAttacker(test);
            
            yield return new WaitForSeconds(0.1f);
            
            Assert.AreEqual(test, _manager.Attacker, "Attacker gets set correctly.");
        }

        [UnityTest]
        public IEnumerator ManagerPreventsSelfDamageCorrectly()
        {
            var creature = Instantiate(Resources.Load<Creature>("Prefabs/CreaturePrefab"), 
                Vector3.zero, Quaternion.identity);
            var player = Substitute.For<ISummoner>();
            creature.AssignPlayer(player);
            yield return new WaitForSeconds(0.1f);

            var testCreatureBase = Instantiate(Resources.Load<CreatureBase>("Prefabs/Creatures/000 Testo"));
            creature.Summon(new CreatureData(testCreatureBase));

            var creatureStartingHealth = creature.GetCreatureHealth();
            creature.Attack1(Vector2.zero);
            
            yield return new WaitForSeconds(0.1f);
            
            Assert.AreEqual(creatureStartingHealth, creature.GetCreatureHealth(), 
                "Creature did not take damage from its own attack.");
        }
        
        [UnityTest]
        public IEnumerator ManagerAllowsSelfDamageCorrectly()
        {
            var creature = Instantiate(Resources.Load<Creature>("Prefabs/CreaturePrefab"), 
                Vector3.zero, Quaternion.identity);
            var player = Substitute.For<ISummoner>();
            creature.AssignPlayer(player);
            yield return new WaitForSeconds(0.1f);

            var testCreatureBase = Instantiate(Resources.Load<CreatureBase>("Prefabs/Creatures/000 Testo"));
            creature.Summon(new CreatureData(testCreatureBase));

            var creatureStartingHealth = creature.GetCreatureHealth();
            creature.Attack2(Vector2.zero);
            
            yield return new WaitForSeconds(0.1f);
            
            Assert.Less( creature.GetCreatureHealth(), creatureStartingHealth, 
                "Creature did not take damage from its own attack.");
        }
    }
}
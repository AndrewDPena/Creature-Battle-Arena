using System.Collections;
using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace PlayTest
{
    public class TestCreatureData
    {
        private CreatureData _creature;
        
        [SetUp]
        public void Setup()
        {
            _creature = new CreatureData("Test", 10, 100);
        }

        [TearDown]
        public void Teardown()
        {
            
        }

        [UnityTest]
        public IEnumerator CreatureInitializesCorrectly()
        {
            yield return new WaitForSeconds(0.1f);
            
            Assert.NotNull(_creature, "Creature instantiates.");
            Assert.AreEqual("Test", _creature.Name, "Creature name is set on creation");
            Assert.AreEqual(100, _creature.CurrentHealth, 
                "Creature sets Current, and therefore Max, health correctly.");
        }

        [UnityTest]
        public IEnumerator CreatureTakesDamage()
        {
            var health = _creature.CurrentHealth;
            var damage = 10;
            _creature.TakeDamage(damage);
            
            yield return new WaitForSeconds(0.1f);
            
            Assert.AreEqual(health - damage, _creature.CurrentHealth, "Creature takes the correct amount of damage.");
        }
    }
}
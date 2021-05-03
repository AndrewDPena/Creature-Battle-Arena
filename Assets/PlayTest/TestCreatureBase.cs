using System.Collections;
using System.Collections.Generic;
using AttackManagement;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using static TypeChart;

namespace PlayTest
{
    public class TestCreatureBase
    {
        private CreatureBase _creatureBase;

        [SetUp]
        public void Setup()
        {
            _creatureBase = ScriptableObject.CreateInstance<CreatureBase>();
            _creatureBase.Name = "Test";
            _creatureBase.Strength = 10;
            _creatureBase.MaxHealth = 100;
            _creatureBase.CreatureSpeed = 1000;
            _creatureBase.CreatureType1 = CreatureType.Normal;
            _creatureBase.CreatureType2 = CreatureType.None;
            _creatureBase.CreatureSprite = Resources.Load<Sprite>("Prefabs/TestSprite");
            _creatureBase.Attacks = new List<AttackBase>();
        }

        [TearDown]
        public void Teardown()
        {
            GameObject.Destroy(_creatureBase);
        }

        [UnityTest]
        public IEnumerator CreatureBaseCanGetName()
        {
            var creatureName = _creatureBase.Name;
            
            yield return new WaitForSeconds(0.1f);
            
            Assert.AreEqual("Test", creatureName, "The get methods for Creature Base properties work correctly.");
        }
        
        [UnityTest]
        public IEnumerator CreatureBaseCanGetTypes()
        {
            var creatureType1 = _creatureBase.CreatureType1;
            
            yield return new WaitForSeconds(0.1f);
            
            Assert.AreEqual(CreatureType.Normal, creatureType1, 
                "The get methods for Creature Base properties also work on enum properties.");
        }
    }
}
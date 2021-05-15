using System.Collections;
using AttackAreas;
using AttackManagement;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using static TypeChart;

namespace PlayTest
{
    public class TestAttackBase
    {
        private AttackBase _attackBase;

        [SetUp]
        public void Setup()
        {
            _attackBase = GameObject.Instantiate(Resources.Load<AttackBase>("Prefabs/Attacks/TestAttack"));  
        }

        [TearDown]
        public void Teardown()
        {
            GameObject.Destroy(_attackBase);
        }

        [UnityTest]
        public IEnumerator AttackCanGetName()
        {
            var attackName = _attackBase.Name;
            
            yield return new WaitForSeconds(0.1f);
            
            Assert.AreEqual("Test", attackName, "Can get the name from an AttackBase instance.");
        }
        
        [UnityTest]
        public IEnumerator AttackCanGetDamage()
        {
            var attackDamage = _attackBase.Damage;
            
            yield return new WaitForSeconds(0.1f);
            
            Assert.AreEqual(20, attackDamage, "Correctly gets attack damage.");
        }
        
        [UnityTest]
        public IEnumerator AttackCanGetDamagePerTick()
        {
            var attackDamagePerTick = _attackBase.DamagePerTick;
            
            yield return new WaitForSeconds(0.1f);
            
            Assert.AreEqual(5, attackDamagePerTick, "Correctly gets attack damage per tick.");
        }
        
        [UnityTest]
        public IEnumerator AttackCanCheckSelfDamageBoolean()
        {
            var damageSelf = _attackBase.CanDamageSelf;
            
            yield return new WaitForSeconds(0.1f);
            
            Assert.False(damageSelf, "Boolean value of test attack is correctly read.");
        }
        
        [UnityTest]
        public IEnumerator AttackCanGetType()
        {
            var attackType = _attackBase.AttackType;
            
            yield return new WaitForSeconds(0.1f);
            
            Assert.AreEqual(CreatureType.Normal, attackType, "Correctly gets attack type.");
        }
        
        [UnityTest]
        public IEnumerator AttackCanGetAreaOfEffect()
        {
            var attackArea = _attackBase.GetAreaOfEffect();
            
            yield return new WaitForSeconds(0.1f);
            
            Assert.AreEqual(typeof(CenteredAttack), attackArea.GetType(), "Correctly gets attack area type.");
        }
    }
}
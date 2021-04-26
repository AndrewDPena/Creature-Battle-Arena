using System.Collections;
using NSubstitute.Extensions;
using NUnit.Framework;
using static TypeChart;
using UnityEngine;
using UnityEngine.TestTools;

namespace PlayTest
{
    public class TestTypeChart
    {
        private float _modifier;
        
        [SetUp]
        public void Setup()
        {

        }

        [TearDown]
        public void Teardown()
        {
            
        }

        [UnityTest]
        public IEnumerator NoneTypeAttackHasModifierOf0()
        {
            _modifier = DamageMult[CreatureType.None][CreatureType.Fire];
            
            yield return new WaitForSeconds(0.1f);

            Assert.AreEqual(0f, _modifier, "None Type attacks return a modifier of 0.");
        }
        
        [UnityTest]
        public IEnumerator NeutralAttackHasModifierOf1()
        {
            _modifier = DamageMult[CreatureType.Normal][CreatureType.Fire];
            
            yield return new WaitForSeconds(0.1f);

            Assert.AreEqual(1f, _modifier, "Neutral attacks return a modifier of 1.");
        }
        
        [UnityTest]
        public IEnumerator EffectiveAttackHasModifierOf2()
        {
            _modifier = DamageMult[CreatureType.Water][CreatureType.Fire];
            
            yield return new WaitForSeconds(0.1f);

            Assert.AreEqual(2f, _modifier, "Super Effective attacks return a modifier of 2.");
        }
    }
}
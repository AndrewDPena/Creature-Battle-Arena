using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PlayTest
{
    public class TestPlayerHud
    {
        private PlayerHUD _hud;
        private CreatureData _creature;
        
        [SetUp]
        public void Setup()
        {
            _hud = GameObject.Instantiate(Resources.Load<PlayerHUD>("Prefabs/PlayerHudPrefab"), Vector3.zero, Quaternion.identity);
        }

        [TearDown]
        public void Teardown()
        {
            GameObject.Destroy(_hud);
        }

        [UnityTest]
        public IEnumerator HudInitializesNullValuesWithEmptyCreature()
        {
            _hud.InitializeHUD(_creature);
            
            yield return new WaitForSeconds(0.1f);
            
            Assert.AreEqual("", _hud.Name.text, "Name is an empty string if the creature is null.");
        }
        
        [UnityTest]
        public IEnumerator HudInitializesValuesCorrectlyWithInstanceOfCreature()
        {
            _creature = new CreatureData("Test", 10, 10);
            _hud.InitializeHUD(_creature);
            
            yield return new WaitForSeconds(0.1f);
            
            Assert.AreEqual("Test", _hud.Name.text, "Name is correctly set when the creature has data.");
        }
    }
}
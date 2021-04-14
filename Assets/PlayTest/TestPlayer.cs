using System.Collections;
using System.Data;
using System.Reflection;
using NSubstitute;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace PlayTest
{

    public class TestPlayer : MonoBehaviour
    {
        private GameObject _gameObject;
        private PocketHUD _pocketHud;
        private PlayerHUD _playerHud;
        private Player _player;


        [SetUp]
        public void Setup()
        {
            _gameObject = Instantiate(new GameObject());
            _pocketHud = _gameObject.AddComponent<PocketHUD>();
            _player = new Player();
            _playerHud = Instantiate(Resources.Load<PlayerHUD>("Prefabs/PlayerHudPrefab"), Vector3.zero, Quaternion.identity);
            _pocketHud.AddHud(_playerHud);
            _player.SetPocketHUD(_pocketHud);
        }

        [TearDown]
        public void Teardown()
        {
            foreach (GameObject o in Object.FindObjectsOfType<GameObject>()) {
                Destroy(o);
            }
        }

        [UnityTest]
        public IEnumerator PlayerCreates()
        {
            yield return new WaitForSeconds(0.1f);

            Assert.IsNotNull(_player, "Player is constructed correctly.");
        }

        [UnityTest]
        public IEnumerator PlayerCanAssignAHud()
        {

            yield return new WaitForSeconds(0.1f);

            Assert.AreEqual(_playerHud, _player.GetType().GetField("_hud", BindingFlags.NonPublic | 
                                                                           BindingFlags.Instance)?.GetValue(_player), 
                "Hud Attaches Correctly.");
        }

        [UnityTest]
        public IEnumerator PlayerCanStoreACreature()
        {
            Assert.AreEqual(0, _player.GetPocketSize(), "Player starts with an empty pocket.");
            _player.AddCreature(new CreatureData("Test", 10, 10));
            
            yield return new WaitForSeconds(0.1f);
            
            Assert.AreEqual(1, _player.GetPocketSize(), "Player pocket increases when creature is added.");
        }

        [UnityTest]
        public IEnumerator PlayerCanSummonACreature()
        {
            var creature = new CreatureData("Test", 10, 10);
            _player.AddCreature(creature);
            yield return new WaitForSeconds(0.1f);
            Assert.AreNotEqual(creature, _player.GetActiveCreature(), "Player starts without an active creature.");

            _player.SummonCreature(0);
            yield return new WaitForSeconds(0.1f);
            
            Assert.AreEqual(creature, _player.GetActiveCreature(), "Player successfully summons creature.");
        }

        [UnityTest]
        public IEnumerator PlayerCanUpdateHud()
        {
            var creature = new CreatureData("Test", 0, 100);
            _player.AddCreature(creature);
            _player.SummonCreature(0);
            
            yield return new WaitForSeconds(0.1f);
            Assert.AreEqual(100, _playerHud.GetHealth(), "Hud updates to summoned creature.");
            
            creature.TakeDamage(10);
            
            _player.UpdateHUD(creature);            
            yield return new WaitForSeconds(0.1f);
            Assert.AreEqual(90, _playerHud.GetHealth(), "Player can update HUD values.");

        }
    }
}
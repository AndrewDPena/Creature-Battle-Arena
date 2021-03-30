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
        private PlayerHUD _playerHud;
        private Creature _creature;
        private Player _player;


        [SetUp]
        public void Setup()
        {
            _gameObject = GameObject.Instantiate(new GameObject());
            _creature = _gameObject.AddComponent<Creature>();
            //_playerHud = _gameObject.AddComponent<PlayerHUD>();
            _player = new Player();
            _playerHud = Instantiate(Resources.Load<PlayerHUD>("Prefabs/PlayerHudPrefab"), Vector3.zero, Quaternion.identity);
            _player.SetHUD(_playerHud);
        }

        [TearDown]
        public void Teardown()
        {
            GameObject.Destroy(_gameObject);
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
            // _player.SetHUD(_playerHud);

            yield return new WaitForSeconds(0.1f);

            Assert.AreEqual(_playerHud, _player.GetType().GetField("_hud", BindingFlags.NonPublic | 
                                                                           BindingFlags.Instance)?.GetValue(_player), 
                "Hud Attaches Correctly.");
        }

        [UnityTest]
        public IEnumerator PlayerCanStoreACreature()
        {
            Assert.AreEqual(0, _player.GetPocketSize(), "Player starts with an empty pocket.");
            _player.AddCreature(_creature);
            
            yield return new WaitForSeconds(0.1f);
            
            Assert.AreEqual(1, _player.GetPocketSize(), "Player pocket increases when creature is added.");
        }

        [UnityTest]
        public IEnumerator PlayerCanSummonACreature()
        {
            _player.AddCreature(_creature);
            yield return new WaitForSeconds(0.1f);
            Assert.AreNotEqual(_creature, _player.GetActiveCreature(), "Player starts without an active creature.");

            _player.SummonCreature(0);

            yield return new WaitForSeconds(0.1f);
            
            Assert.AreEqual(_creature, _player.GetActiveCreature(), "Player successfully summons creature.");
        }

        [UnityTest]
        public IEnumerator PlayerCanUpdateHud()
        {
            _creature.Setup("Test", 0, 100, _player);
            _player.AddCreature(_creature);
            _player.SummonCreature(0);
            
            yield return new WaitForSeconds(0.1f);
            Assert.AreEqual(100, _playerHud.GetHealth(), "Hud updates to summoned creature.");
            
            _player.UpdateHUD(_creature, 10);            
            yield return new WaitForSeconds(0.1f);
            Assert.AreEqual(10, _playerHud.GetHealth(), "Player can update HUD values.");

        }
    }
}
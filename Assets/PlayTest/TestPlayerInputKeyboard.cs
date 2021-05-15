using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.TestTools;

namespace PlayTest
{
    public class TestPlayerInputKeyboard : MonoBehaviour
    {
        private GameObject _gameObject;
        private PlayerInputKeyboard _input;
        //private Rigidbody2D _body;
        private ISummoner _player;
        private CreatureMove _move;
        private Creature _creature;
        private IUnityService _unityService;
        
        [SetUp]
        public void Setup()
        {
            _gameObject = Instantiate(Resources.Load<GameObject>("Prefabs/CreaturePrefab"), 
                Vector3.zero, Quaternion.identity);
            _creature = _gameObject.GetComponent<Creature>();
            _player = Substitute.For<ISummoner>();
            _creature.AssignPlayer(_player);
            _move = _gameObject.GetComponent<CreatureMove>();
            _input = _gameObject.AddComponent<PlayerInputKeyboard>();
            _move.SetCreatureSpeed(1000.0f);
            _unityService = Substitute.For<IUnityService>();
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
        public IEnumerator PlayerInputKeyboardHasUnityService()
        {
            yield return new WaitForSeconds(0.1f);
            
            Assert.NotNull(_input.UnityService, "PlayerInputKeyboard has a UnityService.");
        }
        
        [UnityTest]
        public IEnumerator PlayerInputKeyboardDoesNotTriggerMovementWithoutInput()
        {
            Vector2 position = _move.transform.position;
            
            yield return new WaitForSeconds(0.5f);

            Vector2 newPosition = _move.transform.position;
            
            Assert.AreEqual(position, newPosition, 
                "Move Test Passed, PlayerInputKeyboard does not call movement without input.");
        }
        
        [UnityTest]
        public IEnumerator PlayerInputKeyboardCallsMovementOnInput()
        {
            Vector2 position = _move.transform.position;
            _unityService.GetAxis("Horizontal").Returns(1);
            _unityService.GetDeltaTime().Returns(1);

            _input.UnityService = _unityService; 
            
            yield return new WaitForSeconds(0.5f);

            Vector2 newPosition = _move.transform.position;
            
            Assert.AreNotEqual(position.x, newPosition.x, 
                "Move Test Passed, PlayerInputKeyboard calls movement with input.");
        }
        
        [UnityTest]
        public IEnumerator PlayerInputKeyboardCallsNoMovementWithoutInput()
        {
            Vector2 position = _move.transform.position;
            _unityService.GetAxis("Horizontal").Returns(0);
            _unityService.GetDeltaTime().Returns(1);

            _input.UnityService = _unityService; 
            
            yield return new WaitForSeconds(0.5f);

            Vector2 newPosition = _move.transform.position;
            
            Assert.AreEqual(newPosition.x, position.x, 
                "Move Test Passed, PlayerInputKeyboard calls zero movement with zero vector.");
        }

        [UnityTest]
        public IEnumerator CreatureAttachedToInputIsNotNull()
        {
            yield return new WaitForSeconds(0.1f);
            
            Assert.NotNull(_creature, "The creature object exists and is attached to the input.");
        }
        
        [UnityTest]
        public IEnumerator PlayerInputCallsAttackWithCtrl()
        {
            _unityService.GetKeyDown("left shift").Returns(false);
            _unityService.GetKeyDown("left ctrl").Returns(true);

            var testCreature = Instantiate(Resources.Load<CreatureBase>("Prefabs/Creatures/000 Testo"));
            _creature.Summon(new CreatureData(testCreature));

            _input.UnityService = _unityService;
            
            yield return new WaitForSeconds(0.1f);

            _unityService.Received().GetKeyDown("left shift");
            _unityService.Received().GetKeyDown("left ctrl");
            Debug.Log("PlayerInput calls the ctrl attack coroutine successfully.");
        }
        
        [UnityTest]
        public IEnumerator PlayerInputCallsAttackWithShift()
        {
            _unityService.GetKeyDown("left shift").Returns(true);
            _unityService.GetKeyDown("left ctrl").Returns(false);

            var testCreature = Instantiate(Resources.Load<CreatureBase>("Prefabs/Creatures/000 Testo"));
            _creature.Summon(new CreatureData(testCreature));

            _input.UnityService = _unityService;
            
            yield return new WaitForSeconds(0.1f);

            _unityService.Received().GetKeyDown("left shift");
            _unityService.Received().GetKeyDown("left ctrl");
            Debug.Log("PlayerInput calls the shift attack coroutine successfully.");
        }

        [UnityTest]
        public IEnumerator PlayerInputCallsASwap()
        {
            _unityService.InputString().Returns("abcde1fg");
            _input.UnityService = _unityService;
            _player.CanSummonCreature(1).Returns(true);
            var creatureData = new CreatureData("Test", 10, 10);
            _player.SummonCreature(1).Returns(creatureData);

            yield return new WaitForSeconds(0.1f);

            _player.Received().CanSummonCreature(1);
            _player.Received().SummonCreature(1);
            Debug.Log("The player received a swap call from the input string.");
        }
        
        /* This method swaps back and forth repeatedly, which is why it has failed/succeeded intermittently
        [UnityTest]
        public IEnumerator PlayerInputCallsASwap()
        {
            var slots = new List<string>(){"1", "2", "3", "4"};

            _unityService.InputString().Returns("abcde1fg");
            var pocketHud = _gameObject.AddComponent<PocketHUD>();
            pocketHud.AddHud(Instantiate(Resources.Load<PlayerHUD>("Prefabs/PlayerHudPrefab"), 
                Vector3.zero, Quaternion.identity));

            var player = new Player { };
            player.AddCreature(new CreatureData("test1", 10, 10));
            player.SetPocketHUD(pocketHud);
            _creature.AssignPlayer(player);
            _creature.Summon(player.SummonCreature(0));
            player.AddCreature(new CreatureData("test2", 10, 10));
            Assert.True(player.CanSummonCreature(1));
            
            _input.UnityService = _unityService;
            yield return new WaitForSeconds(0.1f);

            Assert.That(slots.Any(s => _unityService.InputString().Contains(s)));
            
            Assert.AreEqual("test2", player.GetActiveCreature().Name, "Player Input causes creatures to swap.");
        }*/
    }
}
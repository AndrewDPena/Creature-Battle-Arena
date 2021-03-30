using System.Collections;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PlayTest
{
    public class TestPlayerInputKeyboard
    {
        private GameObject _gameObject;
        private PlayerInputKeyboard _input;
        private Rigidbody2D _body;
        private CreatureMove _move;
        private Creature _creature;
        
        [SetUp]
        public void Setup()
        {
            _gameObject = GameObject.Instantiate(new GameObject());
            _input = _gameObject.AddComponent<PlayerInputKeyboard>();
            _body = _gameObject.AddComponent<Rigidbody2D>();
            _move = _gameObject.AddComponent<CreatureMove>();
            _move.CreatureSpeed = 1000.0f;
            _creature = _gameObject.AddComponent<Creature>();
        }

        [TearDown]
        public void Teardown()
        {
            GameObject.Destroy(_gameObject);
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
            var unityService = Substitute.For<IUnityService>();
            unityService.GetAxis("Horizontal").Returns(1);
            unityService.GetDeltaTime().Returns(1);

            _input.UnityService = unityService; 
            
            yield return new WaitForSeconds(0.5f);

            Vector2 newPosition = _move.transform.position;
            
            Assert.AreNotEqual(position.x, newPosition.x, 
                "Move Test Passed, PlayerInputKeyboard calls movement with input.");
        }
        
        [UnityTest]
        public IEnumerator PlayerInputKeyboardCallsNoMovementWithoutInput()
        {
            Vector2 position = _move.transform.position;
            var unityService = Substitute.For<IUnityService>();
            unityService.GetAxis("Horizontal").Returns(0);
            unityService.GetDeltaTime().Returns(1);

            _input.UnityService = unityService; 
            
            yield return new WaitForSeconds(0.5f);

            Vector2 newPosition = _move.transform.position;
            
            Assert.AreEqual(newPosition.x, position.x, 
                "Move Test Passed, PlayerInputKeyboard calls zero movement with zero vector.");
        }

        
        [UnityTest]
        public IEnumerator PlayerInputCallsAttackWithCtrl()
        {
            var unityService = Substitute.For<IUnityService>();
            unityService.GetKeyDown("left shift").Returns(false);
            unityService.GetKeyDown("left ctrl").Returns(true);


            var testAttack1 = Substitute.For<IAttackType>();
            var testAttack2 = Substitute.For<IAttackType>();
            _creature.LearnAttack(testAttack1);
            _creature.LearnAttack(testAttack2);

            _input.UnityService = unityService;
            
            yield return new WaitForSeconds(0.1f);

            testAttack1.ReceivedWithAnyArgs().Attack(default(Vector2), default(Transform[]), default(GameObject));
            testAttack2.DidNotReceiveWithAnyArgs().Attack(default(Vector2), default(Transform[]), default(GameObject));
            Debug.Log("PlayerInput calls the ctrl attack coroutine successfully.");
        }
        
        [UnityTest]
        public IEnumerator PlayerInputCallsAttackWithShift()
        {
            var unityService = Substitute.For<IUnityService>();
            unityService.GetKeyDown("left shift").Returns(true);
            unityService.GetKeyDown("left ctrl").Returns(false);


            var testAttack1 = Substitute.For<IAttackType>();
            var testAttack2 = Substitute.For<IAttackType>();
            _creature.LearnAttack(testAttack1);
            _creature.LearnAttack(testAttack2);

            _input.UnityService = unityService;
            
            yield return new WaitForSeconds(0.1f);

            testAttack1.DidNotReceiveWithAnyArgs().Attack(default(Vector2), default(Transform[]), default(GameObject));
            testAttack2.ReceivedWithAnyArgs().Attack(default(Vector2), default(Transform[]), default(GameObject));
            Debug.Log("PlayerInput calls the shift attack coroutine successfully.");
        }
    }
}
using System.Collections;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PlayTest
{
    public class CreatureBehaviorTest
    {
        private GameObject _gameObject;
        private CreatureMove _move;
        
        [SetUp]
        public void Setup()
        {
            _gameObject = GameObject.Instantiate(new GameObject());
            _move = _gameObject.AddComponent<CreatureMove>();
        }

        [TearDown]
        public void Teardown()
        {
            GameObject.Destroy(_gameObject);
        }

        [UnityTest]
        public IEnumerator CreatureMoveHasCharacterController()
        {
            yield return new WaitForSeconds(0.1f);
            
            Assert.NotNull(_move.GetComponent<CharacterController>(), "CreatureMove has a Character Controller attached.");
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator CreatureMoveDoesNotMoveWithoutInput()
        {
            Vector2 position = _move.transform.position;
            
            yield return new WaitForSeconds(0.1f);

            Vector2 newPosition = _move.transform.position;
            
            Assert.AreEqual(position, newPosition, "Move Test Passed, CreatureMove did not move without input.");
        }
        
        [UnityTest]
        public IEnumerator CreatureMovesHorizontallyOnHorizontalInput()
        {
            Vector2 position = _move.transform.position;
            var unityService = Substitute.For<IUnityService>();
            unityService.GetAxis("Horizontal").Returns(1);
            unityService.GetDeltaTime().Returns(1);

            _move.UnityService = unityService; 
            
            yield return new WaitForSeconds(0.1f);

            Vector2 newPosition = _move.transform.position;
            
            Assert.AreEqual(position.y, newPosition.y, "Move Test Passed, CreatureMove had no vertical movement.");
            Assert.AreNotEqual(newPosition.x, position.x, "Move Test Passed, CreatureMove moved horizontally.");
        }
        
        [UnityTest]
        public IEnumerator CreatureMovesVerticallyOnVerticalInput()
        {
            Vector2 position = _move.transform.position;
            var unityService = Substitute.For<IUnityService>();
            unityService.GetAxis("Vertical").Returns(1);
            unityService.GetDeltaTime().Returns(1);

            _move.UnityService = unityService; 
            
            yield return new WaitForSeconds(0.1f);

            Vector2 newPosition = _move.transform.position;
            
            Assert.AreEqual(position.x, newPosition.x, "Move Test Passed, CreatureMove had no horizontal movement.");
            Assert.AreNotEqual(newPosition.y, position.y, "Move Test Passed, CreatureMove moved vertically.");
        }
        
        [UnityTest]
        public IEnumerator CreatureMovesDiagonallyOnBothInputs()
        {
            Vector2 position = _move.transform.position;
            var unityService = Substitute.For<IUnityService>();
            unityService.GetAxis("Horizontal").Returns(1);
            unityService.GetAxis("Vertical").Returns(1);
            unityService.GetDeltaTime().Returns(1);

            _move.UnityService = unityService; 
            
            yield return new WaitForSeconds(0.1f);

            Vector2 newPosition = _move.transform.position;
            
            Assert.AreNotEqual(position.x, newPosition.x, "Move Test Passed, CreatureMove moved horizontally.");
            Assert.AreNotEqual(newPosition.y, position.y, "Move Test Passed, CreatureMove moved vertically.");
        }
    }
}

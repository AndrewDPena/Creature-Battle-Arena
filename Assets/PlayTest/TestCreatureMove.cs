using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PlayTest
{
    public class TestCreatureMove
    {
        private GameObject _gameObject;
        private CreatureMove _move;
        
        [SetUp]
        public void Setup()
        {
            _gameObject = GameObject.Instantiate(new GameObject());
            _move = _gameObject.AddComponent<CreatureMove>();
            _move.CreatureSpeed = 2.0f;
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
            
            Assert.NotNull(_move.UnityService, "CreatureMove has a UnityService.");
        }
        
        [UnityTest]
        public IEnumerator CreatureMoveHasCharacterController()
        {
            yield return new WaitForSeconds(0.1f);
            
            Assert.NotNull(_move.GetComponent<CharacterController>(), "CreatureMove has a Character Controller attached.");
        }
        
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
            
            var movementVector = new Vector2(1, 0);
            _move.SetVelocity(movementVector);

            yield return new WaitForSeconds(0.1f);

            Vector2 newPosition = _move.transform.position;
            
            Assert.AreEqual(position.y, newPosition.y, "Move Test Passed, CreatureMove had no vertical movement.");
            Assert.AreNotEqual(newPosition.x, position.x, "Move Test Passed, CreatureMove moved horizontally.");
        }
        
        [UnityTest]
        public IEnumerator CreatureMovesVerticallyOnVerticalInput()
        {
            Vector2 position = _move.transform.position;
            
            var movementVector = new Vector2(0, 1);
            _move.SetVelocity(movementVector);

            yield return new WaitForSeconds(0.1f);

            Vector2 newPosition = _move.transform.position;
            
            Assert.AreEqual(position.x, newPosition.x, "Move Test Passed, CreatureMove had no horizontal movement.");
            Assert.AreNotEqual(newPosition.y, position.y, "Move Test Passed, CreatureMove moved vertically.");
        }
        
        [UnityTest]
        public IEnumerator CreatureMovesDiagonallyOnBothInputs()
        {
            Vector2 position = _move.transform.position;
            
            var movementVector = new Vector2(1, 1);
            _move.SetVelocity(movementVector);

            yield return new WaitForSeconds(0.1f);

            Vector2 newPosition = _move.transform.position;
            
            Assert.AreNotEqual(position.y, newPosition.y, "Move Test Passed, CreatureMove moved vertically.");
            Assert.AreNotEqual(newPosition.x, position.x, "Move Test Passed, CreatureMove moved horizontally.");
        }
    }
}
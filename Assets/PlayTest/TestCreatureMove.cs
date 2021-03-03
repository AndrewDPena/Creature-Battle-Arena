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
        private BoxCollider2D _collider;
        private BoxCollider2D _box;
        
        [SetUp]
        public void Setup()
        {
            _gameObject = GameObject.Instantiate(new GameObject());
            _collider = _gameObject.AddComponent<BoxCollider2D>();
            _collider.size = new Vector2(0.5f, 0.5f);
            _move = _gameObject.AddComponent<CreatureMove>();
            _move.CreatureSpeed = 1000.0f;
        }

        [TearDown]
        public void Teardown()
        {
            GameObject.Destroy(_gameObject);
        }
        
        [UnityTest]
        public IEnumerator PlayerInputKeyboardHasUnityService()
        {
            yield return new WaitForSeconds(0.5f);
            
            Assert.NotNull(_move.UnityService, "CreatureMove has a UnityService.");
        }
        
        [UnityTest]
        public IEnumerator CreatureMoveHasRigidBody()
        {
            yield return new WaitForSeconds(0.5f);
            
            Assert.NotNull(_move.GetComponent<Rigidbody2D>(), "CreatureMove has a RigidBody2D attached.");
        }
        
        [UnityTest]
        public IEnumerator CreatureMoveDoesNotMoveWithoutInput()
        {
            Vector2 position = _move.transform.position;
            
            yield return new WaitForSeconds(0.5f);

            Vector2 newPosition = _move.transform.position;
            
            Assert.AreEqual(position, newPosition, "Move Test Passed, CreatureMove did not move without input.");
        }
        
        [UnityTest]
        public IEnumerator CreatureMovesHorizontallyOnHorizontalInput()
        {
            Vector2 position = _move.transform.position;
            
            var movementVector = new Vector2(1, 0);
            _move.SetVelocity(movementVector);

            yield return new WaitForSeconds(0.5f);

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

            yield return new WaitForSeconds(0.5f);

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

            yield return new WaitForSeconds(0.5f);

            Vector2 newPosition = _move.transform.position;
            
            Assert.AreNotEqual(position.y, newPosition.y, "Move Test Passed, CreatureMove moved vertically.");
            Assert.AreNotEqual(newPosition.x, position.x, "Move Test Passed, CreatureMove moved horizontally.");
        }

        [UnityTest]
        public IEnumerator CreatureCollidesWithWall()
        {
            _move.transform.position = new Vector2(0, 0);            
            var movementVector = new Vector2(1, 0);
            _move.SetVelocity(movementVector);
            yield return new WaitForSeconds(0.5f);
            
            Vector2 unblockedPosition = _move.transform.position;
            
            _move.transform.position = new Vector2(0, 0);

            _box = _gameObject.AddComponent<BoxCollider2D>();
            _box.size = new Vector2(0.5f, 100f);
            _box.transform.position = new Vector2(1f, 0);
            
            _move.SetVelocity(movementVector);
            yield return new WaitForSeconds(0.5f);

            Vector2 blockedPosition = _move.transform.position;

            Debug.Log("unblocked position is: " + unblockedPosition);
            Debug.Log("blocked position is: " + blockedPosition);
            Assert.AreNotEqual(unblockedPosition.x, blockedPosition.x, 
                "Move Test Passed, CreatureMove didn't move the same distance with a collider in the way.");
        }
    }
}
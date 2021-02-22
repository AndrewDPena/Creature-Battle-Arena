using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UIElements;

namespace PlayTest
{
    public class CreatureBehavior
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
    }
}

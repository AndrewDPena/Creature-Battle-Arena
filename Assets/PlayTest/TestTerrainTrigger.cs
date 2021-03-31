using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TestTools;

namespace PlayTest
{
    public class TestTerrainTrigger
    {
        private GameObject _gameObject;
        private BoxCollider2D _box;
        private CreatureMove _move;

        private GameObject _gameObject2;
        private BoxCollider2D _terrainCollider;
        private TerrainTrigger _terrainTrigger;
        
        
        [SetUp]
        public void Setup()
        {
            _gameObject = GameObject.Instantiate(new GameObject());
            
            _box = _gameObject.AddComponent<BoxCollider2D>();
            _box.size = new Vector2(5f, 5f);

            _move = _gameObject.AddComponent<CreatureMove>();
            _move.CreatureSpeed = 100.0f;
            _move.TerrainSpeedModifier = 1.0f;

            _gameObject2 = GameObject.Instantiate(new GameObject());
            _gameObject2.transform.position = new Vector3(100, 100, 100);
            
            _terrainCollider = _gameObject2.AddComponent<BoxCollider2D>();
            _terrainCollider.size = new Vector2(5f, 5f);
            _terrainCollider.isTrigger = true;
            
            _terrainTrigger = _gameObject2.AddComponent<TerrainTrigger>();
            _terrainTrigger.TerrainModifier = 0.5f;
        }

        [TearDown]
        public void Teardown()
        {
            GameObject.Destroy(_gameObject);
            GameObject.Destroy(_gameObject2);
        }

        [UnityTest]
        public IEnumerator CreatureMoveTerrainModifierIsUnchangedWithoutEvent()
        {
            yield return new WaitForSeconds(0.1f);
            
            Assert.AreEqual(1.0f, _move.TerrainSpeedModifier, 
                "CreatureMove does not change terrain speed without an event flag.");
        }
        
        [UnityTest]
        public IEnumerator CreatureMoveTerrainModifierChangesOnEvent()
        {
            _terrainCollider.transform.position = Vector3.zero;
            _box.transform.position = Vector3.zero;
            
            yield return new WaitForSeconds(0.1f);
            
            Assert.AreEqual(0.5f, _move.TerrainSpeedModifier, 
                "CreatureMove changes terrain speed with an event flag.");
        }
        
        [UnityTest]
        public IEnumerator CreatureMoveTerrainModifierChangesBackOnExitEvent()
        {
            _terrainCollider.transform.position = Vector3.zero;
            _box.transform.position = Vector3.zero;
            
            yield return new WaitForSeconds(0.1f);
            
            _box.transform.position = new Vector3(100, 100, 100);
            
            yield return new WaitForSeconds(0.1f);

            
            Assert.AreEqual(1f, _move.TerrainSpeedModifier, 
                "CreatureMove changes terrain speed with an event flag.");
        }
    }
}
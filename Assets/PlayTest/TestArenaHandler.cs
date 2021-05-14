using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace PlayTest
{
    public class TestArenaHandler : MonoBehaviour
    {
        private ArenaHandler _handler;
        
        [SetUp]
        public void Setup()
        {
            SceneManager.LoadScene("ArenaScene");
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
        public IEnumerator TestSetupGrabsHandlerCorrectly()
        {
            yield return new WaitForSeconds(0.1f);
            _handler = FindObjectOfType<ArenaHandler>();
            
            Assert.NotNull(_handler, "The handler was successfully pulled from the Scene.");
        }

        // This test must change if a different method of passing info between scenes is used.
        [UnityTest]
        public IEnumerator StaticSetDataSetsPlayerPockets()
        {
            SceneManager.UnloadSceneAsync("ArenaScene"); 
            // Has to be done to prevent Start() from running before pocket setup
            
            var creature1 = Instantiate(Resources.Load<CreatureBase>("Prefabs/Creatures/000 Testo"));
            var creature2 = Instantiate(Resources.Load<CreatureBase>("Prefabs/Creatures/000 Testo"));
            var creature3 = Instantiate(Resources.Load<CreatureBase>("Prefabs/Creatures/000 Testo"));
            var testPocket1 = new List<CreatureBase>()
            {
                creature1,
                creature2,
                creature3
            };
            
            var testPocket2 = new List<CreatureBase>()
            {
                creature1,
            };
            
            SceneManager.LoadScene("ArenaScene");
            ArenaHandler.SetData(testPocket1, testPocket2);

            yield return new WaitForSeconds(0.1f);
            _handler = FindObjectOfType<ArenaHandler>();

            
            Assert.AreEqual(testPocket1, _handler.PlayerCreatures, 
                "The SetData method correctly assigns the player pocket.");
            
            Assert.AreEqual(testPocket2, _handler.NpcCreatures, 
                "The SetData method correctly assigns the NPC pocket.");
        }
    }
}
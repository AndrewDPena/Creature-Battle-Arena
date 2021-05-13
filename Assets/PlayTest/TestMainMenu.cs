using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace PlayTest
{
    public class TestMainMenu
    {
        private GameObject _gameObject;
        private MainMenu _menu;
        
        [SetUp]
        public void Setup()
        {
            _gameObject = GameObject.Instantiate(new GameObject());
            SceneManager.LoadScene("Start Menu");
        }
        
        [TearDown]
        public void Teardown()
        {
            foreach (var o in GameObject.FindObjectsOfType<GameObject>()) {
                GameObject.Destroy(o);
            }
        }
        
        [UnityTest]
        public IEnumerator ArenaSceneIsNotLoadedByDefault()
        {
            yield return new WaitForSeconds(0.1f);

            var scene = SceneManager.GetActiveScene();
            
            Assert.AreNotEqual(SceneManager.GetSceneByName("ArenaScene"), scene, 
                "The default scene is not the ArenaScene.");
        }
        
        [UnityTest]
        public IEnumerator ArenaSceneIsLoadedByMainMenuAction()
        {
            yield return new WaitForSeconds(0.1f);
            _menu = GameObject.FindObjectOfType<MainMenu>();
            
            Assert.IsNotNull(_menu, "The menu should attach correctly.");
            _menu.LoadBattleScene();
            yield return new WaitForSeconds(0.1f);

            var scene = SceneManager.GetActiveScene();
            
            Assert.AreEqual(SceneManager.GetSceneByName("ArenaScene"), scene, 
                "The ArenaScene is loaded upon MainMenu method call.");
        }

        [UnityTest]
        public IEnumerator SavePocketWorksCorrectly()
        {
            yield return new WaitForSeconds(0.1f);
            _menu = GameObject.FindObjectOfType<MainMenu>();

            _menu.PlayerSlots[0].value = 1; // Recall this increments by 1 to account for "none"
            _menu.PlayerSlots[1].value = 2;
            _menu.PlayerSlots[2].value = 3;
            
            _menu.NpcSlots[0].value = 0; // Recall this increments by 1 to account for "none"
            _menu.NpcSlots[1].value = 1;
            _menu.NpcSlots[2].value = 1;
            
            _menu.SavePocket();
            yield return new WaitForSeconds(0.1f);
            
            Assert.AreEqual(_menu.CreatureDex.GetCreatureByDexNumber(2), _menu.PlayerCreatures[0], 
                "Creature List set the First creature correctly.");
            Assert.AreEqual(_menu.CreatureDex.GetCreatureByDexNumber(2), _menu.PlayerCreatures[1], 
                "Creature List set the Second creature correctly.");
            Assert.AreEqual(_menu.CreatureDex.GetCreatureByDexNumber(3), _menu.PlayerCreatures[2], 
                "Creature List set the Third creature correctly.");
            Assert.AreEqual(_menu.CreatureDex.GetCreatureByDexNumber(1), _menu.NpcCreatures[0], 
                "Creature List set the First creature correctly.");
            Assert.AreEqual(_menu.CreatureDex.GetCreatureByDexNumber(1), _menu.NpcCreatures[1], 
                "Creature List set the Second creature correctly.");
            Assert.AreEqual(_menu.CreatureDex.GetCreatureByDexNumber(1), _menu.NpcCreatures[2], 
                "Creature List set the Third creature correctly.");
        }
    }
}
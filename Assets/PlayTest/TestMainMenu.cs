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
    }
}
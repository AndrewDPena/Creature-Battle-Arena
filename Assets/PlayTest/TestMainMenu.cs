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
            _menu = _gameObject.AddComponent<MainMenu>();
            SceneManager.LoadScene("Start Menu");
        }
        
        [TearDown]
        public void Teardown()
        {
            GameObject.Destroy(_gameObject);
        }
        
        [UnityTest]
        public IEnumerator ArenaSceneIsNotLoadedByDefault()
        {
            yield return new WaitForSeconds(0.1f);

            var Scene = SceneManager.GetActiveScene();
            
            Assert.AreNotEqual(SceneManager.GetSceneByName("ArenaScene"), Scene, 
                "The default scene is not the ArenaScene.");
        }
        
        [UnityTest]
        public IEnumerator ArenaSceneIsNotLoadedByMainMenuAction()
        {
            _menu.LoadBattleScene();
            yield return new WaitForSeconds(0.1f);

            var Scene = SceneManager.GetActiveScene();
            
            Assert.AreEqual(SceneManager.GetSceneByName("ArenaScene"), Scene, 
                "The ArenaScene is loaded upon MainMenu method call.");
        }
    }
}
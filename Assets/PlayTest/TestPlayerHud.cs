using System.Collections;
using AttackManagement;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UserInterfaceScripts;

namespace PlayTest
{
    public class TestPlayerHud : MonoBehaviour
    {
        private PlayerHUD _hud;
        private AttackWindowHud _aHud;
        private CreatureData _creature;
        private BattleEndWindow _battleEnd;
        
        [SetUp]
        public void Setup()
        {
            _hud = Instantiate(Resources.Load<PlayerHUD>("Prefabs/PlayerHudPrefab"), 
                Vector3.zero, Quaternion.identity);
            _aHud = Instantiate(Resources.Load<AttackWindowHud>("Prefabs/Command Description Window"),
                Vector3.zero, Quaternion.identity);
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
        public IEnumerator HudInitializesNullValuesWithEmptyCreature()
        {
            _hud.InitializeHUD(_creature);
            
            yield return new WaitForSeconds(0.1f);
            
            Assert.AreEqual("", _hud.Name.text, "Name is an empty string if the creature is null.");
        }
        
        [UnityTest]
        public IEnumerator HudInitializesValuesCorrectlyWithInstanceOfCreature()
        {
            _creature = new CreatureData("Test", 10, 10);
            _hud.InitializeHUD(_creature);
            
            yield return new WaitForSeconds(0.1f);
            
            Assert.AreEqual("Test", _hud.Name.text, "Name is correctly set when the creature has data.");
        }

        [UnityTest]
        public IEnumerator AttackWindowHudSetsEmptyDescriptionOnNullAttack()
        {
            _aHud.SetAttackDesc(null);
            
            yield return new WaitForSeconds(0.1f);
            
            Assert.AreEqual("Empty", _aHud.AttackDesc, "AttackWindowHud shows Empty for a null attack.");
        }
        
        [UnityTest]
        public IEnumerator AttackWindowHudSetsDescriptionToMatchAttack()
        {
            var atk = Instantiate(Resources.Load<AttackBase>("Prefabs/Attacks/TestAttack"));
            _aHud.SetAttackDesc(atk);
            
            yield return new WaitForSeconds(0.1f);
            
            Assert.AreEqual(atk.Name, _aHud.AttackDesc.Substring(0, atk.Name.Length), 
                "AttackWindowHud sets data for an attack.");
        }

        [UnityTest]
        public IEnumerator BattleEndWindowCanReturnToStartScene()
        {
            var go = Instantiate(new GameObject());
            _battleEnd = go.AddComponent<BattleEndWindow>();
            _battleEnd.StartNewGame();
            
            yield return new WaitForSeconds(0.1f);
            
            Assert.AreEqual("Start Menu", SceneManager.GetActiveScene().name, 
                "The currect scene becomes the Start Menu upon play again button press.");
            SceneManager.UnloadSceneAsync("Start Menu");
        }

        [UnityTest]
        public IEnumerator BattleEndWindowCanCallApplicationQuit()
        {
            var exitCode = 9999;
            
            var go = Instantiate(new GameObject());
            _battleEnd = go.AddComponent<BattleEndWindow>();
            exitCode = _battleEnd.EndGame();
            
            yield return new WaitForSeconds(0.1f);
            
            Assert.AreEqual(-1, exitCode, "Exit code successfully returned, Application.Quit() was called.");
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UserInterfaceScripts;
using UnityEngine.UI;

namespace PlayTest
{
    public class TestArenaHandler : MonoBehaviour
    {
        private GameObject _gameObject;
        private ArenaHandler _handler;
        
        [SetUp]
        public void Setup()
        {
            SceneManager.LoadScene("ArenaScene");
            _gameObject = Instantiate(new GameObject());
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

        [UnityTest]
        public IEnumerator NpcLosesTheBattleWithNoCreaturesRemaining()
        {
            yield return new WaitForSeconds(0.1f);
            _handler = FindObjectOfType<ArenaHandler>();

            var type = typeof(ArenaHandler).GetField("_battleEnd", 
                BindingFlags.NonPublic | BindingFlags.Instance);  
            var battleEndValue = (BattleEndWindow)type.GetValue(_handler);

            _gameObject = Instantiate(new GameObject());
            var defaultBattleEnd = _gameObject.AddComponent<BattleEndWindow>();
            var defaultText = _gameObject.AddComponent<Text>();
            defaultBattleEnd.GetType().GetField("_outcomeText", 
                BindingFlags.NonPublic | BindingFlags.Instance)?.SetValue(defaultBattleEnd, defaultText);

            defaultBattleEnd.SetOutcome(true);

            var player = Substitute.For<ISummoner>();
            _handler.NPCPlayer = player;
            player.HasRemainingCreatures().Returns(false);
            _handler.ReportCreatureFainted(player);
            
            yield return new WaitForSeconds(0.1f);
            
            Assert.AreEqual(defaultBattleEnd.OutcomeText, battleEndValue.OutcomeText, 
                "Victory screen shown when NPC creature faints and NPC has no creatures remaining.");
        }
        
        [UnityTest]
        public IEnumerator NpcContinuesTheBattleWithCreaturesRemaining()
        {
            yield return new WaitForSeconds(0.1f);
            _handler = FindObjectOfType<ArenaHandler>();

            var player = Substitute.For<ISummoner>();
            _handler.NPCPlayer = player;
            player.HasRemainingCreatures().Returns(true);
            var creature = new CreatureData("Test", 10, 10);
            player.GetNextHealthyCreature().Returns(1);
            player.CanSummonCreature(1).Returns(true);
            player.SummonCreature(1).Returns(creature);
                        
            var type = typeof(ArenaHandler).GetField("_enemyCreature", 
                BindingFlags.NonPublic | BindingFlags.Instance);
            var currentNpcCreature = (Creature) type.GetValue(_handler);
            currentNpcCreature.AssignPlayer(player);
            
            _handler.ReportCreatureFainted(player);

            
            yield return new WaitForSeconds(0.1f);

            player.Received().HasRemainingCreatures();
            player.Received().GetNextHealthyCreature();
            player.ReceivedWithAnyArgs().CanSummonCreature(1);
            player.ReceivedWithAnyArgs().SummonCreature(1);
            
            Assert.AreEqual(creature.Name, currentNpcCreature.CurrentCreature.Name, 
                "NPC summons its next healthy creature when NPC creature faints and NPC has creatures remaining.");
        }
        
        [UnityTest]
        public IEnumerator PlayerLosesTheBattleWithNoCreaturesRemaining()
        {
            yield return new WaitForSeconds(0.1f);
            _handler = FindObjectOfType<ArenaHandler>();

            var type = typeof(ArenaHandler).GetField("_battleEnd", 
                BindingFlags.NonPublic | BindingFlags.Instance);  
            var battleEndValue = (BattleEndWindow)type.GetValue(_handler);

            _gameObject = Instantiate(new GameObject());
            var defaultBattleEnd = _gameObject.AddComponent<BattleEndWindow>();
            var defaultText = _gameObject.AddComponent<Text>();
            defaultBattleEnd.GetType().GetField("_outcomeText", 
                BindingFlags.NonPublic | BindingFlags.Instance)?.SetValue(defaultBattleEnd, defaultText);

            defaultBattleEnd.SetOutcome(false);

            var player = Substitute.For<ISummoner>();
            _handler.HumanPlayer = player;
            player.HasRemainingCreatures().Returns(false);
            _handler.ReportCreatureFainted(player);
            
            yield return new WaitForSeconds(0.1f);
            
            Assert.AreEqual(defaultBattleEnd.OutcomeText, battleEndValue.OutcomeText, 
                "Loss screen shown when player creature faints and player has no creatures remaining.");
        }

        [UnityTest]
        public IEnumerator PlayerIsAbleToSwapCreaturesOnFaint()
        {
            yield return new WaitForSeconds(0.1f);
            _handler = FindObjectOfType<ArenaHandler>();
            
            var player = Substitute.For<ISummoner>();
            _handler.HumanPlayer = player;
            player.HasRemainingCreatures().Returns(true);
            var creature = new CreatureData("Test", 10, 10);
            player.CanSummonCreature(1).Returns(true);
            player.SummonCreature(1).Returns(creature);
            
            var type = typeof(ArenaHandler).GetField("_playerCreature", 
                BindingFlags.NonPublic | BindingFlags.Instance);
            var playerCreature = (Creature) type.GetValue(_handler);
            playerCreature.AssignPlayer(player);
            playerCreature.TakeDamage(10000);
            
            //_handler.ReportCreatureFainted(player);
            yield return new WaitForSeconds(0.1f);
            
            Assert.AreNotEqual(creature.Name, playerCreature.CurrentCreature.Name, 
                "Creatures are not the same after a faint until  player calls a swap.");
            
            playerCreature.Swap(1);
            yield return new WaitForSeconds(0.1f);
            
            Assert.AreEqual(creature.Name, playerCreature.CurrentCreature.Name,
                "After a faint, a player can call a swap and get the next creature.");
        }
    }
}
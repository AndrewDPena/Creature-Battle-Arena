using System.Collections;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UserInterfaceScripts;

namespace PlayTest
{

    public class TestPlayer : MonoBehaviour
    {
        private GameObject _gameObject;
        private PocketHUD _pocketHud;
        private PlayerHUD _playerHud;
        private Player _player;
        private AttackWindowHud _atkHud1;
        private AttackWindowHud _atkHud2;


        [SetUp]
        public void Setup()
        {
            _gameObject = Instantiate(new GameObject());
            _pocketHud = _gameObject.AddComponent<PocketHUD>();
            _player = new Player();
            _playerHud = Instantiate(Resources.Load<PlayerHUD>("Prefabs/PlayerHudPrefab"), Vector3.zero, Quaternion.identity);
            _pocketHud.AddHud(_playerHud);
            _player.SetPocketHUD(_pocketHud);
            _atkHud1 = Instantiate(Resources.Load<AttackWindowHud>("Prefabs/Command Description Window"), 
                Vector3.zero, Quaternion.identity);
            _atkHud2 = Instantiate(Resources.Load<AttackWindowHud>("Prefabs/Command Description Window"), 
                Vector3.zero, Quaternion.identity);
            _player.SetAttackHuds(_atkHud1, _atkHud2);
        }

        [TearDown]
        public void Teardown()
        {
            foreach (GameObject o in FindObjectsOfType<GameObject>()) {
                Destroy(o);
            }
        }

        [UnityTest]
        public IEnumerator PlayerPropertiesWorkCorrectly()
        {
            var pName = "TestPlayer";
            _player.Name = pName;
            
            yield return new WaitForSeconds(0.1f);
            Assert.AreEqual(pName, _player.Name, "C# isn't broken and the properties of the player work.");
        }
        

        [UnityTest]
        public IEnumerator PlayerCreates()
        {
            yield return new WaitForSeconds(0.1f);

            Assert.IsNotNull(_player, "Player is constructed correctly.");
        }

        [UnityTest]
        public IEnumerator PlayerCanAssignAHud()
        {

            yield return new WaitForSeconds(0.1f);

            Assert.AreEqual(_playerHud, _player.GetType().GetField("_hud", BindingFlags.NonPublic | 
                                                                           BindingFlags.Instance)?.GetValue(_player), 
                "Hud Attaches Correctly.");
        }

        [UnityTest]
        public IEnumerator PlayerCanStoreACreature()
        {
            Assert.AreEqual(0, _player.GetPocketSize(), "Player starts with an empty pocket.");
            _player.AddCreature(new CreatureData("Test", 10, 10));
            
            yield return new WaitForSeconds(0.1f);
            
            Assert.AreEqual(1, _player.GetPocketSize(), "Player pocket increases when creature is added.");
        }

        [UnityTest]
        public IEnumerator PlayerCanSummonACreature()
        {
            var creature = new CreatureData("Test", 10, 10);
            _player.AddCreature(creature);
            yield return new WaitForSeconds(0.1f);
            Assert.AreNotEqual(creature, _player.GetActiveCreature(), "Player starts without an active creature.");

            _player.SummonCreature(0);
            yield return new WaitForSeconds(0.1f);
            
            Assert.AreEqual(creature, _player.GetActiveCreature(), "Player successfully summons creature.");
        }

        [UnityTest]
        public IEnumerator PlayerCanUpdateHud()
        {
            var creature = new CreatureData("Test", 0, 100);
            _player.AddCreature(creature);
            _player.SummonCreature(0);
            
            yield return new WaitForSeconds(0.1f);
            Assert.AreEqual(100, _playerHud.GetHealth(), "Hud updates to summoned creature.");
            
            creature.TakeDamage(10);
            
            _player.UpdateHUD(creature);            
            yield return new WaitForSeconds(0.1f);
            Assert.AreEqual(90, _playerHud.GetHealth(), "Player can update HUD values.");

        }

        [UnityTest]
        public IEnumerator PlayerReportsRemainingCreatures()
        {
            yield return new WaitForSeconds(0.1f);
            Assert.False(_player.HasRemainingCreatures(), "Player starts with no remaining creatures.");
            
            _player.AddCreature(new CreatureData("Test", 10, 10));
            yield return new WaitForSeconds(0.1f);
            Assert.True(_player.HasRemainingCreatures(), "Player has remaining creatures after a creature is added.");
        }
        
        [UnityTest]
        public IEnumerator PlayerReportsNoRemainingCreaturesOnZeroHp()
        {
            var creature = new CreatureData("test", 10, 10);
            _player.AddCreature(creature);
            yield return new WaitForSeconds(0.1f);
            Assert.True(_player.HasRemainingCreatures(), "Player has remaining creatures after a creature is added.");
            
            
            creature.TakeDamage(10);
            yield return new WaitForSeconds(0.1f);
            Assert.False(_player.HasRemainingCreatures(), 
                "Player has no remaining creatures when all creatures HP is zero.");
        }

        [UnityTest]
        public IEnumerator PlayerGetsFirstCreatureWhenAllAreHealthy()
        {
            _player.AddCreature(new CreatureData("Test1", 10, 10));
            _player.AddCreature(new CreatureData("Test2", 10, 10));
            _player.AddCreature(new CreatureData("Test3", 10, 10));   
            
            yield return new WaitForSeconds(0.1f);
            Assert.AreEqual(0, _player.GetNextHealthyCreature(),
                "The first index is returned when all creatures are healthy.");
        }
        
        [UnityTest]
        public IEnumerator PlayerGetsFirstHealthyCreatureWhenSomeAreFainted()
        {
            _player.AddCreature(new CreatureData("Test1", 0, 0));
            _player.AddCreature(new CreatureData("Test2", 0, 0));
            _player.AddCreature(new CreatureData("Test3", 10, 10));   
            
            yield return new WaitForSeconds(0.1f);
            Assert.AreEqual(2, _player.GetNextHealthyCreature(),
                "The third index is returned when it's the first healthy creature.");
        }
        
        [UnityTest]
        public IEnumerator FirstHealthyCreatureIsNegativeOneForEmptyPocket()
        {  
            yield return new WaitForSeconds(0.1f);
            Assert.AreEqual(-1, _player.GetNextHealthyCreature(),
                "A Negative One is returned in an error case.");
        }

        [UnityTest]
        public IEnumerator PlayerAssignsAttackHuds()
        {
            var creatureBase = Instantiate(Resources.Load<CreatureBase>("Prefabs/Creatures/000 Testo"));
            
            _player.AddCreature(new CreatureData(creatureBase));
            var creature = _player.SummonCreature(0);
            
            yield return new WaitForSeconds(0.1f);
            Assert.AreEqual(creature.Attacks[0].Name, _atkHud1.AttackDesc.Substring(0, 4), 
                "Player assigns the attack hud correctly on a summon.");
        }
    }
}
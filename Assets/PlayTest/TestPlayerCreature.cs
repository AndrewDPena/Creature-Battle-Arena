using System.Collections;
using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace PlayTest
{
    public class TestPlayerCreature
    {
        private GameObject _gameObject;
        private Player _player;
        //private HealthBar _healthBar;
        //private Slider _slider;
        //private Image _fill;
        //private Gradient _gradient;
        private Creature _creature;
        
        [SetUp]
        public void Setup()
        {
            _gameObject = GameObject.Instantiate(new GameObject());
            _player = new Player();
            //_slider = _gameObject.AddComponent<Slider>();
            //_fill = _gameObject.AddComponent<Image>();
            //_gradient = new Gradient();
            //_healthBar = _gameObject.AddComponent<HealthBar>();
            _creature = _gameObject.AddComponent<Creature>();
            //_healthBar.GetType().GetField("Slider")?.SetValue(_healthBar, _slider);
            //_healthBar.GetType().GetField("Fill")?.SetValue(_healthBar, _fill);
            //_healthBar.GetType().GetField("Gradient")?.SetValue(_healthBar, _gradient);
            //_slider.maxValue = 100;
            //_slider.value = 0;
            
            //_creature.GetType().GetField("HealthBar")?.SetValue(_creature, _healthBar);
        }

        [TearDown]
        public void Teardown()
        {
            GameObject.Destroy(_gameObject);
        }
        
        [UnityTest]
        public IEnumerator PlayerCreatureInstantiates()
        {
            yield return new WaitForSeconds(0.1f);
            
            Assert.NotNull(_creature, "Creature instantiates correctly.");
        }

        [UnityTest]
        public IEnumerator PlayerCreatureSpawnsWithDefaultValues()
        {
            yield return new WaitForSeconds(0.1f);
            Assert.Null(_creature.Name, "Name is null.");
            Assert.AreEqual(0, _creature.Strength, "Strength is 0.");
            Assert.AreEqual(0, _creature.CurrentHealth, "CurrentHealth is 0.");
            Assert.AreEqual(0, _creature.MaxHealth, "MaxHealth is 0.");
        }
        
        [UnityTest]
        public IEnumerator PlayerCreatureSetsValuesCorrectly()
        {
            _creature.Setup("Name", 1, 2, _player);
            yield return new WaitForSeconds(0.1f);
            
            Assert.AreEqual(2, _creature.MaxHealth,  
                "Creature sets the second int to max health.");
            Assert.AreEqual("Name", _creature.Name,
                "Creature sets the first param to be its Name.");
        }
        
        [UnityTest]
        public IEnumerator PlayerCreatureTakesDamage()
        {
            _creature.Setup("Name", 1, 20, _player);
            yield return new WaitForSeconds(0.1f);
            var newHealth = _creature.MaxHealth - 10;
            _creature.TakeDamage(10);
            yield return new WaitForSeconds(0.1f);
            
            Assert.AreEqual(newHealth, _creature.CurrentHealth, "Creature updates current health when taking damage.");
        }
        
        /**
        [UnityTest]
        public IEnumerator PlayerCreatureUpdatesSetsHealthBarMaxValueWhenSpawning()
        {
            yield return new WaitForSeconds(0.1f);
            
            Assert.AreEqual(_creature.MaxHealth, _creature.HealthBar.GetMaxHealth(), 
                "Creature changes the max health value of the health bar upon spawn.");
        }
        
        [UnityTest]
        public IEnumerator PlayerCreatureUpdatesHealthBarWhenTakingDamage()
        {
            yield return new WaitForSeconds(0.1f);
            var newHealth = _creature.MaxHealth - 10;
            _creature.TakeDamage(10);
            
            Assert.AreEqual(newHealth, _creature.HealthBar.GetHealth(), 
                "Creature changes the current health of the healthbar when taking damage.");
        } **/
    }
}
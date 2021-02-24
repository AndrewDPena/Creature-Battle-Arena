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
        private HealthBar _healthBar;
        private Slider _slider;
        private Image _fill;
        private Gradient _gradient;
        private PlayerCreature _creature;
        
        [SetUp]
        public void Setup()
        {
            _gameObject = GameObject.Instantiate(new GameObject());
            _slider = _gameObject.AddComponent<Slider>();
            _fill = _gameObject.AddComponent<Image>();
            _gradient = new Gradient();
            _healthBar = _gameObject.AddComponent<HealthBar>();
            _creature = _gameObject.AddComponent<PlayerCreature>();
            _healthBar.GetType().GetField("Slider")?.SetValue(_healthBar, _slider);
            _healthBar.GetType().GetField("Fill")?.SetValue(_healthBar, _fill);
            _healthBar.GetType().GetField("Gradient")?.SetValue(_healthBar, _gradient);
            _slider.maxValue = 100;
            _slider.value = 0;
            
            _creature.GetType().GetField("HealthBar")?.SetValue(_creature, _healthBar);
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
        public IEnumerator PlayerCreatureSetsCurrentHealthOnSpawn()
        {
            yield return new WaitForSeconds(0.1f);
            
            Assert.AreEqual(_creature.MaxHealth, _creature.CurrentHealth, 
                "Creature sets the current health to max health.");
        }
        
        [UnityTest]
        public IEnumerator PlayerCreatureTakesDamage()
        {
            yield return new WaitForSeconds(0.1f);
            var newHealth = _creature.MaxHealth - 10;
            _creature.TakeDamage(10);
            yield return new WaitForSeconds(0.1f);
            
            Assert.AreEqual(newHealth, _creature.CurrentHealth, "Creature updates current health when taking damage.");
        }
        
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
        }
    }
}
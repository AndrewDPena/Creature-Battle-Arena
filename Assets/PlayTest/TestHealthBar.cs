// This was ONLY possible thanks to this tutorial on reflection with unity components
// Singh, Kuldeep, "Practical Unit Testing in Unity3D", Medium.com, accessed at:
// https://medium.com/xrpractices/practical-unit-testing-in-unity3d-f8d5f777c5db

using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace PlayTest
{
    public class TestHealthBar
    {
        private GameObject _gameObject;
        private HealthBar _healthBar;
        private Slider _slider;
        private Image _fill;
        private Gradient _gradient;
        
        [SetUp]
        public void Setup()
        {
            _gameObject = GameObject.Instantiate(new GameObject());
            _slider = _gameObject.AddComponent<Slider>();
            _fill = _gameObject.AddComponent<Image>();
            _gradient = new Gradient();
            _healthBar = _gameObject.AddComponent<HealthBar>();
            _healthBar.GetType().GetField("Slider")?.SetValue(_healthBar, _slider);
            _healthBar.GetType().GetField("Fill")?.SetValue(_healthBar, _fill);
            _healthBar.GetType().GetField("Gradient")?.SetValue(_healthBar, _gradient);
            _slider.maxValue = 1;
            _slider.value = 0;
        }

        [TearDown]
        public void Teardown()
        {
            GameObject.Destroy(_gameObject);
        }
        
        [UnityTest]
        public IEnumerator HealthBarInstantiates()
        {
            yield return new WaitForSeconds(0.1f);
            
            Assert.NotNull(_healthBar, "Reflection worked, HealthBar instantiates.");
        }
        
        [UnityTest]
        public IEnumerator HealthBarCanSetMaxHealth()
        {
            _healthBar.SetMaxHealth(10);
            
            yield return new WaitForSeconds(0.1f);
            
            Assert.AreEqual(10, _healthBar.GetMaxHealth(), "Max Health set Correctly.");
        }
        
        [UnityTest]
        public IEnumerator HealthBarCanSetHealth()
        {
            _healthBar.SetMaxHealth(10);
            _healthBar.SetHealth(5);
            
            yield return new WaitForSeconds(0.1f);
            
            Assert.AreNotEqual(_healthBar.GetHealth(), _healthBar.GetMaxHealth(), "Max Health and Currect Health are different.");
            Assert.AreEqual(5, _healthBar.GetHealth(), "Current Health set Correctly.");
        }
    }
}
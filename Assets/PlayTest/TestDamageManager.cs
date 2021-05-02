using System.Collections;
using AttackManagement;
using NSubstitute;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace PlayTest
{
    public class TestDamageManager
    {
        private GameObject _gameObject;
        private DamageManager _manager;

        [SetUp]
        public void Setup()
        {
            _gameObject = GameObject.Instantiate(new GameObject());
            _manager = _gameObject.AddComponent<DamageManager>();
        }
    }
}
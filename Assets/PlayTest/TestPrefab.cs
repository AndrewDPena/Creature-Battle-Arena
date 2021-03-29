using System.Collections;
using UnityEngine;

namespace PlayTest
{
    public class TestPrefab : IAttack
    {
        private Vector2 _direction;

        public IEnumerator Attack(Vector2 direction, Transform[] exitPoints, GameObject attackPrefab)
        {
            _direction = direction;
            yield return new WaitForSeconds(.1f);

        }

        public Vector2 GetDirection()
        {
            return _direction;
        }
    }
}
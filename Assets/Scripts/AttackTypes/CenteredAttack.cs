using System.Collections;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

namespace AttackTypes
{
    public class CenteredAttackType : IAttackType
    {
        public IEnumerator Attack(Vector2 direction, Transform[] exitPoints, GameObject attackPrefab)
        {
            var centered = Object.Instantiate(attackPrefab, exitPoints[6].position, Quaternion.identity);
            yield return new WaitForSeconds(.25f); 
    
            Object.Destroy(centered);
        }
    }
} 
using System.Collections;
using UnityEngine;


namespace AttackAreas
{
    public class ConeAttack : AttackAreaOfEffect
    {
        public override IEnumerator Attack(Vector2 direction, Transform[] exitPoints, GameObject attackPrefab)
        {
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            var cast = angle - (angle % 45);
            var point = (int) (cast / 45);
            if (point < 0)
            {
                point += exitPoints.Length;
            }
            
            var breath = Object.Instantiate(attackPrefab, exitPoints[point].position, Quaternion.identity);


            breath.transform.rotation = Quaternion.AngleAxis(cast, Vector3.forward);

            yield return new WaitForSeconds(.25f);
    
            Object.Destroy(breath);
        }
    }
}
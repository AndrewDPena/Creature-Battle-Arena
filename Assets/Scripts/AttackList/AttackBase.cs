using UnityEngine;

namespace AttackList
{
    [CreateAssetMenu(fileName = "Attack", menuName = "Attack/Create a new Attack")]
    public class AttackBase : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private int _damage;

        [SerializeField] private int _damagePerTick;

        //[SerializeField] Sprite attackSprite;
        [SerializeField] private GameObject _areaOfEffect;
        [SerializeField] private GameObject _spriteObject;

        public string Name => _name;

        public int Damage => _damage;

        public int DamagePerTick => _damagePerTick;

        //public GameObject AreaOfEffect => _areaOfEffect;

        public GameObject SpriteObject => _spriteObject;

        public AttackAreaOfEffect GetAreaOfEffect()
        {
            return _areaOfEffect.GetComponent<AttackAreaOfEffect>();
        }
    }
}
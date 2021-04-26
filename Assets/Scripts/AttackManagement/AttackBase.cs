using UnityEngine;

namespace AttackManagement
{
    [CreateAssetMenu(fileName = "Attack", menuName = "Attack/Create a new Attack")]
    public class AttackBase : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private int _damage;
        [SerializeField] private int _damagePerTick;
        [SerializeField] private bool _canDamageSelf;
        [SerializeField] private TypeChart.CreatureType _attackType;
        [SerializeField] private GameObject _areaOfEffect;
        [SerializeField] private GameObject _spriteObject;

        public string Name => _name;

        public int Damage => _damage;

        public int DamagePerTick => _damagePerTick;

        public bool CanDamageSelf => _canDamageSelf;

        public TypeChart.CreatureType AttackType => _attackType;

        public GameObject SpriteObject => _spriteObject;

        public AttackAreaOfEffect GetAreaOfEffect()
        {
            return _areaOfEffect.GetComponent<AttackAreaOfEffect>();
        }
    }
}
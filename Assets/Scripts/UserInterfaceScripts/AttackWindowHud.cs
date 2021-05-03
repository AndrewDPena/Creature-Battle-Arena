using AttackManagement;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterfaceScripts
{
    public class AttackWindowHud : MonoBehaviour
    {
        [SerializeField] private Text _attackDesc;

        public void SetAttackDesc(AttackBase attack)
        {
            _attackDesc.text = attack.Name + "\n" + attack.Damage + " DMG\n" + attack.AttackType;
        }
    }
}
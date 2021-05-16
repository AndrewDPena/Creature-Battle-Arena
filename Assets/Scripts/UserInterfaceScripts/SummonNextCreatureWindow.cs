using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterfaceScripts
{

    public class SummonNextCreatureWindow : MonoBehaviour
    {
        [SerializeField] private Image[] _keys;

        public void SetKeys(ISummoner player)
        {
            for (var i = 0; i < _keys.Length; i++)
            {
                _keys[i].color = player.CanSummonCreature(i + 1) ? Color.white : Color.black;
            }
        }
    }
}

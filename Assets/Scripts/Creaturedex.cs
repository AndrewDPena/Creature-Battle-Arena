using UnityEngine;
using UnityEngine.UI;

public class Creaturedex : MonoBehaviour
{
    [SerializeField] private CreatureBase[] _creatureDex;

    public CreatureBase GetCreatureByDexNumber(int dexNo)
    {

        return dexNo > _creatureDex.Length ? _creatureDex[0] : _creatureDex[dexNo - 1];
    }
}

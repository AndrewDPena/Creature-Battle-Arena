using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pocketMenu;
    [SerializeField] private GameObject _mainMenu;
    
    public void LoadPocketSetup()
    {
        
    }
    
    public void LoadBattleScene()
    {
        SceneManager.LoadScene("ArenaScene");
    }

    public void SavePocket()
    {
        Debug.Log("Success");
    }
}

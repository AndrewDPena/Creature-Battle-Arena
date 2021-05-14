using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UserInterfaceScripts
{
    public class BattleEndWindow : MonoBehaviour
    {
        [SerializeField] private Text _outcomeText;

        public void SetOutcome(bool playerWonBattle)
        {
            _outcomeText.text = playerWonBattle ? "You won!" : "You lost...";
        }
        
        public void EndGame()
        {
            Application.Quit();
        }

        public void StartNewGame()
        {
            SceneManager.LoadScene("Start Menu");
        }

        public string OutcomeText => _outcomeText.text;
    }
}
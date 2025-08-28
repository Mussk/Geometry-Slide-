using System;
using Cysharp.Threading.Tasks;
using TMPro;
using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameEndController : MonoBehaviour
{
    private UiScreen gameEndScreen;
    private TextMeshProUGUI scoreText;
    private GameObject nameInputField;
    [SerializeField] private GameObject leaderboardUI;
    [SerializeField] private string gameEndScreenTag;
    [SerializeField] private string scoreGameEndTextTag;
    [SerializeField] private string playerNameInputFieldTag;
    
    
    
    private Action _deathEventHandler;
    
    private void OnEnable()
    {   
        _deathEventHandler = () => ExecuteGameEnd().Forget();
        HealthSystem.DeathEvent += _deathEventHandler; 
        
        gameEndScreen = GameObject.FindWithTag(gameEndScreenTag).GetComponent<UiScreen>();
        scoreText = GameObject.FindWithTag(scoreGameEndTextTag).GetComponent<TextMeshProUGUI>();
        nameInputField = GameObject.FindWithTag(playerNameInputFieldTag);
        
        leaderboardUI.SetActive(false);
        nameInputField.gameObject.SetActive(false);
        
    }

    private void OnDisable()
    {
        HealthSystem.DeathEvent -= _deathEventHandler;
    }

    private async UniTask ExecuteGameEnd()
    {
        var leaderboardUIcontroller = leaderboardUI.GetComponent<LeaderboardUIController>();
        int thisGameScore = ScoreManager.Instance.GetScore();
        scoreText.text = thisGameScore.ToString();
        Time.timeScale = 0;
        gameEndScreen.ShowUI();
        leaderboardUIcontroller.isGetDataFromServer = true;
        if (thisGameScore > ScoreManager.HighestScore)
        {
            ScoreManager.HighestScore = thisGameScore;

            if (!LeaderboardManager.IsPlayerNameWasSet)
            {
                nameInputField.gameObject.SetActive(true);
                
            }
            else
            {   
                
                await LeaderboardManager.Instance.SubmitScore();
                await leaderboardUIcontroller.ShowLeaderboard();
            }
            
        }
        
        
    }
}

using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    
    public static int HighestScore {get; set;}
    
    [SerializeField] private TextMeshProUGUI scoreText;

    private int score = 0;

    public static Action<int> ScoreChangedEvent;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddScore(int value)
    {
        score += value;
        scoreText.text = "Score: " + score;
        ScoreChangedEvent?.Invoke(score);
    }

    public int GetScore()
    {
        return score;
    }
}

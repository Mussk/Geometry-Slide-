
using TMPro;
using Unity.Services.Leaderboards.Models;

using UnityEngine;


public class LeaderboardEntryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI place;
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private TextMeshProUGUI score;

    public void SetLeaderboardEntry(LeaderboardEntry leaderboardEntry)
    {
        place.text = (leaderboardEntry.Rank + 1).ToString();
        playerName.text = leaderboardEntry.PlayerName;
        score.text = ((int)leaderboardEntry.Score).ToString();
    }

    public void SetPlace(int placeToSet)
    {
        place.text = placeToSet.ToString();
    }
    public void SetPlayerName(string playerNameToSet)
    {
        playerName.text = playerNameToSet;
    }
    public void SetScore(int scoreToSet)
    {
        score.text = scoreToSet.ToString();
    }
    
}

using System;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Leaderboards;
using Unity.Services.Leaderboards.Models;

public class LeaderboardUIController : MonoBehaviour
{
    [SerializeField] private List<LeaderboardEntryUI> leaderboardEntries;
    [SerializeField] private LeaderboardEntryUI playerLeaderboardEntry;
    [SerializeField] private int leaderboardEntriesLimit = 10;
    [SerializeField] private int leaderboardEntriesOffset;
    
    public bool isGetDataFromServer = false;
    
    private async UniTask GetLeaderboardData()
    {
        var leaderBoardData = 
            await LeaderboardManager.Instance.GetPaginatedScores(leaderboardEntriesOffset, leaderboardEntriesLimit);
        
        if (leaderBoardData != null) 
        {
            BindDataToLeaderboardEntries(leaderBoardData);
            await BindPlayerLeaderboardEntry();
        }
    }

    private void BindDataToLeaderboardEntries(LeaderboardScoresPage leaderboardData)
    {
        foreach (var leaderboardDataEntry in leaderboardData.Results)
        {
            leaderboardEntries[leaderboardData.Results.IndexOf(leaderboardDataEntry)]
                .SetLeaderboardEntry(leaderboardDataEntry);
        }
    }

    private async UniTask BindPlayerLeaderboardEntry()
    {
            var playerLeaderboardData = await LeaderboardManager.Instance.GetPlayerScore();
            
            if (playerLeaderboardData != null)
                playerLeaderboardEntry.SetLeaderboardEntry(playerLeaderboardData);
            
    }

    public async UniTask ShowLeaderboard()
    {   
        
        if(isGetDataFromServer)
            await GetLeaderboardData();
        Debug.Log("Show Leaderboard");
        gameObject.SetActive(true);
    }
}

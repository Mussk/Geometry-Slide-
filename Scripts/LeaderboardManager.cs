using System;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using Unity.Services.Leaderboards.Models;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager Instance;
    public static bool IsPlayerNameWasSet = false;
    // Create a leaderboard with this ID in the Unity Dashboard
    private string LeaderboardId;

    private string VersionId { get; set; }
    private int Offset { get; set; }
    private int Limit { get; set; }
    int RangeLimit { get; set; }
    List<string> FriendIds { get; set; }
    [SerializeField] private TextAsset leaderboardIdFile;

    private async UniTask Awake()
    {   
        Instance = this;
        
        try
        {
            await UnityServices.InitializeAsync().AsUniTask();

            await SignInAnonymously();
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }

        LeaderboardId = GetLeaderboardId(leaderboardIdFile);
    }

    private string GetLeaderboardId(TextAsset leaderboardFile)
    {
        if (!leaderboardFile) throw new Exception("leaderboardIdFile is null");
        
        return leaderboardFile.text;
    }

    private async UniTask SignInAnonymously()
    {
        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed in as: " + AuthenticationService.Instance.PlayerId);
            
        };
        AuthenticationService.Instance.SignInFailed += s =>
        {
            // Take some action here...
            Debug.Log(s);
        };
        
        if (AuthenticationService.Instance.IsSignedIn) return;
        
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        
        await SetPlayerScoreAsHighestScore();
        
    }
    //on Inspector (Submit Button)
    public async UniTask SetPlayerName(TMP_InputField inputField)
    {   
        string playerName = inputField.text;
        if (playerName.Equals("")) return;
        await AuthenticationService.Instance.UpdatePlayerNameAsync(playerName);
        IsPlayerNameWasSet = true;
    }
    
    //on Inspector (Submit Button)
    public async UniTask SubmitScore()
    {   
        var scoreResponse = await LeaderboardsService.Instance.AddPlayerScoreAsync(LeaderboardId, ScoreManager.HighestScore);
        Debug.Log(JsonConvert.SerializeObject(scoreResponse));
    }

    public async UniTask GetScores()
    {
        var scoresResponse =
            await LeaderboardsService.Instance.GetScoresAsync(LeaderboardId);
        Debug.Log(JsonConvert.SerializeObject(scoresResponse));
    }

    public async UniTask<LeaderboardScoresPage> GetPaginatedScores(int offset, int limit)
    {
        try
        {
            var scoresResponse =
                await LeaderboardsService.Instance.GetScoresAsync(LeaderboardId,
                    new GetScoresOptions { Offset = offset, Limit = limit });
            Debug.Log(JsonConvert.SerializeObject(scoresResponse));

            return scoresResponse;
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            return null;
        }
    }

    public async UniTask SetPlayerScoreAsHighestScore()
    {
        try
        {
            var scoreResponse =
                await LeaderboardsService.Instance.GetPlayerScoreAsync(LeaderboardId);
            Debug.Log(JsonConvert.SerializeObject(scoreResponse));
        
            ScoreManager.HighestScore = (int)scoreResponse.Score;
            Debug.Log("Highest Score: " + ScoreManager.HighestScore);
        }
        catch (Exception e)
        {
            Debug.Log("Score was not set yet!");
            Debug.Log(e);
        }
    }

    public async UniTask<LeaderboardEntry> GetPlayerScore()
    {
        try
        {
            var scoreResponse =
                await LeaderboardsService.Instance.GetPlayerScoreAsync(LeaderboardId);

            return scoreResponse;
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            return null;
        }
    }
    

    public async UniTask GetVersionScores()
    {
        var versionScoresResponse =
            await LeaderboardsService.Instance.GetVersionScoresAsync(LeaderboardId, VersionId);
        Debug.Log(JsonConvert.SerializeObject(versionScoresResponse));
    }
}

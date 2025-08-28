using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubmitScoresButtonController : MonoBehaviour
{
    private Button _button;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject leaderboardUI;
    private void Awake()
    {
        _button = GetComponent<Button>();
        
        _button.onClick.AddListener(delegate
        {
            SubmitButtonListener().Forget();
        });
        
    }

    private async UniTask SubmitButtonListener()
    {
        await LeaderboardManager.Instance.SetPlayerName(inputField);
        await LeaderboardManager.Instance.SubmitScore();
        await leaderboardUI.GetComponent<LeaderboardUIController>().ShowLeaderboard();
        transform.parent.gameObject.SetActive(false);
    }
    
}

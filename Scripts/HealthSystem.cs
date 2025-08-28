using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class HealthSystem
{
    public int CurrentLives { get; private set; }
    public int MaxLives { get; private set; }
    
    private PlayerController _playerController;
    
    public static event Action DeathEvent;
    
    public static event Action<int> LivesChangedEvent; 

    public HealthSystem(PlayerController playerController, int maxLives)
    {
        _playerController = playerController; 
        MaxLives = maxLives;
        CurrentLives = maxLives;
    }

    public void TakeDamage()
    {   
        if(_playerController.isInvulreable) return;
        CurrentLives = Mathf.Max(0, CurrentLives - 1);
        LivesChangedEvent?.Invoke(CurrentLives);
        AudioManager.AudioLibrary.levelSounds.missSound.Play();
        if (CurrentLives == 0)
        {
            _playerController.IsDead = true;
            AudioManager.AudioLibrary.levelSounds.gameEndSound.Play();
            DeathEvent?.Invoke();
        }
              
    }

    public void Heal()
    {   
        if(_playerController.IsDead) return;
        CurrentLives = Mathf.Min(CurrentLives + 1, MaxLives);
        LivesChangedEvent?.Invoke(CurrentLives);
    }
}

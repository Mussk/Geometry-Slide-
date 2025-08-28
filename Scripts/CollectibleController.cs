using UnityEngine;
using TMPro;

public class CollectibleController : RailContent
{
    public CollectibleData data;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        var playerController = other.GetComponent<PlayerController>();
        Debug.Log($"Collected {data.collectibleId}, Score: {data.scoreValue}");

        if (data.isHealItem)
        {
            if (playerController.HealthSystem.CurrentLives == playerController.HealthSystem.MaxLives)
            {
               
                ScoreManager.Instance.AddScore(data.scoreValue);
                PopUpCanvas("+ " + data.scoreValue);
                AudioManager.AudioLibrary.levelSounds.cubePickUpSound.PlayOneShot(data.sound);
            }
            else
            {
                playerController.HealthSystem.Heal();
                PopUpCanvas("+ 1 life!");
                AudioManager.AudioLibrary.levelSounds.healSound.Play();
            }
            
            gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
            return;
        }
            
        if (data.collectibleId == playerController.currentFormId)
        {
            ScoreManager.Instance.AddScore(data.scoreValue);
            PopUpCanvas("+ " + data.scoreValue);
            AudioManager.AudioLibrary.levelSounds.cubePickUpSound.PlayOneShot(data.sound);
        }
        else
        {
            playerController.HealthSystem.TakeDamage();
        }
        
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }
}

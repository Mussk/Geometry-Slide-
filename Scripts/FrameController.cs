using TMPro;
using UnityEngine;

public class FrameController : RailContent
{   
    public FrameData frameData;
    
    private bool wasHit = false;

    public void EnterFrame()
    {
        wasHit = false;
        Debug.Log("Entered frame, press the key now!");
        AudioManager.AudioLibrary.levelSounds.frameAbleToHitSound.Play();
        
    }

    public void RegisterHit()
    {
        if (!wasHit)
        {
            wasHit = true;
            Debug.Log("Hit!");
            ScoreManager.Instance.AddScore(frameData.scoreValue);
            PopUpCanvas("+ " + frameData.scoreValue);
        }
    }

    public void Missed()
    {
        if (!wasHit)
        {   
            Debug.Log("Miss!");
            PopUpCanvas("Miss!");
        }
    }
    
   
}

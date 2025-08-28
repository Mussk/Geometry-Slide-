using System;
using UnityEngine;

public class FloorController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {       
            other.GetComponent<PlayerController>().IsGrounded = true;
            AudioManager.AudioLibrary.levelSounds.landSound.Play();
            other.GetComponent<PlayerVisualController>().PlayLandEffect();
            AudioManager.AudioLibrary.levelSounds.slideSound.Play();
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {       
            other.GetComponent<PlayerController>().IsGrounded = false;
            other.GetComponent<PlayerVisualController>().StopLandEffect();
            AudioManager.AudioLibrary.levelSounds.slideSound.Stop();
        }
    }
}

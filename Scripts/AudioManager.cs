using UnityEngine;

[System.Serializable]
public struct MainMenuSounds
{
    public AudioSource backgroundMusic1;
    public AudioSource backgroundMusic2;
    public AudioSource backgroundMusic3;
}

[System.Serializable]
public struct LevelSounds
{
    public AudioSource backgroundMusic1;
    public AudioSource backgroundMusic2;
    public AudioSource backgroundMusic3;
    
    public AudioSource slideSound;
    public AudioSource jumpSound;
    public AudioSource landSound;
    
    //also for cube/sphere/cone frame hit
    public AudioSource cubePickUpSound;
    public AudioSource conePickUpSound;
    public AudioSource spherePickUpSound;
    public AudioSource heartPickUpSound;

    public AudioSource healSound;
    
    public AudioSource missSound;

    public AudioSource frameAbleToHitSound;
    
    public AudioSource gameEndSound;
}

[System.Serializable]
public struct UISounds
{
    public AudioSource uIClickSound;
}


[System.Serializable]
public struct AudioLibrary 
{
    public MainMenuSounds mainMenuSounds;
    public LevelSounds levelSounds;
    public UISounds uISounds;
    
}

public class AudioManager : MonoBehaviour
{

    public static AudioLibrary AudioLibrary;

    [SerializeField]
    private AudioLibrary audioLibraryInstance;


    private void Awake()
    {   
           
        AudioLibrary = audioLibraryInstance;
       
    }

    public static void PlaySound(AudioSource soundToPlay) 
    { 
    
        soundToPlay.Play();
       
    }

    public static void StopSound(AudioSource soundToPlay)
    {

        soundToPlay.Stop();

    }
}
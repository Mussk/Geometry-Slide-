using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BackgroundMusicSelector : MonoBehaviour
{
   private void OnEnable()
   {
      var randNumber = Random.Range(0, 3);
      Debug.Log("Rand number: " + randNumber);
      switch (randNumber)
      {
         case 0:
            AudioManager.AudioLibrary.levelSounds.backgroundMusic1.Play();
            break;
         case 1:
            AudioManager.AudioLibrary.levelSounds.backgroundMusic2.Play();
            break;
         case 2:
            AudioManager.AudioLibrary.levelSounds.backgroundMusic3.Play();
            break;
      }

      HealthSystem.DeathEvent += StopMusicOnDeath;
   }

   private void OnDisable()
   {
      HealthSystem.DeathEvent -= StopMusicOnDeath;
   }

   private void StopMusicOnDeath()
   {
      AudioManager.AudioLibrary.levelSounds.backgroundMusic1.Stop(); 
      AudioManager.AudioLibrary.levelSounds.backgroundMusic2.Stop();
      AudioManager.AudioLibrary.levelSounds.backgroundMusic3.Stop();
   }
}

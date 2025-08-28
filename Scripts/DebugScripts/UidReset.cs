using Unity.Services.Authentication;
using UnityEngine;

namespace DebugScripts
{
    public class UidReset : MonoBehaviour
    {
            [ContextMenu("Reset Auth UID")]
            public void ResetAuth()
            {
                if (AuthenticationService.Instance.IsSignedIn)
                {
                    AuthenticationService.Instance.SignOut();
                }

                PlayerPrefs.DeleteAll();
                PlayerPrefs.Save();

                Debug.Log("Authentication reset. Restart the game to get a new UID.");
            }
        
    }
}

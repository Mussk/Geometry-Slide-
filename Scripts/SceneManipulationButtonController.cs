using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManipulationButtonController : MonoBehaviour
{
    [SerializeField]
    private int sceneToLoadIndex;

    private void OnEnable()
    {
        Button button = GetComponent<Button>();

        button.onClick.AddListener(delegate { LoadScene(sceneToLoadIndex); });
    }

    private void LoadScene(int sceneIndex)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneIndex);
        
        
    }
}

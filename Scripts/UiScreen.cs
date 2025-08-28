using System;

using UnityEngine;
using UnityEngine.Rendering;

public class UiScreen : MonoBehaviour
{   
    private GameObject _uiScreen;
    [SerializeField] private string tagOfScreen;
    [SerializeField] private Volume blurVolume;

    
    private static UiScreen _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            
            Destroy(gameObject); 
            return;
        }

        _instance = this;
        
    }

    private void OnDestroy()
    {
        if (_instance == this)
            _instance = null;
    }
    
    private void OnEnable()
    {
        _instance._uiScreen = GameObject.FindWithTag(tagOfScreen);
        _instance._uiScreen.SetActive(false);
        
    }

    public void ShowUI()
    {
        _instance._uiScreen.SetActive(true);
        _instance.blurVolume.enabled = true;
    }

    public void HideUI()
    {
        _instance._uiScreen.SetActive(false);
        _instance.blurVolume.enabled = false;
    }
}

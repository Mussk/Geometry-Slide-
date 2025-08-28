using TMPro;
using UnityEngine;

public abstract class RailContent : MonoBehaviour
{
    
    [SerializeField] private GameObject popupCanvas;
    
    protected void PopUpCanvas(string textToWrite)
    {
        if (popupCanvas == null) return;
        TextMeshProUGUI text = popupCanvas.GetComponentInChildren<TextMeshProUGUI>();
        text.text = textToWrite;
        popupCanvas.SetActive(true);
    }
}

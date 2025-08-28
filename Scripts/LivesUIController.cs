using System;
using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class LivesUIController : MonoBehaviour
{
    //private TextMeshProUGUI _livesText;
    public GameObject heartLeft;
    public GameObject heartCenter;
    public GameObject heartRight;
    
    private List<GameObject> lossOrder;
    private List<GameObject> gainOrder;
    
    private void OnEnable()
    {
        lossOrder = new List<GameObject> { heartCenter, heartLeft, heartRight };
        gainOrder = new List<GameObject> { heartRight, heartLeft, heartCenter };
        
        //_livesText = GetComponent<TextMeshProUGUI>();
        HealthSystem.LivesChangedEvent += OnLivesChanged;
    }

    private void OnDisable()
    {
        HealthSystem.LivesChangedEvent -= OnLivesChanged;
    }

    private void OnLivesChanged(int newLivesAmount)
    {
        //_livesText.text = "Lives: " + newLivesAmount;
        UpdateHearts(newLivesAmount);
    }
    
    private void UpdateHearts(int currentHealth)
    {
        // Turn off hearts in loss order based on current health
        for (int i = 0; i < lossOrder.Count; i++)
        {
            if (i < currentHealth)
                gainOrder[i].SetActive(true);
            else
                lossOrder[i].SetActive(false);
        }
    }
}

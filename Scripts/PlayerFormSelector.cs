using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFormSelector : MonoBehaviour
{
    [SerializeField] private List<GameObject> playerForms;
    [SerializeField] private int defaultFormId;
    
    private PlayerController _playerController;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();

        _playerController.PlayerInputSystem.Player.ChooseFormCube.performed += ChangeFormCube;
        _playerController.PlayerInputSystem.Player.ChooseFormSphere.performed += ChangeFormSphere;
        _playerController.PlayerInputSystem.Player.ChooseFormCone.performed += ChangeFormCone;
        
        _playerController.currentFormId = defaultFormId;
    }

    private void ChangeFormCube(InputAction.CallbackContext ctx)
    {   
        if(_playerController.IsDead) return;
        foreach (var playerForm in playerForms)
        {
            playerForm.gameObject.SetActive(false);
        }
        playerForms[0].SetActive(true);
        _playerController.currentFormId = 1;
            
    }
    
    private void ChangeFormSphere(InputAction.CallbackContext ctx)
    {   
        if(_playerController.IsDead) return;
        foreach (var playerForm in playerForms)
        {
            playerForm.gameObject.SetActive(false);
        }
        playerForms[1].SetActive(true);
        _playerController.currentFormId = 2;
       
    }
    
    private void ChangeFormCone(InputAction.CallbackContext ctx)
    { 
        if(_playerController.IsDead) return;
        
        foreach (var playerForm in playerForms)
        {
            playerForm.gameObject.SetActive(false);
        }
        playerForms[2].SetActive(true);
        _playerController.currentFormId = 3;
    }
}


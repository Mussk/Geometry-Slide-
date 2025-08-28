using System;
using UnityEngine;

public class PlayerVisualController : MonoBehaviour
{
    
    [SerializeField] private ParticleSystem sparksParticleSystem;
    [SerializeField] private ParticleSystem landEffect;
    private PlayerController _playerController;
    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (_playerController.IsGrounded)
        {
            sparksParticleSystem.Play();
        }
        else
        {
            sparksParticleSystem.Stop();
        }
    }

    public void PlayLandEffect()
    {
        landEffect.Play();
    }

    public void StopLandEffect()
    {
        landEffect.Stop();
    }
}

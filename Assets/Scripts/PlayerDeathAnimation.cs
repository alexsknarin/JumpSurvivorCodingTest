using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerDeathAnimation : MonoBehaviour
{
    [SerializeField] private VisualEffect _deathAnimationVFX;
    [SerializeField] private GameObject _playerViewBase;

    private void OnEnable()
    {
        Game.OnGameOver += PlayDeathAnimation;
    }

    private void OnDisable()
    {
        Game.OnGameOver -= PlayDeathAnimation;
    }

    private void PlayDeathAnimation()
    {
        _playerViewBase.SetActive(false);
        _deathAnimationVFX.gameObject.SetActive(true);
    }
}

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerHealAnimation : MonoBehaviour
{
    [SerializeField] private VisualEffect _healAnimationVFX;
    private WaitForSeconds _waitForHeal;

    private void Awake()
    {
        _waitForHeal = new WaitForSeconds(1f);
    }

    private IEnumerator WaitForHeal()
    {
        yield return _waitForHeal;
        _healAnimationVFX.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        PlayerCollisionHandler.MedkitCollided += PlayerCollisionHandler_MedkitCollided;
    }
    private void OnDisable()
    {
        PlayerCollisionHandler.MedkitCollided -= PlayerCollisionHandler_MedkitCollided;
    }
    /// <summary>
    /// Play Medkit Heal Animation
    /// </summary>
    private void PlayerCollisionHandler_MedkitCollided(Vector3 pos)
    {
        _healAnimationVFX.gameObject.SetActive(true);
        StartCoroutine(WaitForHeal());
    }
}

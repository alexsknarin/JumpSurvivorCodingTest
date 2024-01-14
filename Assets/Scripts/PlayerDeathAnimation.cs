using UnityEngine;
using UnityEngine.VFX;

/// <summary>
/// Start Player Death Animation using VFX graph particles.
/// </summary>
public class PlayerDeathAnimation : MonoBehaviour
{
    [SerializeField] private VisualEffect _deathAnimationVFX;
    [SerializeField] private GameObject _playerViewBase;

    private void OnEnable()
    {
        Game.GameOver += Game_GameOver;
    }

    private void OnDisable()
    {
        Game.GameOver -= Game_GameOver;
    }

    /// <summary>
    /// Play Death Animation
    /// </summary>
    private void Game_GameOver()
    {
        _playerViewBase.SetActive(false);
        _deathAnimationVFX.gameObject.SetActive(true);
    }
}

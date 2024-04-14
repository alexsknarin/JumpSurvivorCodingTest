using UnityEngine;

/// <summary>
/// Make Player character aatar flash with a given color. Subscribed to EnemyCollision and indicated received Damage.
/// </summary>
public class PlayerFlashingHit : MonoBehaviour
{
    [SerializeField] private FloatVariable _gameTime;
    [SerializeField] private Color _flashColor;
    [SerializeField] private float _flashFreq;
    [SerializeField] private float _flashDuration;
    [SerializeField] private SpriteRenderer _catSpriteRenderer;
    private float _flashValue;
    private Material _material;
    private bool _isFlashing;
    private float _prevTime;
    
    private void Awake()
    {
        _material = _catSpriteRenderer.sharedMaterial;
        _isFlashing = false;
    }
    
    private void OnEnable()
    {
        PlayerCollisionHandler.EnemyCollided += StartFlashing;
        PlayerHealth.PlayerInvincibilityFinished += StopFlashing;
    }

    private void OnDisable()
    {
        PlayerCollisionHandler.EnemyCollided -= StartFlashing;
        PlayerHealth.PlayerInvincibilityFinished -= StopFlashing;
        _material.SetColor("_Color", Color.white);
        _material.SetFloat("_HitMix", 0);
    }

    private void Update()
    {
        if (_isFlashing)
        {
            PerformFlashing();    
        }
    }

    private void PerformFlashing()
    {
        _flashValue = (Mathf.Sin(_gameTime.Value * _flashFreq) + 1f) / 2f; 
        _material.SetColor("_Color", _flashColor);
        _material.SetFloat("_HitMix", _flashValue);
    }
    
    /// <summary>
    /// Start flashing
    /// </summary>
    private void StartFlashing()
    {
        _isFlashing = true;
    }
    
    /// <summary>
    /// Stop flashing
    /// </summary>
    private void StopFlashing()
    {
        _isFlashing = false;
        _material.SetColor("_Color", Color.white);
        _material.SetFloat("_HitMix", 0f);
    }
}

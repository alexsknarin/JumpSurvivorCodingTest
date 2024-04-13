using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for lifebar and health icon management. Link to _bonusTextTransform is for hearts heal animation;
/// </summary>
public class PlayerHealthUIView : MonoBehaviour
{
    [SerializeField] private HealthHeartAnimation _healthIconPrefab;
    [SerializeField] private IntVariable _currentHealth;
    [SerializeField] private IntVariable _maxHealth;
    [SerializeField] private float _iconMargin;
    [SerializeField] private Transform _bonusTextTransform;
    [SerializeField] private Transform _inGameUIRect;
    private Vector3 _newPosition = Vector3.zero;
    private Vector3 _newLocalPosition = Vector3.zero;
    private List<HealthHeartAnimation> _healthIcons = new List<HealthHeartAnimation>();
    private float _nearDeathHealthNumber;

    private void OnEnable()
    {
        PlayerHealth.HealthDecreased += PlayerHealth_HealthDecreased;
        PlayerHealth.PlayerHealthSetUp += PlayerHealth_PlayerHealthSetUp;
        PlayerHealth.HealthIncreased += PlayerHealth_HealthIncreased;
        PlayerHealth.NearDeathStarted += PlayerHealth_NearDeathStarted;
        PlayerHealth.NearDeathEnded += PlayerHealth_NearDeathEnded;
    }

    private void OnDisable()
    {
        PlayerHealth.HealthDecreased -= PlayerHealth_HealthDecreased;
        PlayerHealth.PlayerHealthSetUp -= PlayerHealth_PlayerHealthSetUp;
        PlayerHealth.HealthIncreased -= PlayerHealth_HealthIncreased;
        PlayerHealth.NearDeathStarted -= PlayerHealth_NearDeathStarted;
        PlayerHealth.NearDeathEnded -= PlayerHealth_NearDeathEnded;
    }
   
    public void PlayerHealth_PlayerHealthSetUp()
    {
        _newPosition = transform.position;
        
        for (int i = 0; i < _maxHealth.Value; i++)
        {
            HealthHeartAnimation newHealthHeartPrefab = Instantiate(_healthIconPrefab, _newPosition, _healthIconPrefab.transform.rotation, _inGameUIRect);
            _newLocalPosition = newHealthHeartPrefab.transform.localPosition;
            _newLocalPosition.x -= _iconMargin * i;
            newHealthHeartPrefab.transform.localPosition = _newLocalPosition;
            newHealthHeartPrefab.SaveInitialState(newHealthHeartPrefab.transform.position, _bonusTextTransform.position);
            _healthIcons.Add(newHealthHeartPrefab);
        }
    }

    private void PlayerHealth_NearDeathStarted()
    {
        foreach (var icon in _healthIcons)
        {
            icon.StartNearDeath();
        }
    }

    private void PlayerHealth_NearDeathEnded()
    {
        foreach (var icon in _healthIcons)
        {
            icon.StopNearDeath();
        }
    }
    
    private void PlayerHealth_HealthDecreased()
    {
        _healthIcons[_currentHealth.Value].StartDamageAnimation();
    }

    private void PlayerHealth_HealthIncreased(int mode, Vector3 pos)
    {
        Vector2 canvasPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, pos);
        _healthIcons[_currentHealth.Value-1].StartHealAnimation(mode, canvasPosition);
    }
}

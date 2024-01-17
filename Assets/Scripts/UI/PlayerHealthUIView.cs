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
    private Vector3 _newPosition = Vector3.zero;
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
        // Generate Lifebar items
        for (int i = 0; i < _maxHealth.Value; i++)
        {
            _newPosition.x -= _iconMargin;
            HealthHeartAnimation newHealthHeartPrefab = Instantiate(_healthIconPrefab, Vector3.zero, _healthIconPrefab.transform.rotation, transform);
            newHealthHeartPrefab.transform.localPosition = _newPosition;
            newHealthHeartPrefab.SaveInitialState(_newPosition, transform.localPosition, _bonusTextTransform.localPosition);
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
        Debug.Log(pos); 
        _healthIcons[_currentHealth.Value-1].StartHealAnimation(mode, canvasPosition);
    }
}

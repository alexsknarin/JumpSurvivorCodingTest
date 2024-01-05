using System;
using UnityEngine;

public class BonusViewHandler : MonoBehaviour
{
    [SerializeField] private GameObject _bonusAnimated;
    [SerializeField] private RectTransform _canvasRectTransform;
    private void OnEnable()
    {
        PlayerCollisionHandler.OnBonusCollided += SpawnBonusAnimation;
    }

    private void OnDisable()
    {
        PlayerCollisionHandler.OnBonusCollided -= SpawnBonusAnimation;
    }

    private void SpawnBonusAnimation(int enemy, Vector3 playerPosition)
    {
        Vector2 canvasPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, playerPosition);
        Instantiate(_bonusAnimated, canvasPosition, _bonusAnimated.transform.rotation, transform);
    }
}

using System;
using UnityEngine;

public class BonusViewHandler : MonoBehaviour
{
    [SerializeField] private GameObject _bonusAnimated;
    [SerializeField] private RectTransform _canvasRectTransform;
    [SerializeField] private RectTransform _bonusTextTransform;
    [SerializeField] private int _maxBonusPointOnScreen;
    

    private ObjectPool _bonusAnimatedPool;

    private void Start()
    {
        InitSpawn();
    }

    public void InitSpawn()
    {
        _bonusAnimatedPool = new ObjectPool(_maxBonusPointOnScreen, _bonusAnimated);
    }
    
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
        GameObject currentBonusPointsView = _bonusAnimatedPool.GetPooledObject();
        if (currentBonusPointsView == null)
        {
            Debug.Log("No object to spawn");
            return;
        }
        Debug.Log(currentBonusPointsView.name);
        currentBonusPointsView.GetComponent<BonusPointsView>().SpawnSetup(5, canvasPosition, _bonusTextTransform);
        currentBonusPointsView.transform.parent = _canvasRectTransform;
        
    }
}

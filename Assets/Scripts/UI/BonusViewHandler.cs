using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class BonusViewHandler : MonoBehaviour
{
    [SerializeField] private GameObject _bonusAnimated;
    [SerializeField] private GameObject _bonusStarsAnimated;
    [SerializeField] private TMP_Text _bonusText;
    [SerializeField] private BonusTextAnimation _bonusTextAnimation;
    [SerializeField] private RectTransform _canvasRectTransform;
    [SerializeField] private RectTransform _bonusTextTransform;
    [SerializeField] private int _maxBonusPointOnScreen;
    [SerializeField] private IntVariable _bonusPoints;
    private ObjectPool _bonusAnimatedPool;
    private ObjectPool _bonusStarFxPool;
    private int _currentBonus;

    private void Start()
    {
        _bonusText.text = "";
        InitSpawn();
    }
    
    private void OnEnable()
    {
        BonusPointsManager.BonusUpdated += BonusPointsManager_BonusUpdated;
    }

    private void OnDisable()
    {
        BonusPointsManager.BonusUpdated -= BonusPointsManager_BonusUpdated;
    }
    
    public void InitSpawn()
    {
        _bonusAnimatedPool = new ObjectPool(_maxBonusPointOnScreen, _bonusAnimated);
        _bonusStarFxPool = new ObjectPool(_maxBonusPointOnScreen, _bonusStarsAnimated);
        
    }

    private void BonusPointsManager_BonusUpdated(int currentBonus, Vector3 playerPosition)
    {
        Vector2 canvasPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, playerPosition);
        GameObject currentBonusPointsView = _bonusAnimatedPool.GetPooledObject();
        GameObject currentBonusStarsFX = _bonusStarFxPool.GetPooledObject();
        if (currentBonusPointsView == null)
        {
            return;
        }
        currentBonusPointsView.transform.SetParent(_canvasRectTransform);
        currentBonusPointsView.GetComponent<BonusPointsView>().SpawnSetup(
            currentBonus, 
            canvasPosition, 
            _bonusTextTransform);
        currentBonusStarsFX.transform.SetParent(_canvasRectTransform);
        currentBonusStarsFX.transform.position = canvasPosition;
        currentBonusStarsFX.GetComponent<StarExplosion>().Play();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BonusPointsAnimation"))
        {
            other.gameObject.SetActive(false);
            _bonusTextAnimation.PlayAnimation();
            _bonusText.text = $"+{_bonusPoints.Value}";
        }
    }
}

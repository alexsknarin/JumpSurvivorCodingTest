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
    
    private GenericObjectPool _bonusAnimatedPool;
    private GenericObjectPool _bonusStarFxPool;
    
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
        _bonusAnimatedPool = new GenericObjectPool(_maxBonusPointOnScreen, _bonusAnimated);
        _bonusStarFxPool = new GenericObjectPool(_maxBonusPointOnScreen, _bonusStarsAnimated);
        
    }

    private void BonusPointsManager_BonusUpdated(int currentBonus, Vector3 playerPosition)
    {
        Vector2 canvasPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, playerPosition);
        GameObject currentBonusPointsView = _bonusAnimatedPool.Get();
        GameObject currentBonusStarsFX = _bonusStarFxPool.Get();
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
            other.gameObject.GetComponent<GenericObjectPoolClient>().Release();
            _bonusTextAnimation.PlayAnimation();
            _bonusText.text = $"+{_bonusPoints.Value}";
        }
    }
}

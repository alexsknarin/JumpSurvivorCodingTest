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
    private WaitForSeconds _bonusTextUpdateDelay;
    private ObjectPool _bonusAnimatedPool;
    private ObjectPool _bonusStarFxPool;
    private int _currentBonus;

    private void Start()
    {
        _bonusTextUpdateDelay = new WaitForSeconds(0.5f);
        _bonusText.text = "";
        InitSpawn();
    }
    
    public void InitSpawn()
    {
        _bonusAnimatedPool = new ObjectPool(_maxBonusPointOnScreen, _bonusAnimated);
        _bonusStarFxPool = new ObjectPool(_maxBonusPointOnScreen, _bonusStarsAnimated);
        
    }
    
    private void OnEnable()
    {
        BonusPointsManager.OnBonusUpdated += SpawnBonusAnimation;
    }

    private void OnDisable()
    {
        BonusPointsManager.OnBonusUpdated -= SpawnBonusAnimation;
    }

    private void SpawnBonusAnimation(int currentBonus, Vector3 playerPosition)
    {
        Vector2 canvasPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, playerPosition);
        GameObject currentBonusPointsView = _bonusAnimatedPool.GetPooledObject();
        GameObject currentBonusStarsFX = _bonusStarFxPool.GetPooledObject();
        if (currentBonusPointsView == null)
        {
            return;
        }
        currentBonusPointsView.transform.SetParent(_canvasRectTransform);
        currentBonusPointsView.GetComponent<BonusPointsView>().SpawnSetup(currentBonus, canvasPosition, _bonusTextTransform);
        currentBonusStarsFX.transform.SetParent(_canvasRectTransform);
        currentBonusStarsFX.GetComponent<StarSpawner>().SpawnSetup(canvasPosition);
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

using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class BonusViewHandler : MonoBehaviour
{
    [SerializeField] private GameObject _bonusAnimated;
    [SerializeField] private TMP_Text _bonusText;
    [SerializeField] private RectTransform _canvasRectTransform;
    [SerializeField] private RectTransform _bonusTextTransform;
    [SerializeField] private int _maxBonusPointOnScreen;
    [SerializeField] private IntVariable _bonusPoints;
    private WaitForSeconds _bonusTextUpdateDelay;
    

    private ObjectPool _bonusAnimatedPool;

    private void Start()
    {
        _bonusTextUpdateDelay = new WaitForSeconds(0.5f);
        _bonusText.text = "";
        InitSpawn();
    }

    IEnumerator BonusTextUpdateDelay()
    {
        yield return _bonusTextUpdateDelay;
        _bonusText.text = $"+{_bonusPoints.Value}";
    }
    
    public void InitSpawn()
    {
        _bonusAnimatedPool = new ObjectPool(_maxBonusPointOnScreen, _bonusAnimated);
    }
    
    private void OnEnable()
    {
        BonusPointsManager.OnBonusUpdated += SpawnBonusAnimation;
    }

    private void OnDisable()
    {
        BonusPointsManager.OnBonusUpdated -= SpawnBonusAnimation;
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

        StartCoroutine(BonusTextUpdateDelay());
    }
}

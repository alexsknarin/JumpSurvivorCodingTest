using CandyCoded.HapticFeedback;
using System.Collections;
using UnityEngine;

public class OnscreenButtonMobileHaptic : MonoBehaviour
{
    private bool _hapticAllowed = true;
    private WaitForSeconds _waitForHapticBlocked;
    
    private void Start()
    {
        _waitForHapticBlocked = new WaitForSeconds(0.8f);
    }
    
    private void OnEnable()
    {
        PlayerCollisionHandler.EnemyCollided += PlayerCollisionHandler_EnemyCollided;
    }

    private void OnDisable()
    {
        PlayerCollisionHandler.EnemyCollided -= PlayerCollisionHandler_EnemyCollided;
    }
    
    public void Perform()
    {
        if (_hapticAllowed)
        {
            HapticFeedback.HeavyFeedback();
        }
    }
    
    private void PlayerCollisionHandler_EnemyCollided()
    {
        if (_hapticAllowed)
        {
            _hapticAllowed = false;
            StartCoroutine(WaitForHapticUnblock());            
        }
    }
    
    private IEnumerator WaitForHapticUnblock()
    {
        yield return _waitForHapticBlocked;
        _hapticAllowed = true;
    }
}

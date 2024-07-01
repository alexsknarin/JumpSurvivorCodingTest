#if UNITY_ANDROID
using CandyCoded.HapticFeedback;
#endif

using System.Collections;
using UnityEngine;

public class OnscreenJumpButtonMobileHaptic : MonoBehaviour
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
    
    public void ButtonVibrate()
    {
        if (_hapticAllowed)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            HapticFeedback.HeavyFeedback();
#endif
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

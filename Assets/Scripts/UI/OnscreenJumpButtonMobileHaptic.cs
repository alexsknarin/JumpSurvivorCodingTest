using CandyCoded.HapticFeedback;
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
        PlayerCollisionHandler.OnEnemyCollided += BlockHaptic;
    }

    private void OnDisable()
    {
        PlayerCollisionHandler.OnEnemyCollided -= BlockHaptic;
    }
    
    private void BlockHaptic()
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

    public void ButtonVibrate()
    {
        if (_hapticAllowed)
        {
            HapticFeedback.HeavyFeedback();
        }
    }
}

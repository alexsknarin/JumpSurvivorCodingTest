using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnscreenJumpButtonMobileHaptic : MonoBehaviour
{
    private bool _hapticAllowed = true;
    private WaitForSeconds _waitForHapticBlocked;
    
    private void Start()
    {
        _waitForHapticBlocked = new WaitForSeconds(0.8f);
    }
}

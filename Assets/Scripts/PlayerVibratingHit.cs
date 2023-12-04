using UnityEngine;

public class PlayerVibratingHit : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerCollisionHandler.OnEnemyCollided += Vibrate;
    }

    private void OnDisable()
    {
        PlayerCollisionHandler.OnEnemyCollided -= Vibrate;
    }
    
    void Vibrate()
    {
        Handheld.Vibrate();
    }
}

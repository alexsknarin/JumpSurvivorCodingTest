using UnityEngine;

public class EnvironmentParallax : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;    
    
    [SerializeField] private Transform _l01SkyTransform;
    [SerializeField] private Transform _l02MountainsTransform;
    [SerializeField] private Transform _l03CityTransform;
    [SerializeField] private Transform _l04NearBgTransform;
    [SerializeField] private Transform _fgTransform;
    
    [SerializeField] private float _l01SkyRange;
    [SerializeField] private float _l02MountainsRange;
    [SerializeField] private float _l03CityRange;
    [SerializeField] private float _l04NearBgRange;
    [SerializeField] private float _fgRange;


    private float GetPlayerJumpPhase(float playerY)
    {
        return (playerY-0.5f)/4.0f;
    }
    
    void Update()
    {
        Vector3 pos = Vector3.zero;

        float playerPhase = GetPlayerJumpPhase(_playerTransform.position.y);
       
        pos.y = Mathf.Lerp(0, _l01SkyRange, playerPhase);
        _l01SkyTransform.localPosition = pos;
        
        pos.y = Mathf.Lerp(0, _l02MountainsRange, playerPhase);
        _l02MountainsTransform.localPosition = pos;
        
        pos.y = Mathf.Lerp(0, _l03CityRange, playerPhase);
        _l03CityTransform.localPosition = pos;
        
        pos = _l04NearBgTransform.localPosition;
        pos.y = Mathf.Lerp(0, _l04NearBgRange, playerPhase);
        _l04NearBgTransform.localPosition = pos;
        
        Vector3 scale = _fgTransform.localScale;
        scale.y = Mathf.Lerp(1, _fgRange, playerPhase);
        _fgTransform.localScale = scale;
    }
}

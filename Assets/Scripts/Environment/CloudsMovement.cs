using UnityEngine;

public class CloudsMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _cloudsRangeMin;
    [SerializeField] private float _cloudsRangeMax;
    [SerializeField] private Transform _cloudsTransform;
    
    

    // Update is called once per frame
    void Update()
    {
        float phase = (Mathf.Sin(Time.time * _speed) + 1.0f) * 0.5f;
        Vector3 pos = _cloudsTransform.localPosition;
        pos.x = Mathf.Lerp(_cloudsRangeMin, _cloudsRangeMax, phase);
        _cloudsTransform.localPosition = pos;
    }
}

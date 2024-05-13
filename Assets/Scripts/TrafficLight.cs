using System;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    [SerializeField] private TrafficLightsModes Mode = TrafficLightsModes.Off;
    [SerializeField] private GameObject _redLight;
    [SerializeField] private GameObject _greenLight;
    
    

    private void Start()
    {
        SetMode(TrafficLightsModes.Red); 
    }
    
    public void SetMode(TrafficLightsModes mode)
    {
        Mode = mode;
        switch (Mode)
        {
            case TrafficLightsModes.Off:
                _redLight.SetActive(false);
                _greenLight.SetActive(false);
                break;
            case TrafficLightsModes.Red:
                _redLight.SetActive(true);
                _greenLight.SetActive(false);
                break;
            case TrafficLightsModes.Green:
                _redLight.SetActive(false);
                _greenLight.SetActive(true);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

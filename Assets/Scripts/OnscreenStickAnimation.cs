using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnscreenStickAnimation : MonoBehaviour
{
    [SerializeField] private Image _stickImage;
    [SerializeField] private Sprite _neutralPositionImage;
    [SerializeField] private Sprite _leftPositionImage;
    [SerializeField] private Sprite _rightPositionImage;
    private Color _activeColor = new Color(1f, 1f, 1f, 0.3f);
    private Color _passiveColor = new Color(1f, 1f, 1f, 0.1f);


    public void OnPressed()
    {
        _stickImage.color = _activeColor;
    }
    
    public void OnReleased()
    {
        _stickImage.color = _passiveColor;
    }

    private void Awake()
    {
        _stickImage.color = _passiveColor;
    }

    private void OnEnable()
    {
        PlayerInputHandler.OnStickNeutralPositionEnterEvent += SetNeutralPosition;
        PlayerInputHandler.OnStickLeftPositionEnterEvent += SetLeftPosition;
        PlayerInputHandler.OnStickRightPositionEnterEvent += SetRightPosition;
    }

    private void OnDisable()
    {
        PlayerInputHandler.OnStickNeutralPositionEnterEvent -= SetNeutralPosition;
        PlayerInputHandler.OnStickLeftPositionEnterEvent -= SetLeftPosition;
        PlayerInputHandler.OnStickRightPositionEnterEvent -= SetRightPosition;
    }

    void SetNeutralPosition()
    {
        _stickImage.sprite = _neutralPositionImage;
    }

    void SetLeftPosition()
    {
        _stickImage.sprite = _leftPositionImage;
    }

    void SetRightPosition()
    {
        _stickImage.sprite = _rightPositionImage;
    }
    

}

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

    private void Awake()
    {
        _stickImage.color = _passiveColor;
    }

    private void OnEnable()
    {
        PlayerInputHandler.StickPositionChanged += PlayerInputHandler_StickPositionChanged;
    }

    private void OnDisable()
    {
        PlayerInputHandler.StickPositionChanged -= PlayerInputHandler_StickPositionChanged;
    }
    
    public void OnPressed()
    {
        _stickImage.color = _activeColor;
    }
    
    public void OnReleased()
    {
        _stickImage.color = _passiveColor;
    }

    private void PlayerInputHandler_StickPositionChanged(float xValue)
    {
        if (xValue < -0.8f)
        {
            SetLeftPosition();
        }

        if ((xValue > -0.8f) && (xValue < 0.8f))
        {
            SetNeutralPosition();
        }
            
        if (xValue > 0.8f)
        {
            SetRightPosition();
        }
    }
    
    private void SetNeutralPosition()
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

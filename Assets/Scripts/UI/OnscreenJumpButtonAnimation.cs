using UnityEngine;
using UnityEngine.UI;
public class OnscreenJumpButtonAnimation : MonoBehaviour
{
    [SerializeField] private Image _jumpButtonImage;
    private Color _activeColor = new Color(1f, 1f, 1f, 0.3f);
    private Color _passiveColor = new Color(1f, 1f, 1f, 0.1f);

    private void Awake()
    {
        _jumpButtonImage.color = _passiveColor;
    }

    public void OnPressed()
    {
        _jumpButtonImage.color = _activeColor;
    }
    
    public void OnReleased()
    {
        _jumpButtonImage.color = _passiveColor;
    }
}

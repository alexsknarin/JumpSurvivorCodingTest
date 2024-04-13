using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;

[AddComponentMenu("Input/On-Screen Button")]
public class OnScreenMoveButton : OnScreenControl, IPointerEnterHandler, IPointerExitHandler
{
    [InputControl(layout = "Button")]
    [SerializeField] private string _controlPath;
    
    protected override string controlPathInternal
    {
        get => _controlPath;
        set => _controlPath = value;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        SendValueToControl(1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SendValueToControl(0f);
    }
}


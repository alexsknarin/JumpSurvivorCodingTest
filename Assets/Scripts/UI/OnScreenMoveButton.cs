using UnityEngine;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.EventSystems;

[AddComponentMenu("Input/On-Screen Button")]
public class OnScreenMoveButton : OnScreenControl, IPointerEnterHandler, IPointerExitHandler
{
    [InputControl(layout = "Button")]
    [SerializeField]
    private string _ControlPath;

    protected override string controlPathInternal
    {
        get => _ControlPath; 
        set => _ControlPath = value;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SendValueToControl(1.0f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SendValueToControl(0.0f);
    }
}

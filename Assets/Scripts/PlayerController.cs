using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Get value of the direction of mouse scroll.
    public UnityAction<int> OnScroll;
    private PlayerInputActions _actions;

    /// <summary>
    /// Subscribe to player input action.
    /// </summary>
    private void Awake()
    {
        _actions = new PlayerInputActions();
        _actions.Action.MouseScrolling.performed += scroll => GetDirectionOfScroll(scroll);
    }

    /// <summary>
    /// If mouse scrolling up returns 1, if mouse scrolling down returns -1,
    /// then send value to OnScroll action.
    /// </summary>
    /// <param name="callbackContext">scroll</param>
    private void GetDirectionOfScroll(InputAction.CallbackContext callbackContext)
    {
        var result = 0;
        var scrollingDirection = callbackContext.ReadValue<float>();
        if (scrollingDirection > 0)
        {
            result = 1;
            OnScroll.Invoke(result);
        }
        else if (scrollingDirection < 0)
        {
            result = -1;
            OnScroll.Invoke(result);
        }
    }

    private void OnEnable()
    {
        _actions.Enable();
    }

    private void OnDisable()
    {
        _actions.Disable();
    }
}
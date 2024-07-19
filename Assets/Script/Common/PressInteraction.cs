using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.Events;

public class PressInteraction : MonoBehaviour
{

    private UnityAction action;
    private void OnEnable()
    {
        action += VirtualInputManager.Instance.InputDownDown;
    }
    // 押された瞬間のコールバック
    public void OnPress(InputAction.CallbackContext context)
    {
        // 押された瞬間でPerformedとなる
        if (!context.performed) return;
        Debug.Log("Press");
    }

    // 離された瞬間のコールバック
    public void OnRelease(InputAction.CallbackContext context)
    {
        // 離された瞬間でPerformedとなる
        if (!context.performed) return;

        Debug.Log("Release");
    }

    // 押された瞬間と離された瞬間のコールバック
    public void OnBoth(InputAction.CallbackContext context)
    {
        // 両方の瞬間でPerformedとなる
        if (!context.performed) return;

        Debug.Log("Both");
    }
}

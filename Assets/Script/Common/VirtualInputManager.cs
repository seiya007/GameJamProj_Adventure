using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using Vector3 = UnityEngine.Vector3;


public class VirtualInputManager : SingletonCustom<VirtualInputManager>
{
    // Actionをインスペクターから編集できるようにする
    [SerializeField] private InputAction _action;
    [SerializeField] private InputAction _stickX;
    [SerializeField] private InputAction _stickY;
    [SerializeField] private InputAction _actionInputDown;
    // Start is called before the first frame update

    public UnityEvent InputUpAction;
    public UnityEvent InputDownAction;
    public UnityEvent InputLeftAction;
    public UnityEvent InputRightAction;
    public UnityEvent InputInteractionAction;

    private InputAction move;
    private PlayerInput playerInput;

    public Vector3 StickDir;

    public enum ButtonType
    {
        A,
        B,
        X,
        Y,
        DpadUp,
        DpadDown,
        DpadRight,
        DpadLeft,
        StickL,
        StickR,
        RS,
        LS,
        RT,
        LT,
    }
    // 有効化
    private void OnEnable()
    {
        // InputActionを有効化
        // これをしないと入力を受け取れないことに注意
        _action?.Enable();
        _stickX?.Enable();
        _stickY?.Enable();
        _actionInputDown?.Enable();
        playerInput = GetComponent<PlayerInput>();
        move = playerInput.actions["Move"];
    }

    // 無効化
    private void OnDisable()
    {
        // 自身が無効化されるタイミングなどで
        // Actionを無効化する必要がある
        _action?.Disable();
        _stickX?.Disable();
        _stickY?.Disable();
        _actionInputDown?.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        // 現在のキーボード情報
        var current = Keyboard.current;

        // キーボード接続チェック
        if (current == null)
        {
            // キーボードが接続されていないと
            // Keyboard.currentがnullになる
            return;
        }

        if (_action == null) return;
        if (_stickX == null) return;
        if (_stickY == null) return;
        if (_actionInputDown == null) return;

        // Actionの入力値を読み込む
        var value = _action.ReadValue<float>();
        var valueStickX = _stickX.ReadValue<float>();
        var valueStickY = _stickY.ReadValue<float>();
        var stickValue = new Vector3(valueStickX,0.0f,valueStickY);
        StickDir = stickValue;
        // // 入力値をログ出力
        // Debug.Log($"Actionの入力値 : {value}");
        // Debug.Log($"Stickの入力値 : {valueStick}");
    }

    public bool GetButton(ButtonType _button)
    {
        switch(_button)
        {
            case ButtonType.A:
                if (_action == null) return false;
                // Actionの入力値を読み込む
                var value = _action.ReadValue<float>();
                return value > 0;
                // break;
            case ButtonType.B:
                break;
            case ButtonType.X:
                break;
            case ButtonType.Y:
                break;
            case ButtonType.DpadUp:
                break;
            case ButtonType.DpadDown:
                break;
            case ButtonType.DpadRight:
                break;
            case ButtonType.DpadLeft:
                break;
            case ButtonType.StickL:
                break;
            case ButtonType.StickR:
                break;
            case ButtonType.RS:
                break;
            case ButtonType.LS:
                break;
            case ButtonType.RT:
                break;
            case ButtonType.LT:
                break;
            default:
                return false;
        }
        return false;
    }

    public bool InputLeft()
    {
        if (_stickX == null) return false;
        // Actionの入力値を読み込む
        var value = _stickX.ReadValue<float>();
        return value < 0;
    }

    public bool InputRight()
    {
        if (_stickX == null) return false;
        // Actionの入力値を読み込む
        var value = _stickX.ReadValue<float>();
        return value > 0;
    }    
    public bool InputDown()
    {
        if (_stickY == null) return false;
        // Actionの入力値を読み込む
        var value = _stickY.ReadValue<float>();
        return value < 0;
    }

    public bool InputUp()
    {
        if (_stickY == null) return false;
        // Actionの入力値を読み込む
        var value = _stickY.ReadValue<float>();
        return value > 0;
    }

    public void InputDownDown()
    {
    }

    // // 押された瞬間のコールバック
    // public void OnPressUp(InputAction.CallbackContext context)
    // {
    //     // 押された瞬間でPerformedとなる
    //     if (!context.performed) return;
    //     Debug.Log("PressUp");
        
    //     if(InputUpAction != null)
    //     {
    //         InputUpAction.Invoke();
    //     }
    // }
    // public void OnPressDown(InputAction.CallbackContext context)
    // {
    //     // 押された瞬間でPerformedとなる
    //     if (!context.performed) return;
    //     Debug.Log("PressDown");
    //     if(InputDownAction != null)
    //     {
    //         InputDownAction.Invoke();
    //     }
    // }
    
    // public void OnPressLeft(InputAction.CallbackContext context)
    // {
    //     // 押された瞬間でPerformedとなる
    //     if (!context.performed) return;
    //     Debug.Log("PressLeft");
    //     if(InputLeftAction != null)
    //     {
    //         InputLeftAction.Invoke();
    //     }
    // }

    
    // public void OnPressRight(InputAction.CallbackContext context)
    // {
    //     // 押された瞬間でPerformedとなる
    //     if (!context.performed) return;
    //     Debug.Log("PressRight");
    //     if(InputRightAction != null)
    //     {
    //         InputRightAction.Invoke();
    //     }
    // }
    public void OnPressSubmit(InputAction.CallbackContext context)
    {
        // 押された瞬間でPerformedとなる
        if (!context.performed) return;
        Debug.Log("PressSubmit");
        if(InputInteractionAction != null)
        {
            InputInteractionAction.Invoke();
        }
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

    public Vector3 GetStickDir()
    {
        UnityEngine.Vector3 vec = StickDir;
        var horizontal = 0.0f;
        var vertical = 0.0f;
        if (StickDir.magnitude <= 0.0f)
        {
            var axis = move.ReadValue<Vector2>();
            horizontal = axis.x;
            vertical = axis.y;
            vec = new Vector3(horizontal ,0.0f, vertical);
            return vec;
        }
        else
        {
            return StickDir;
        }
    }
}

using UnityEngine.InputSystem;

public static class InputMapNames
{
    public const string Player = "Player";
    public const string UI = "UI";
    public const string Global = "Global";
} 

public class staticInput : staticObject<staticInput>
{
    private PlayerInput Input_;

    protected override void Awake() {
        base.Awake();
        if (!ThisInstance()) {
            return;
        }

        Input_ = GetComponent<PlayerInput>();
        DebugUtility.NullCheck(Input_, "InputNotFound");
    }

    private void OnEnable() {
        if (!DebugUtility.NullCheck(Input_, "InputNotFound")) {
            GetInputMap(InputMapNames.Global)?.Enable();
        }
    }

    private void OnDisable() {
        if (!DebugUtility.NullCheck(Input_, "InputNotFound")) {
            GetInputMap(InputMapNames.Global)?.Disable();
        }
    }

    public void ChangeInputMap(string name) {
        if (!DebugUtility.NullCheck(Input_, "InputNotFound")) {
            Input_.SwitchCurrentActionMap(name);
        }
    }

    public InputActionMap GetInputMap(string name) {
        if (!DebugUtility.NullCheck(Input_, "InputNotFound")) {
            return Input_.actions.FindActionMap(name);
        }
        return null;
    }

    public void DebugMapName() {
        if(!DebugUtility.NullCheck(Input_,"InputNotFound")) {
            DebugUtility.Log(Input_.currentActionMap.name);
        }
    }
}

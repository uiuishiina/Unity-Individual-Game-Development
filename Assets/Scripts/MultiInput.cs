using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class MultiInput : MonoBehaviour
{
    private PlayerInput Input_;
    private MultiInputManager InputManager_;

    private Dictionary<string, InputActionMap> MapList_ = new();
    private InputAction JumpAction_;
    private InputAction PlayerEscAction_;
    private InputAction UIEscAction_;
    [SerializeField] private int ID_;

    private void Awake() {
        Input_ = GetComponent<PlayerInput>();

        AddMapList("Player");
        AddMapList("UI");

        JumpAction_ = GetMap("Player").FindAction("Jump");
        PlayerEscAction_ = GetMap("Player").FindAction("Esc");
        UIEscAction_ = GetMap("UI").FindAction("Esc");

        Input_.currentActionMap = GetMap("Player");
    }
    private void OnEnable()
    {
        JumpAction_.performed += DebugObject;
        PlayerEscAction_.performed += Pouse;
        UIEscAction_.performed += Pouse;
    }
    private void OnDisable()
    {
        JumpAction_.performed -= DebugObject;
        PlayerEscAction_.performed -= Pouse;
        UIEscAction_.performed -= Pouse;
    }
    private void DebugObject(InputAction.CallbackContext context)
    {
        Debug.Log("DEBUG");
    }
    private void Pouse(InputAction.CallbackContext context)
    {
        InputManager_.OnEsc(ID_);
        Debug.Log("Pouse");
    }

    private void AddMapList(string mapName)
    {
        var map = Input_.actions.FindActionMap(mapName);
        if (map != null) {
            MapList_[mapName] = map;
        }
    }

    private InputActionMap GetMap(string mapName)
    {
        MapList_.TryGetValue(mapName, out var map);
        return map;
    }
    public void SetData(int id,MultiInputManager instance)
    {
        ID_ = id;
        InputManager_ = instance;
    }

}

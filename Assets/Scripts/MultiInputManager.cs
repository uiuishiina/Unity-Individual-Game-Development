using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MultiInputManager : MonoBehaviour
{
    public static MultiInputManager Instance_;
    private PlayerInputManager InputManager_;

    private InputActionMap PlayerMap_;
    private InputActionMap UIMap_;

    bool is_Pouse = false;

    [SerializeField] private List<PlayerInput> PlayersList_ = new();

    private void Awake()
    {
        if(Instance_ == null) {
            Instance_ = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance_ != this){
            Destroy(gameObject);
            return;
        }
        InputManager_ = GetComponent<PlayerInputManager>();
    }

    private void OnEnable()
    {
        InputManager_.onPlayerJoined += JoinPlayer;
        InputManager_.onPlayerLeft += LeftPlayer;
    }
    private void OnDisable() 
    {
        InputManager_.onPlayerJoined -= JoinPlayer;
        InputManager_.onPlayerLeft -= LeftPlayer;
    }

    private void JoinPlayer(PlayerInput input)
    {
        PlayersList_.Add(input);
        DontDestroyOnLoad(input.gameObject);

        var id = input.playerIndex;
        Debug.Log("Join Player"+ $" : PlayerID = {id}");
        foreach (var device in input.devices) {
            Debug.Log(device.displayName);
        }

        var multi = input.gameObject.GetComponent<MultiInput>();
        multi?.SetData(id,Instance_);
    }
    private void LeftPlayer(PlayerInput input)
    {
        PlayersList_.Remove(input);
        Destroy(input.gameObject);
        Debug.Log("Left Player");
    }

    private void OnDestroy()
    {
        PlayersList_.Clear();
        Destroy(gameObject);
        Debug.Log("Player Clear");
    }
    public void OnEsc(int input_id)
    {
        Debug.Log($" ON ESC : id = {input_id}");

        var mapName = is_Pouse ? "Player" : "UI";
        foreach (var input in PlayersList_)
        {
            input.SwitchCurrentActionMap(mapName);
        }
        is_Pouse = !is_Pouse;
    }
}

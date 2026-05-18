using UnityEngine;
using UnityEngine.InputSystem;

public class Scroll_Player_Shot : MonoBehaviour
{
    PlayerInput playerInput;
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        if (playerInput.actions["Attack"].WasPressedThisFrame()) {
            Debug.Log("Attack");
        }
    }
}

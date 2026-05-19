using UnityEngine;
using UnityEngine.InputSystem;

public class Scroll_Player_Shot : MonoBehaviour
{
    PlayerInput playerInput;
    [SerializeField, Header("ライトプール")] Scrool_Light_Pool pool;
    [SerializeField, Header("ライトスピード")] float light_speed = 2;
    [SerializeField, Header("クールタイム")] float cooltime = 2;

    float timer;
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if ((timer <= 0f) && playerInput.actions["Attack"].WasPressedThisFrame()) {
            Debug.Log("Attack");
            var g = pool.pool.Get();
            g.transform.position = this.transform.position + new Vector3(2, 0, 0);
            g.GetComponent<Scroll_Light_Move>().SetVector(new Vector2(light_speed, 0));
            timer = cooltime;
        }
    }
}

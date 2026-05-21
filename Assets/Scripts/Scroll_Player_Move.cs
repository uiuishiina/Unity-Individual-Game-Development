using UnityEngine;
using UnityEngine.InputSystem;
using System.Threading.Tasks;

public class Scroll_Player_Move : MonoBehaviour
{
    [SerializeField, Header("移動スピード")] private float MoveSpeed = 1;
    [SerializeField, Header("ジャンプ力")] private float JumpPower = 1;
    [SerializeField, Header("GameManager")] private GameObject Gamemanager;
    Vector3 startpos;
    PlayerInput playerInput;
    Rigidbody2D rb;
    bool IsJump = false;
    GameObject model;
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        startpos = transform.position;

        model = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        //移動処理
        var move = playerInput.actions["Move"].ReadValue<Vector2>();
        rb.linearVelocityX = move.x * MoveSpeed;

        //ジャンプ処理
        if (!IsJump && playerInput.actions["Jump"].WasPressedThisFrame())
        {
            rb.AddForce(new Vector2(0, JumpPower), ForceMode2D.Impulse);
            IsJump = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsJump && collision.gameObject.tag == "Floor")
        {
            IsJump = false;
        }
        else if(collision.gameObject.tag == "Ded")
        {

            model.GetComponent<SpriteRenderer>().enabled = false;
            if (!Gamemanager.GetComponent<Scrool_GameManager>().is_Goal)
            {
                Gamemanager.GetComponent<Scrool_GameManager>().PlayerMiss(Resetpos);
            }
        }
        else if(collision.gameObject.tag == "Flag")
        {
            startpos = collision.gameObject.GetComponent<Scrool_Flag>().restart_pos;
        }
    }

    async void Resetpos()
    {
        model.GetComponent<SpriteRenderer>().enabled = true;
        transform.position = startpos;

        for(int i = 0; i < 5; i++)
        {
            await Wait();
            model.GetComponent<SpriteRenderer>().enabled = false;
            await Wait();
            model.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    async Task Wait()
    {
        await Task.Delay(200);
    }
}

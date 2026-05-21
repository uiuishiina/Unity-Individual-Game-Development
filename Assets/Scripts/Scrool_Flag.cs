using Unity.VisualScripting;
using UnityEngine;

public class Scrool_Flag : MonoBehaviour
{
    [SerializeField,Header("リスタート位置")] public Vector3 restart_pos = new Vector3();

    void Start()
    {
        restart_pos = transform.position + restart_pos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}

using UnityEngine;

public class Tira : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        FindFirstObjectByType<GameManager>().GetComponent<GameManager>().Sporn();
        Destroy(this.gameObject);
    }
}
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 3f;

    private Vector3 moveDirection;

    public void Initialize(Vector3 targetPosition)
    {
        moveDirection = (targetPosition - transform.position).normalized;
    }

    private void Update()
    {
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            GameManager.Instance.AddScore(100);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            GameManager.Instance.GameOver();

            Destroy(gameObject);
        }
    }
}
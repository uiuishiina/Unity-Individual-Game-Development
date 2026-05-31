using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Shot : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    [Header("Shoot")]
    [SerializeField] private float cooldown = 0.3f;

    private float nextShootTime;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void OnAttack(InputValue value)
    {
        if (Time.time >= nextShootTime)
        {
            nextShootTime = Time.time + cooldown;
            Shoot();
        }
    }

    private void Shoot()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();

        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0f;

        Vector2 direction = (mouseWorldPos - firePoint.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0f, 0f, angle));

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.Initialize(direction);
        }
    }
}

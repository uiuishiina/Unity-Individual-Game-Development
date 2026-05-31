using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform player;

    [Header("Spawn")]
    [SerializeField] private float spawnInterval = 1f;

    [SerializeField] private Vector2 spawnOffset = new Vector2(8f, 8f);

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPos = new Vector3(Random.Range(6f, spawnOffset.x), Random.Range(-3, spawnOffset.y), 0f);

        GameObject enemyObj = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

        Enemy enemy = enemyObj.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.Initialize(player.position);
        }
    }
}
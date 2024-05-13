using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("References:")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Vector3[] spawnLocations;
    [SerializeField] private GameObject canvas;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float diffScalingFactor = 0.75f;

    private int currentWave = 1;
    private float timeSinceWave = 0;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;

    void Start()
    {
        StartWave();
    }

    void Update()
    {
        if (!isSpawning) return;
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f/ enemiesPerSecond) && enemiesLeftToSpawn > 0)
        {
            enemiesLeftToSpawn--;
            enemiesAlive++;
            SpawnEnemy();
            timeSinceLastSpawn = 0f;
        }
        Debug.Log(currentWave);
        if (isSpawning && enemiesLeftToSpawn == 0)
        {
            currentWave++;
            StartWave();
        }
    }

    private void StartWave()
    {
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
    }

    private void SpawnEnemy()
    {
        int enemyType = Random.Range(0, enemyPrefabs.Length);
        GameObject prefabToSpawn = enemyPrefabs[enemyType];
        int spawn = Random.Range(0, spawnLocations.Length);
        GameObject enemy = Instantiate(prefabToSpawn, spawnLocations[spawn], Quaternion.identity);
        GameObject healthBar = Instantiate(canvas, enemy.transform.position, enemy.transform.rotation);
        healthBar.transform.parent = enemy.transform;
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, diffScalingFactor));
    }
}

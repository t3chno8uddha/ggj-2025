using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class EnemyData
    {
        public UnitHealth prefab; // Enemy prefab
        [Range(0f, 1f)] public float spawnChance; // Chance to spawn (0-1)
    }

    [Header("Spawner Settings")]
    public List<EnemyData> enemies = new List<EnemyData>();
    public int enemiesPerWave = 5;
    public int increaseBy = 2;
    public float spawnDelay = 1f;
    public Transform[] spawnPoints;

    private List<UnitHealth> activeEnemies = new();
    private bool waveActive = false;

    void Start()
    {
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        waveActive = true;

        for (int i = 0; i < enemiesPerWave; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnDelay);
        }

        waveActive = false;

        yield return new WaitUntil(() => activeEnemies.Count == 0);
        enemiesPerWave += increaseBy;
        StartCoroutine(SpawnWave());
    }

    void SpawnEnemy()
    {
        var enemyToSpawn = SelectRandomEnemy();
        if (enemyToSpawn != null && spawnPoints.Length > 0)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            var enemy = Instantiate(enemyToSpawn, spawnPoint.position.WithY(0f), Quaternion.identity, null);
            activeEnemies.Add(enemy);

            enemy.OnDeath += () => activeEnemies.Remove(enemy);
        }
    }

    UnitHealth SelectRandomEnemy()
    {
        float totalChance = 0f;
        foreach (var enemy in enemies)
        {
            totalChance += enemy.spawnChance;
        }

        float randomValue = Random.value * totalChance;
        float cumulativeChance = 0f;

        foreach (var enemy in enemies)
        {
            cumulativeChance += enemy.spawnChance;
            if (randomValue <= cumulativeChance)
            {
                return enemy.prefab;
            }
        }

        return null;
    }
}
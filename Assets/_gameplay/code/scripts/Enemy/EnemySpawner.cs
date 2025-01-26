using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
    public Transform[] SpawnPoints;

    private int _currentBaseIndex = 0;
    public Transform MainBase;
    public Transform[] MainBaseLocations;
    public float jumpPower = 10;
    public float jumpDuration = 4;

    [SerializeField] private List<UnitHealth> activeEnemies = new();
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
        ShiftMainBase();

        StartCoroutine(SpawnWave());
    }

    void SpawnEnemy()
    {
        var enemyToSpawn = SelectRandomEnemy();
        if (enemyToSpawn != null && SpawnPoints.Length > 0)
        {
            Transform spawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
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

    private void ShiftMainBase()
    {
        _currentBaseIndex += 1;

        if (_currentBaseIndex > MainBaseLocations.Length - 1)
        {
            _currentBaseIndex = 0;
        }

        var nextPoint = MainBaseLocations[_currentBaseIndex];

        MainBase.DOJump(nextPoint.position, jumpPower, 1, jumpDuration).SetEase(Ease.InOutQuad);
    }
}
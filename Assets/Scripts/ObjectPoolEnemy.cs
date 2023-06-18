using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolEnemy : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform[] _spawnPoints;

    private List<GameObject> _enemyPool = new List<GameObject>();

    private int _minSpawnCount = 6;
    private int _maxSpawnCount = 10;
    private bool _stopSpawning = false;

    private void Start()
    {
        _stopSpawning = false;
        SpawnInitialEnemies();
    }

    private void Update()
    {
        int activeEnemies = CountActiveEnemies();
        if (activeEnemies < 3 && !_stopSpawning)
        {
            SpawnAdditionalEnemies();
        }
    }

    private void SpawnInitialEnemies()
    {
        int spawnCount = Random.Range(_minSpawnCount, _maxSpawnCount + 1);
        for (int i = 0; i < spawnCount; i++)
        {
            GameObject enemy = Instantiate(_enemyPrefab, GetRandomSpawnPoint(), Quaternion.Euler(0f, 180f, 0f));
            enemy.SetActive(false);
            _enemyPool.Add(enemy);
        }
    }

    private void SpawnAdditionalEnemies()
    {
        int spawnCount = Random.Range(_minSpawnCount, _maxSpawnCount + 1);
        for (int i = 0; i < spawnCount; i++)
        {
            GameObject enemy = GetInactiveEnemy();
            if (enemy == null)
            {
                enemy = Instantiate(_enemyPrefab, GetRandomSpawnPoint(), Quaternion.Euler(0f, 180f, 0f));
                enemy.SetActive(false);
                _enemyPool.Add(enemy);
            }
            enemy.SetActive(true);
        }
    }

    private GameObject GetInactiveEnemy()
    {
        for (int i = 0; i < _enemyPool.Count; i++)
        {
            GameObject enemy = _enemyPool[i];
            if (enemy != null && !enemy.activeSelf)
            {
                return enemy;
            }
        }
        return null;
    }


    private Vector3 GetRandomSpawnPoint()
    {
        int randomIndex = Random.Range(0, _spawnPoints.Length);
        return _spawnPoints[randomIndex].position;
    }

    private int CountActiveEnemies()
    {
        int count = 0;
        foreach (GameObject enemy in _enemyPool)
        {
            if (enemy != null && enemy.activeSelf)
            {
                count++;
            }
        }
        return count;
    }
    
    public void StopEnemySpawning()
    {
        _stopSpawning = true;
    }
}

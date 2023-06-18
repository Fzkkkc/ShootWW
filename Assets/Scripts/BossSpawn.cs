using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    [SerializeField] private GameObject _bossPrefab;
    [SerializeField] private Transform _spawnPoint;
    
    private float _bossSpawnTimer = 20f;
    private bool _isBossSpawned = false;
    
    public float BossSpawnTimer => _bossSpawnTimer;

    private void Update()
    {
        if (!_isBossSpawned)
        {
            _bossSpawnTimer -= Time.deltaTime;
            if (_bossSpawnTimer <= 0f)
            {
                SpawnBoss();
                _isBossSpawned = true;
            }
        }
    }

    private void SpawnBoss()
    {
        if (_bossPrefab != null && _spawnPoint != null)
        {
            GameObject boss = Instantiate(_bossPrefab, _spawnPoint.position, Quaternion.Euler(0f, 180f, 0f));
        }
    }
}
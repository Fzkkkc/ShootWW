using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }
    public event Action<IEnemyBehavior> OnEnemyKilledEvent;
    public event Action<IEnemyBossBehavior> OnEnemyBossKilledEvent;

    private List<IEnemyBehavior> _enemyList = new List<IEnemyBehavior>();

    [SerializeField] private ObjectPoolEnemy _objectPoolEnemy;
    [SerializeField] private EndPanel _endPanel;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void RegisterEnemy(IEnemyBehavior enemy)
    {
        _enemyList.Add(enemy);
        enemy.OnEnemyKilledEvent += OnOnEnemyKilledEvent;
        enemy.OnEnemyReachedEndEvent += OnEnemyReachedEnd;

        if (enemy is IEnemyBossBehavior bossEnemy)
        {
            bossEnemy.OnEnemyBossKilledEvent += OnEnemyBossKilled;
        }
    }

    public void UnregisterEnemy(IEnemyBehavior enemy)
    {
        _enemyList.Remove(enemy);
        enemy.OnEnemyKilledEvent -= OnOnEnemyKilledEvent;
        enemy.OnEnemyReachedEndEvent -= OnEnemyReachedEnd;

        if (enemy is IEnemyBossBehavior bossEnemy)
        {
            bossEnemy.OnEnemyBossKilledEvent -= OnEnemyBossKilled;
        }
    }

    private void OnOnEnemyKilledEvent(IEnemyBehavior enemy)
    {
        OnEnemyKilledEvent?.Invoke(enemy);
    }

    private void OnEnemyBossKilled(IEnemyBossBehavior bossEnemy)
    {
        OnEnemyBossKilledEvent?.Invoke(bossEnemy);
        ClearRegisteredEnemies();
        _endPanel.SetWinPanel();
    }
    
    private void OnEnemyReachedEnd(IEnemyBehavior enemy)
    {
        _endPanel.SetLosePanel();
        ClearRegisteredEnemies();
        Debug.Log("LOSE");
    }

    private void ClearRegisteredEnemies()
    {
        _objectPoolEnemy.StopEnemySpawning();
        List<IEnemyBehavior> enemyListCopy = new List<IEnemyBehavior>(_enemyList);

        foreach (IEnemyBehavior enemy in enemyListCopy)
        {
            enemy.OnEnemyKilledEvent -= OnOnEnemyKilledEvent;
            if (enemy is MonoBehaviour enemyMonoBehaviour)
            {
                Destroy(enemyMonoBehaviour.gameObject);
            }
        }

        _enemyList.Clear();
    }
}
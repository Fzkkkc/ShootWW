using System;
using UnityEngine;

public class EnemyBoss : MonoBehaviour, IEnemyBehavior, IEnemyBossBehavior
{
    public int Reward { get; private set; }
    public event Action<IEnemyBehavior> OnEnemyKilledEvent;
    public event Action<IEnemyBossBehavior> OnEnemyBossKilledEvent;
    public event Action<IEnemyBehavior> OnEnemyReachedEndEvent;
    
    public int Health { get; private set; }

    [SerializeField] private float _speed = 10f;

    private Rigidbody _enemyBossRigidbody;
    private Animator _enemyBossAnimator;
    private EnemyManager _enemyManager;
    
    private void Awake()
    {
        Health = 40;
        Reward = 10;
        _enemyBossRigidbody = GetComponent<Rigidbody>();
        _enemyBossAnimator = GetComponent<Animator>();
        _enemyManager = EnemyManager.Instance;
    }

    private void FixedUpdate()
    {
        EnemyMoveForward();
    }
    
    private void OnEnable()
    {
        _enemyManager.RegisterEnemy(this);
    }

    private void OnDisable()
    {
        _enemyManager.UnregisterEnemy(this);
    }

    public void EnemyMoveForward()
    {
        Vector3 forwardDirection = transform.forward;
        _enemyBossRigidbody.MovePosition(_enemyBossRigidbody.position + forwardDirection * _speed * Time.fixedDeltaTime);
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            OnEnemyKilledEvent?.Invoke(this);
            OnEnemyBossKilledEvent?.Invoke(this);
            _enemyBossAnimator.SetTrigger("EnemyDead");
            _speed = 0f;
            Destroy(gameObject, 2f);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LoseCollider"))
        {
            OnEnemyReachedEndEvent?.Invoke(this);
        }
    }
}
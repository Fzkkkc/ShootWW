using System;
using UnityEngine;

public class DefaultEnemy : MonoBehaviour, IEnemyBehavior
{
    [SerializeField] private float _speed = 5;

    private Rigidbody _enemyRigidbody;
    private Animator _enemyAnimator;
    private EnemyManager _enemyManager;
    
    public event Action<IEnemyBehavior> OnEnemyKilledEvent;
    public event Action<IEnemyBehavior> OnEnemyReachedEndEvent;
    
    public int Health { get; private set; }
    
    public int Reward { get; private set; }
    
    private void Awake()
    {
        Health = 1;
        Reward = 1;
        _enemyRigidbody = GetComponent<Rigidbody>();
        _enemyAnimator = GetComponent<Animator>();
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
        _enemyRigidbody.MovePosition(_enemyRigidbody.position + forwardDirection * _speed * Time.fixedDeltaTime);
    }


    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            OnEnemyKilledEvent?.Invoke(this);
            _enemyAnimator.SetTrigger("EnemyDead");
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